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

            internal uint Value;
        }

        protected readonly StringBuilder builder= new StringBuilder();

        public Lexer(SourceText text)
            : base(text)
        {
            
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
            var errors = new DiagnosticInfo[0];

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

                    if(TextWindow.PeekChar() == '=')
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
                    if(!TextWindow.IsReallyAtEnd())
                    {
                        goto default;
                    }

                    //check if any syntax is unfinished. e.g. if

                    //now we are really at the file end.
                    info.Kind = SyntaxKind.EndOfFileToken;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool ScanIdentifierOrKeyword(ref TokenInfo info)
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
                        val = GetValueUInt64(valueText, isHex);
                    }

                    //we only support 32Bit unsigned
                    if(val <= UInt32.MaxValue)
                    {
                        info.ValueKind = SpecialType.UInt32;
                        info.Value = (uint)val;
                    }
                    else
                    {
                        //this.AddError(MakeError(ErrorCode.ERR_IntOverflow));
                        throw new NotImplementedException("Implement: this.AddError(MakeError(ErrorCode.ERR_IntOverflow))");
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

        private ulong GetValueUInt64(string text, bool isHex)
        {
            ulong result;
            if (!UInt64.TryParse(text, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.None, CultureInfo.InvariantCulture, out result))
            {
                //we've already lexed the literal, so the error must be from overflow
                //this.AddError(MakeError(ErrorCode.ERR_IntOverflow));
                throw new NotImplementedException("Implement: this.AddError(MakeError(ErrorCode.ERR_IntOverflow))");
            }

            return result;
        }

        #endregion Lex NumericalLiterals
        //##############################





        //####
        //Other
        //####

        private SyntaxToken Create(ref TokenInfo info, object leading, object trailing, DiagnosticInfo[] errors)
        {
            //Debug.Assert(info.Kind != SyntaxKind.IdentifierToken || info.StringValue != null);

            //var leadingNode = leading?.ToListNode();
            //var trailingNode = trailing?.ToListNode();

            SyntaxToken token;

            switch (info.Kind)
            {
                case SyntaxKind.None:
                    //    token = SyntaxFactory.BadToken()
                    throw new NotImplementedException();

                //case SyntaxKind.EndOfLineTrivia:
                //    token = SyntaxFactory.EndOfLine(info.Text);
                //    break;

                case SyntaxKind.NumericLiteralToken:
                    switch (info.ValueKind)
                    {
                        case SpecialType.UInt32:
                            //token = SyntaxFactory.Literal(leadingNode, info.Text, info.UintValue, trailingNode);
                            token = SyntaxFactory.Literal(info.Text, info.Value);
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

            //throw new NotImplementedException();

            return token;
        }

    }
}
