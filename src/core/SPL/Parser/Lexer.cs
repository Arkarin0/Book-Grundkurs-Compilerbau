using Arkarin0.CodeAnalysis;
using Arkarin0.CodeAnalysis.Syntax;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Sonea.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal partial class Lexer : AbstractLexer
    {
        internal enum SpecialType
        {
            None,
            UInt32,
            Character,
        }

        internal struct TokenInfo
        {
            internal SyntaxKind Kind;
            internal string Text;
            internal SpecialType ValueKind;

            internal uint UInt32Value;
            internal string StringValue;
        }

        protected readonly StringBuilder builder;

        private char[] _identBuffer;
        private int _identLen;

        private int _badTokenCount; // cumulative count of bad tokens produced

        public Lexer(SourceText text)
            : base(text)
        {
            this.builder = new StringBuilder();
            this._identBuffer = new char[32];
        }


#if DEBUG
        internal static int TokensLexed;
#endif

        public SyntaxToken Lex()
        {
#if DEBUG
            TokensLexed++;
#endif

            //return this.QuickScanSyntaxToken() ?? this.LexSyntaxToken();
            return this.LexSyntaxToken();
        }


        private SyntaxToken LexSyntaxToken()
        {
            //_leadingTriviaCache.Clear();
            //this.LexSyntaxTrivia(afterFirstToken: TextWindow.Position > 0, isTrailing: false, triviaList: ref _leadingTriviaCache);
            //var leading = _leadingTriviaCache;

            var tokenInfo = default(TokenInfo);

            this.Start();
            this.ScanSyntaxToken(ref tokenInfo);
            //var errors = this.GetErrors(GetFullWidth(leading));
            var errors = this.GetErrors();

            //_trailingTriviaCache.Clear();
            //this.LexSyntaxTrivia(afterFirstToken: true, isTrailing: true, triviaList: ref _trailingTriviaCache);
            //var trailing = _trailingTriviaCache;

            //return Create(ref tokenInfo, leading, trailing, errors);
            return Create(ref tokenInfo, null, null, errors);
        }

        private void ScanSyntaxToken(ref TokenInfo info)
        {
            //initilaize for new token scan
            info.Kind = SyntaxKind.None;
            info.Text = null;

            char character;
            int startingPosition = TextWindow.Position;

            //start scanning
            character = TextWindow.PeekChar();
            switch (character)
            {
                case '+':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.PlusToken;
                    break;
                case '-':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.MinusToken;
                    break;
                case '*':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.AsteriskToken;
                    break;
                case '/':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.SlashToken;
                    break;
                case ':':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.ColonToken;

                    if (TextWindow.PeekChar() == '=')
                    {
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.ColonEqualToken;
                    }
                    break;
                case ';':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.SemicolonToken;
                    break;
                case ',':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.CommaToken;
                    break;
                case '<':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.LessThanToken;

                    if (TextWindow.PeekChar() == '=')
                    {
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.LessThenEqualToken;
                    }
                    break;
                case '>':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.GreaterThanToken;

                    if (TextWindow.PeekChar() == '=')
                    {
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.GreaterThenEqualToken;
                    }
                    break;
                case '=':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.EqualsToken;
                    break;
                case '#':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.HashToken;
                    break;
                case '(':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.OpenParenToken;
                    break;
                case ')':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.CloseParenToken;
                    break;
                case '{':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.OpenBraceToken;
                    break;
                case '}':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.CloseBraceToken;
                    break;
                case '[':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.OpenBracketToken;
                    break;
                case ']':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.CloseBracketToken;
                    break;
                case '\'':
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.SingleQuoteToken;
                    break;

                // All the 'common' identifier characters are represented directly in
                // these switch cases for optimal perf.  Calling IsIdentifierChar() functions is relatively
                // expensive.
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                case '_':
                    this.ScanIdentifierOrKeyword(ref info);
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    this.ScanNumericLiteral(ref info);
                    break;


                case SlidingTextWindow.InvalidCharacter:
                    if (!TextWindow.IsReallyAtEnd())
                    {
                        goto default;
                    }

                    //check if any syntax is unfinished. e.g. if

                    //now we are really at the file end.
                    info.Kind = SyntaxKind.EndOfFileToken;
                    break;
                default:
                    if (SyntaxFacts.IsIdentifierStartCharacter(character)) goto case 'a';

                    if (_badTokenCount++ > 200)
                    {
                        // If we get too many characters that we cannot make sense of, absorb the rest of the input.
                        int end = TextWindow.Text.Length;
                        int width = end - startingPosition;
                        info.Text = TextWindow.Text.ToString(new TextSpan(startingPosition, width));
                        TextWindow.Reset(end);
                    }
                    else
                    {
                        info.Text = TextWindow.GetText(intern: true);
                    }

                    this.AddError(ErrorCode.ERR_UnexpectedCharacter, info.Text);
                    break;
            }
        }

        //##############################
        #region Lex Identifier or Keyword
        //##############################

        private bool ScanIdentifierOrKeywordabc(ref TokenInfo info)
        {
            int offset = 0;
            builder.Clear();
            bool exit = false;
            do
            {
                //get the actual char into the buffer
                char character = TextWindow.PeekChar(offset);
                builder.Append(character);

                //peel next char
                character = TextWindow.PeekChar(offset + 1);
                switch (character)
                {
                    case SlidingTextWindow.InvalidCharacter:
                        info.Kind = SyntaxKind.EndOfFileToken;
                        exit = true;
                        break;

                    case ' ':
                    case '\r':
                    case '\n':
                    case '\t':
                    case '\v':
                        exit = true;
                        break;

                }

                offset++;
            } while (!exit);
            var text= builder.ToString();
            var kind = SyntaxFacts.GetKeywordKind(text);
            if (text == "_") kind = SyntaxKind.UnderscoreToken;
            else if (kind == SyntaxKind.None) kind = SyntaxKind.IdentifierToken;

            TextWindow.AdvanceChar(offset);
            info.Text = text;
            info.Kind = kind;

            return true;
        }

        private bool ScanIdentifierOrKeyword(ref TokenInfo info)
        {
            if(ScanIdentifier(ref info))
            {
                //check if the text found is a keyword
                //if (!_cache.TryGetKeywordKind(info.Text, out info.Kind))
                //{
                //    /*info.ContextualKind =*/ info.Kind = SyntaxKind.IdentifierToken;
                //}
                //else if (SyntaxFacts.IsContextualKeyword(info.Kind))
                //{
                //    info.ContextualKind = info.Kind;
                //    info.Kind = SyntaxKind.IdentifierToken;
                //}


                //if it's not a keyword then it must be an identifier.
                if (info.Kind == SyntaxKind.None)
                    info.Kind= SyntaxKind.IdentifierToken;

                return true;
            }
            else
            {
                info.Kind = SyntaxKind.None;
                return false;
            }
        }
        private bool ScanIdentifier(ref TokenInfo info)
        {
            return
                ScanIdentifier_FastPath(ref info) ||
                ScanIdentifier_SlowPath(ref info);
        }

        // Implements a faster identifier lexer for the common case in the 
        // language where:
        //
        //   a) identifiers are not verbatim
        //   b) identifiers don't contain unicode characters
        //   c) identifiers don't contain unicode escapes
        //
        // Given that nearly all identifiers will contain [_a-zA-Z0-9] and will
        // be terminated by a small set of known characters (like dot, comma, 
        // etc.), we can sit in a tight loop looking for this pattern and only
        // falling back to the slower (but correct) path if we see something we
        // can't handle.
        //
        // Note: this function also only works if the identifier (and terminator)
        // can be found in the current sliding window of chars we have from our
        // source text.  With this constraint we can avoid the costly overhead 
        // incurred with peek/advance/next.  Because of this we can also avoid
        // the unnecessary stores/reads from identBuffer and all other instance
        // state while lexing.  Instead we just keep track of our start, end,
        // and max positions and use those for quick checks internally.
        //
        // Note: it is critical that this method must only be called from a 
        // code path that checked for IsIdentifierStartChar or '@' first. 
        private bool ScanIdentifier_FastPath(ref TokenInfo info)
        {

            var currentOffset = TextWindow.Offset;
            var characterWindow = TextWindow.CharacterWindow;
            var characterWindowCount = TextWindow.CharacterWindowCount;

            var startOffset = currentOffset;

            while (true)
            {
                if (currentOffset == characterWindowCount)
                {
                    // no more contiguous characters.  Fall back to slow path
                    return false;
                }

                switch (characterWindow[currentOffset])
                {
                    case '\0':
                    case ' ':
                    case '\r':
                    case '\n':
                    case '\t':
                    case '(':
                    case ')':
                    case '*':
                    case '+':
                    case ',':
                    case '-':
                    case '/':
                    case ':':
                    case ';':
                    case '<':
                    case '=':
                    case '>':
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                    case '"':
                    case '\'':
                        // All of the following characters are not valid in an 
                        // identifier.  If we see any of them, then we know we're
                        // done.
                        var length = currentOffset - startOffset;
                        TextWindow.AdvanceChar(length);
                        info.Text = info.StringValue = TextWindow.Intern(characterWindow, startOffset, length);
                        return true;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        if (currentOffset == startOffset)
                        {
                            return false;
                        }
                        else
                        {
                            goto case 'A';
                        }
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case '_':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        // All of these characters are valid inside an identifier.
                        // consume it and keep processing.
                        currentOffset++;
                        continue;

                    // case '@':  verbatim identifiers are handled in the slow path
                    // case '\\': unicode escapes are handled in the slow path
                    default:
                        // Any other character is something we cannot handle.  i.e.
                        // unicode chars or an escape.  Just break out and move to
                        // the slow path.
                        return false;
                }
            }
        }
        private bool ScanIdentifier_SlowPath(ref TokenInfo info)
        {
            int start = TextWindow.Position;
            this.ResetIdentBuffer();
            
            while (true)
            {
                char surrogateCharacter = SlidingTextWindow.InvalidCharacter;
                char ch = TextWindow.PeekChar();

                switch (ch)
                {
                    case SlidingTextWindow.InvalidCharacter:
                        if (!TextWindow.IsReallyAtEnd())
                        {
                            goto default;
                        }

                        goto LoopExit;
                    case '_':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        {
                            // Again, these are the 'common' identifier characters...
                            break;
                        }

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        {
                            // Again, these are the 'common' identifier characters...
                            break;
                        }

                    case ' ':
                    case '\t':
                    case ';':
                    case '(':
                    case ')':
                    case ',':
                        // ...and these are the 'common' stop characters.
                        goto LoopExit;

                    default:
                        {
                            // This is the 'expensive' call
                            if (_identLen == 0 && ch > 127 && SyntaxFacts.IsIdentifierStartCharacter(ch))
                            {
                                break;
                            }
                            else if (_identLen > 0 && ch > 127 && SyntaxFacts.IsIdentifierPartCharacter(ch))
                            {                               
                                break;
                            }
                            else
                            {
                                // Not a valid identifier character, so bail.                                
                                goto LoopExit;                                
                            }
                        }
                }


                    TextWindow.AdvanceChar();


                this.AddIdentChar(ch);
                if (surrogateCharacter != SlidingTextWindow.InvalidCharacter)
                {
                    this.AddIdentChar(surrogateCharacter);
                }
            }

        LoopExit:

            var width = TextWindow.Width; // exact size of input characters
            if (_identLen > 0)
            {
                info.Text = TextWindow.GetInternedText();

                // id buffer is identical to width in input
                if (_identLen == width)
                {
                    info.StringValue = info.Text;
                }
                else
                {
                    info.StringValue = TextWindow.Intern(_identBuffer, 0, _identLen);
                }
                               

                return true;
            }

        //Fail:
            info.Text = null;
            info.StringValue = null;
            TextWindow.Reset(start);
            return false;
        }


        #endregion Lex Identifier or Keyword
        //##############################

        //##############################
        #region Lex NumericalLiterals
        //##############################

        private bool ScanNumericLiteral(ref TokenInfo info)
        {
            int start = TextWindow.Position;
            char ch;
            bool isHex = false;
            info.Text = null;
            info.ValueKind = SpecialType.None;
            builder.Clear();

            //check the int representation. hex? bin? oct?
            ch = TextWindow.PeekChar();
            if(ch== '0')
            {
                ch = TextWindow.PeekChar(1);
                if(ch== 'x' || ch== 'X')
                {
                    TextWindow.AdvanceChar(2);
                    isHex = true;
                }
            }

            //get the value
            ScanNumericaLiteralSingleInteger(isHex);

            info.Kind = SyntaxKind.NumericLiteralToken;
            info.Text = TextWindow.GetText(true);
            Debug.Assert(info.Text != null);
            var valueText = TextWindow.Intern(builder);
            ulong val;

            switch (info.ValueKind)
            {
                //to add other NOT integar values uncomment the line below.
                //case SpecialType.Double:
                //    info.DoubleValue= this GetDoubleValue(valueText);

                //a normal int value of any size. 16Bit 32Bit 64Bit signed or unsigned
                default:
                    if (string.IsNullOrEmpty(valueText))
                    {
                        val = 0; //safe default;
                    }
                    else
                    {
                        //store the Value in the biggest container possible
                        val = GetValueUInt32(valueText, isHex);
                    }

                    //we only support 32Bit unsigned
                    if(val <= UInt32.MaxValue)
                    {
                        info.ValueKind = SpecialType.UInt32;
                        info.UInt32Value = (uint)val;
                    }
                    else
                    {
                        this.AddError(MakeError(ErrorCode.ERR_IntOverflow));
                        //store the value in the biggest container.
                        info.ValueKind = SpecialType.UInt32;
                        info.UInt32Value = (uint)val;
                    }

                    break;
            }
                        

            return true;
        }

        /// <summary>
        /// Scans the text as long it find numerical digits [0-9].
        /// </summary>
        /// <param name="isHex"><see langword="true"/> to include Hex Digits [a-f|A-F].</param>
        private void ScanNumericaLiteralSingleInteger(bool isHex=false)
        {
            while (true)
            {
                char ch = TextWindow.PeekChar();
                if (!(isHex ? SyntaxFacts.IsHexDigit(ch) :
                              SyntaxFacts.IsDecDigit(ch)))
                {
                    break;
                }
                else
                {
                    builder.Append(ch);
                }
                TextWindow.AdvanceChar();
            }
        }

        private uint GetValueUInt32(string text, bool isHex)
        {
            uint result;
            if (!UInt32.TryParse(text, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None, CultureInfo.InvariantCulture, out result))
            {
                //we've already lexed the literal, so the error must be from overflow
                this.AddError(MakeError(ErrorCode.ERR_IntOverflow));
            }

            return result;
        }

        #endregion Lex NumericalLiterals
        //##############################





        //####
        //Other
        //####

        private void ResetIdentBuffer()
        {
            _identLen = 0;
        }

        private void AddIdentChar(char ch)
        {
            if (_identLen >= _identBuffer.Length)
            {
                this.GrowIdentBuffer();
            }

            _identBuffer[_identLen++] = ch;
        }

        private void GrowIdentBuffer()
        {
            var tmp = new char[_identBuffer.Length * 2];
            Array.Copy(_identBuffer, tmp, _identBuffer.Length);
            _identBuffer = tmp;
        }

        private SyntaxToken Create(ref TokenInfo info, object leading, object trailing, DiagnosticInfo[] errors)
        {
            //Debug.Assert(info.Kind != SyntaxKind.IdentifierToken || info.StringValue != null);

            //var leadingNode = leading?.ToListNode();
            //var trailingNode = trailing?.ToListNode();

            SyntaxToken token;

            switch (info.Kind)
            {
                case SyntaxKind.IdentifierToken:
                    token = SyntaxFactory.Identifier(info.Text);
                    break;

                case SyntaxKind.None:
                    token = SyntaxFactory.BadToken(info.Text);
                    break;


                //case SyntaxKind.EndOfLineTrivia:
                //    token = SyntaxFactory.EndOfLine(info.Text);
                //    break;

                case SyntaxKind.NumericLiteralToken:
                    switch (info.ValueKind)
                    {
                        case SpecialType.UInt32:
                            //token = SyntaxFactory.Literal(leadingNode, info.Text, info.UintValue, trailingNode);
                            token = SyntaxFactory.Literal(info.Text, info.UInt32Value);
                            break;
                        default:
                            throw ExceptionUtilities.UnexpectedValue(info.ValueKind);
                    }
                    break;

                default:
                    Debug.Assert(SyntaxFacts.IsPunctuationOrKeyword(info.Kind));
                    //token = SyntaxFactory.Token(leading, info.Kind, trailing);
                    token = SyntaxFactory.Token(info.Kind);
                    break;
            }

            if (errors != null)
            {
                token = token.WithDiagnosticsGreen(errors);
            }

            return token;
        }

    }
}
