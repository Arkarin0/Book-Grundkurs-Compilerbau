using Arkarin0.CodeAnalysis;
using Arkarin0.CodeAnalysis.Syntax;
using BGC.CodeAnalysis.SPL;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.Core.Parser
{
    internal partial class Lexer:AbstractLexer
    {
        private const int TriviaListInitialCapacity = 8;

        internal struct TokenInfo
        {
            internal SyntaxKind Kind;
            internal string Text;
        }

        public Lexer(SourceText text)
            :base(text)
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

        private SyntaxListBuilder _leadingTriviaCache = new SyntaxListBuilder(10);
        private SyntaxListBuilder _trailingTriviaCache = new SyntaxListBuilder(10);

        //private static int GetFullWidth(SyntaxListBuilder builder)
        //{
        //    int width = 0;

        //    if (builder != null)
        //    {
        //        for (int i = 0; i < builder.Count; i++)
        //        {
        //            width += builder[i].FullWidth;
        //        }
        //    }

        //    return width;
        //}

        private SyntaxToken LexSyntaxToken()
        {
            _leadingTriviaCache.Clear();
            this.LexSyntaxTrivia(afterFirstToken: TextWindow.Position > 0, isTrailing: false, triviaList: ref _leadingTriviaCache);
            var leading = _leadingTriviaCache;

            var tokenInfo = default(TokenInfo);

            this.Start();
            this.ScanSyntaxToken(ref tokenInfo);
            //var errors = this.GetErrors(GetFullWidth(leading));
            var errors = new object[0];

            _trailingTriviaCache.Clear();
            this.LexSyntaxTrivia(afterFirstToken: true, isTrailing: true, triviaList: ref _trailingTriviaCache);
            var trailing = _trailingTriviaCache;

            return Create(ref tokenInfo, leading, trailing, errors);
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

                default:
                    TextWindow.AdvanceChar();
                    info.Text = "noInfo";
                    break;
            }

            
        }

        private void LexComment(ref TokenInfo token, SlidingTextWindow reader)
        {
            //var text = "";
            //var c = '\0';

            //do
            //{
            //    c = reader.NextChar();
            //    text += c;

            //} while (!reader.IsReallyAtEnd() && c != '\n');

            //token.Kind = SyntaxKind.SingleLineCommentToken;
            //token.Text = text;
        }
        private void LexMultilineComment(ref TokenInfo token, SlidingTextWindow reader)
        {
            //var text = "";
            //var c = '\0';

            //do
            //{
            //    c = reader.NextChar();
            //    text += c;

            //} while (!reader.IsReallyAtEnd() && !text.EndsWith("*/"));

            //token.Kind = SyntaxKind.MultilineCommentToken;
            //token.Text = text;
        }

        //private SyntaxToken Create(ref TokenInfo info, SyntaxListBuilder leading, SyntaxListBuilder trailing, SyntaxDiagnosticInfo[] errors)
        private SyntaxToken Create(ref TokenInfo info, SyntaxListBuilder leading, SyntaxListBuilder trailing, object[] errors)
        {
            //Debug.Assert(info.Kind != SyntaxKind.IdentifierToken || info.StringValue != null);

            var leadingNode = leading?.ToListNode();
            var trailingNode = trailing?.ToListNode();

            SyntaxToken token;

            switch (info.Kind)
            {
                //case SyntaxKind.none:
                //    token = SyntaxFactory.BadToken()

                case SyntaxKind.EndOfLineTrivia:
                    token = SyntaxFactory.EndOfLine(info.Text);
                    break;

                default:
                    token = new SyntaxToken(SyntaxKind.BadToken);
                    token = SyntaxFactory.Token(leading, info.Kind, trailing);
                    break;
            }

            //throw new NotImplementedException();

            return token;
        }

        private void LexSyntaxTrivia(bool afterFirstToken, bool isTrailing, ref SyntaxListBuilder triviaList)
        {
            bool onlyWhitespaceOnLine = !isTrailing;

            while(true)
            {
                this.Start();
                char ch = TextWindow.PeekChar();

                //out of range of UTF-7
                if(ch > 127)
                {
                    if (SyntaxFacts.IsNewLine(ch))
                    {
                        ch = '\n';
                    }
                }

                switch (ch)
                {
                    case '\r':
                    case '\n':
                        this.AddTrivia(this.ScanEndOfLine(), ref triviaList);

                        if(isTrailing)
                        {
                            return;
                        }

                        onlyWhitespaceOnLine = true;
                        break;

                    default:
                        return;
                }
            }
        }

        private void AddTrivia(SPLSyntaxNode trivia, ref SyntaxListBuilder list)
        {
            //if (this.HasErrors)
            //{
            //    trivia = trivia.WithDiagnosticsGreen(this.GetErrors(leadingTriviaWidth: 0));
            //}

            if (list == null)
            {
                list = new SyntaxListBuilder(TriviaListInitialCapacity);
            }

            list.Add(trivia);
        }


        //#########################################################################################

        /// <summary>
        /// Scans a new-line sequence (either a single new-line character or a CR-LF combo).
        /// </summary>
        /// <returns>A trivia node with the new-line text</returns>
        private SPLSyntaxNode ScanEndOfLine()
        {
            char ch;
            switch (ch= TextWindow.PeekChar())
            {
                case '\n':
                    TextWindow.AdvanceChar();
                    return SyntaxFactory.EndOfLine(ch.ToString());
                default:
                    if( SyntaxFacts.IsNewLine(ch))
                    {
                        TextWindow.AdvanceChar();
                        return SyntaxFactory.EndOfLine(ch.ToString());
                    }

                    return null;
            }
        }

    }
}
