using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.Core.Syntax.InternalSyntax
{
    internal class SlidingTextWindow : IDisposable
    {
        /// <summary>
        /// In many cases, e.g. PeekChar, we need the ability to indicate that there are
        /// no characters left and we have reached the end of the stream, or some other
        /// invalid or not present character was asked for. Due to perf concerns, things
        /// like nullable or out variables are not viable. Instead we need to choose a
        /// char value which can never be legal.
        /// 
        /// In .NET, all characters are represented in 16 bits using the UTF-16 encoding.
        /// Fortunately for us, there are a variety of different bit patterns which
        /// are *not* legal UTF-16 characters. 0xffff (char.MaxValue) is one of these
        /// characters -- a legal Unicode code point, but not a legal UTF-16 bit pattern.
        /// </summary>
        public const char InvalidCharacter = char.MaxValue;

        private const int DefaultWindowLength = 2048;

        private readonly string _text;                     // Source of text to parse.
        private int _basis;                                // Offset of the window relative to the SourceText start.
        private int _offset;                               // Offset from the start of the window.
        private readonly int _textEnd;                     // Absolute end position
        private char[] _characterWindow;                   // Moveable window of chars from source text
        private int _characterWindowCount;                 // # of valid characters in chars buffer

        private int _lexemeStart;                          // Start of current lexeme relative to the window start.

        // Example for the above variables:
        // The text starts at 0.
        // The window onto the text starts at basis.
        // The current character is at (basis + offset), AKA the current "Position".
        // The current lexeme started at (basis + lexemeStart), which is <= (basis + offset)
        // The current lexeme is the characters between the lexemeStart and the offset.

        public SlidingTextWindow(string text)
        {
            _text = text;
            _basis = 0;
            _offset = 0;
            _textEnd = text.Length;
            //_strings = StringTable.GetInstance();
            //_characterWindow = s_windowPool.Allocate();
            _characterWindow = text.ToCharArray();
            _lexemeStart = 0;
        }

        /// <summary>
        /// The current absolute position in the text file.
        /// </summary>
        public int Position
        {
            get
            {
                return _basis + _offset;
            }
        }

        /// <summary>
        /// The current offset inside the window (relative to the window start).
        /// </summary>
        public int Offset
        {
            get
            {
                return _offset;
            }
        }

        public void Reset(int position)
        {
            // if position is within already read character range then just use what we have
            int relative = position - _basis;
            if (relative >= 0 && relative <= _characterWindowCount)
            {
                _offset = relative;
            }
            else
            {
                // we need to reread text buffer
                int amountToRead = Math.Min(_text.Length, position + _characterWindow.Length) - position;
                amountToRead = Math.Max(amountToRead, 0);
                if (amountToRead > 0)
                {
                    _text.CopyTo(position, _characterWindow, 0, amountToRead);
                }

                _lexemeStart = 0;
                _offset = 0;
                _basis = position;
                _characterWindowCount = amountToRead;
            }
        }

        private bool MoreChars()
        {
            if (_offset >= _characterWindowCount)
            {
                if (this.Position >= _textEnd) return false;

                if (_characterWindowCount >= _characterWindow.Length)
                {
                    // grow char array, since we need more contiguous space
                    char[] oldWindow = _characterWindow;
                    char[] newWindow = new char[_characterWindow.Length * 2];
                    Array.Copy(oldWindow, 0, newWindow, 0, _characterWindowCount);
                    _characterWindow = newWindow;
                }

                int amountToRead = Math.Min(_textEnd - (_basis + _characterWindowCount),
                    _characterWindow.Length - _characterWindowCount);
                _text.CopyTo(_basis + _characterWindowCount,
                    _characterWindow,
                    _characterWindowCount,
                    amountToRead);
                _characterWindowCount += amountToRead;
                return amountToRead > 0;
            }

            return true;
        }

        /// <summary>
        /// After reading <see cref=" InvalidCharacter"/>, a consumer can determine
        /// if the InvalidCharacter was in the user's source or a sentinel.
        /// 
        /// Comments and string literals are allowed to contain any Unicode character.
        /// </summary>
        /// <returns></returns>
        internal bool IsReallyAtEnd()
        {
            return _offset >= _characterWindowCount && Position >= _textEnd;
        }

        /// <summary>
        /// Advance the current position by one. No guarantee that this
        /// position is valid.
        /// </summary>
        public void AdvanceChar()
        {
            _offset++;
        }

        /// <summary>
        /// Advance the current position by n. No guarantee that this position
        /// is valid.
        /// </summary>
        public void AdvanceChar(int n)
        {
            _offset += n;
        }

        /// <summary>
        /// Grab the next character and advance the position.
        /// </summary>
        /// <returns>
        /// The next character, <see cref="InvalidCharacter" /> if there were no characters 
        /// remaining.
        /// </returns>
        public char NextChar()
        {
            char c = PeekChar();
            if (c != InvalidCharacter)
            {
                this.AdvanceChar();
            }
            return c;
        }

        /// <summary>
        /// Gets the next character if there are any characters in the 
        /// SourceText. May advance the window if we are at the end.
        /// </summary>
        /// <returns>
        /// The next character if any are available. InvalidCharacter otherwise.
        /// </returns>
        public char PeekChar()
        {
            if (_offset >= _characterWindowCount
                && !MoreChars())
            {
                return InvalidCharacter;
            }

            // N.B. MoreChars may update the offset.
            return _characterWindow[_offset];
        }

        /// <summary>
        /// Gets the character at the given offset to the current position if
        /// the position is valid within the SourceText.
        /// </summary>
        /// <returns>
        /// The next character if any are available. InvalidCharacter otherwise.
        /// </returns>
        public char PeekChar(int delta)
        {
            int position = this.Position;
            this.AdvanceChar(delta);

            char ch;
            if (_offset >= _characterWindowCount
                && !MoreChars())
            {
                ch = InvalidCharacter;
            }
            else
            {
                // N.B. MoreChars may update the offset.
                ch = _characterWindow[_offset];
            }

            this.Reset(position);
            return ch;
        }

       

      

        #region IDisposeable    
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: Verwalteten Zustand (verwaltete Objekte) bereinigen
                }

                // TODO: Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                // TODO: Große Felder auf NULL setzen
                disposedValue = true;
            }
        }

        // // TODO: Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
        // ~SlidingTextWindow()
        // {
        //     // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposeable
    }
}
