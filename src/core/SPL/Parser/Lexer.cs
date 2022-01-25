using Arkarin0.CodeAnalysis;
using Arkarin0.CodeAnalysis.Syntax;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal partial class Lexer : AbstractLexer
    {
        //        private const int TriviaListInitialCapacity = 8;

        internal struct TokenInfo
        {
            internal SyntaxKind Kind;
            internal string Text;
        }

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

        //        private SyntaxListBuilder _leadingTriviaCache = new SyntaxListBuilder(10);
        //        private SyntaxListBuilder _trailingTriviaCache = new SyntaxListBuilder(10);

        //        //private static int GetFullWidth(SyntaxListBuilder builder)
        //        //{
        //        //    int width = 0;

        //        //    if (builder != null)
        //        //    {
        //        //        for (int i = 0; i < builder.Count; i++)
        //        //        {
        //        //            width += builder[i].FullWidth;
        //        //        }
        //        //    }

        //        //    return width;
        //        //}

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

        private bool ScanIdentifierOrKeyword( ref TokenInfo info)
        {
            char character= TextWindow.PeekChar();

            if(ScanIdentifier(ref info))
            {

            }
            else if (ScanKeyword(ref info))
            {

            }else
                switch (character)
                {
                    case '_':
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.UnderscoreToken;
                        break;
                    default:
                        throw new NotImplementedException();
                }

            return true;
        }
        private bool ScanIdentifier(ref TokenInfo info)
        {
            return false;
        }
        private bool ScanKeyword(ref TokenInfo info)
        {
            return false;
        }

        //        private void LexComment(ref TokenInfo token, SlidingTextWindow reader)
        //        {
        //            //var text = "";
        //            //var c = '\0';

        //            //do
        //            //{
        //            //    c = reader.NextChar();
        //            //    text += c;

        //            //} while (!reader.IsReallyAtEnd() && c != '\n');

        //            //token.Kind = SyntaxKind.SingleLineCommentToken;
        //            //token.Text = text;
        //        }
        //        private void LexMultilineComment(ref TokenInfo token, SlidingTextWindow reader)
        //        {
        //            //var text = "";
        //            //var c = '\0';

        //            //do
        //            //{
        //            //    c = reader.NextChar();
        //            //    text += c;

        //            //} while (!reader.IsReallyAtEnd() && !text.EndsWith("*/"));

        //            //token.Kind = SyntaxKind.MultilineCommentToken;
        //            //token.Text = text;
        //        }

        //private SyntaxToken Create(ref TokenInfo info, SyntaxListBuilder leading, SyntaxListBuilder trailing, SyntaxDiagnosticInfo[] errors)
        private SyntaxToken Create(ref TokenInfo info, object leading, object trailing, DiagnosticInfo[] errors)
        {
            //Debug.Assert(info.Kind != SyntaxKind.IdentifierToken || info.StringValue != null);

            //var leadingNode = leading?.ToListNode();
            //var trailingNode = trailing?.ToListNode();

            SyntaxToken token;

            switch (info.Kind)
            {
                //case SyntaxKind.none:
                //    token = SyntaxFactory.BadToken()

                //case SyntaxKind.EndOfLineTrivia:
                //    token = SyntaxFactory.EndOfLine(info.Text);
                //    break;

                default:
                    //token = SyntaxFactory.Token(leading, info.Kind, trailing);
                    token = SyntaxFactory.Token(info.Kind);
                    break;
            }

            //throw new NotImplementedException();

            return token;
        }

        //        private void LexSyntaxTrivia(bool afterFirstToken, bool isTrailing, ref SyntaxListBuilder triviaList)
        //        {
        //            bool onlyWhitespaceOnLine = !isTrailing;

        //            while(true)
        //            {
        //                this.Start();
        //                char ch = TextWindow.PeekChar();

        //                //out of range of UTF-7
        //                if(ch > 127)
        //                {
        //                    if (SyntaxFacts.IsNewLine(ch))
        //                    {
        //                        ch = '\n';
        //                    }
        //                }

        //                switch (ch)
        //                {
        //                    case '\r':
        //                    case '\n':
        //                        this.AddTrivia(this.ScanEndOfLine(), ref triviaList);

        //                        if(isTrailing)
        //                        {
        //                            return;
        //                        }

        //                        onlyWhitespaceOnLine = true;
        //                        break;

        //                    default:
        //                        return;
        //                }
        //            }
        //        }

        //        private void AddTrivia(SPLSyntaxNode trivia, ref SyntaxListBuilder list)
        //        {
        //            //if (this.HasErrors)
        //            //{
        //            //    trivia = trivia.WithDiagnosticsGreen(this.GetErrors(leadingTriviaWidth: 0));
        //            //}

        //            if (list == null)
        //            {
        //                list = new SyntaxListBuilder(TriviaListInitialCapacity);
        //            }

        //            list.Add(trivia);
        //        }


        //        //#########################################################################################

        //        /// <summary>
        //        /// Scans a new-line sequence (either a single new-line character or a CR-LF combo).
        //        /// </summary>
        //        /// <returns>A trivia node with the new-line text</returns>
        //        private SPLSyntaxNode ScanEndOfLine()
        //        {
        //            char ch;
        //            switch (ch= TextWindow.PeekChar())
        //            {
        //                case '\n':
        //                    TextWindow.AdvanceChar();
        //                    return SyntaxFactory.EndOfLine(ch.ToString());
        //                default:
        //                    if( SyntaxFacts.IsNewLine(ch))
        //                    {
        //                        TextWindow.AdvanceChar();
        //                        return SyntaxFactory.EndOfLine(ch.ToString());
        //                    }

        //                    return null;
        //            }
        //        }

    }
}
