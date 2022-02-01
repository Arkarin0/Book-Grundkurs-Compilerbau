#nullable disable

using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    //using BGC.CodeAnalysis.Syntax.InternalSyntax;

    internal static partial class SyntaxFactory
    {
        //        private const string CrLf = "\r\n";
        //        internal static readonly SyntaxTrivia CarriageReturnLineFeed = EndOfLine(CrLf);
        //        internal static readonly SyntaxTrivia LineFeed = EndOfLine("\n");
        //        internal static readonly SyntaxTrivia CarriageReturn = EndOfLine("\r");
        //        internal static readonly SyntaxTrivia Space = Whitespace(" ");
        //        internal static readonly SyntaxTrivia Tab = Whitespace("\t");



        //        internal static SyntaxTrivia EndOfLine(string text)
        //        {
        //            SyntaxTrivia trivia = null;

        //            // use predefined trivia
        //            switch (text)
        //            {
        //                case "\r":
        //                    trivia =  SyntaxFactory.CarriageReturn;
        //                    break;
        //                case "\n":
        //                    trivia =  SyntaxFactory.LineFeed;
        //                    break;
        //                case "\r\n":
        //                    trivia =  SyntaxFactory.CarriageReturnLineFeed;
        //                    break;
        //            }

        //            // note: predefined trivia might not yet be defined during initialization
        //            if (trivia != null)
        //            {
        //                return trivia;
        //            }

        //            trivia = SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, text);


        //            return trivia;
        //        }

        //        internal static SyntaxTrivia Whitespace(string text)
        //        {
        //            var trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, text);
        //            return trivia;

        //        }

        //        //internal static SyntaxTrivia Comment(string text)
        //        //{
        //        //    if (text.StartsWith("/*", StringComparison.Ordinal))
        //        //    {
        //        //        return SyntaxTrivia.Create(SyntaxKind.MultiLineCommentTrivia, text);
        //        //    }
        //        //    else
        //        //    {
        //        //        return SyntaxTrivia.Create(SyntaxKind.SingleLineCommentTrivia, text);
        //        //    }
        //        //}


        /// <inheritdoc cref="SyntaxToken.Create(SyntaxKind)"/>
        //public static SyntaxToken Token(GreenNode leading, SyntaxKind kind, GreenNode trailing)
        public static SyntaxToken Token(SyntaxKind kind)
        {
            //return SyntaxToken.Create(kind, leading, trailing);
            return SyntaxToken.Create(kind);
        }

        /// <inheritdoc cref="SyntaxToken.MissingToken()"/>
        internal static SyntaxToken MissingToken(SyntaxKind kind)
            => SyntaxToken.CreateMissing(kind, null, null);

        /// <inheritdoc cref="SyntaxToken.CreateMissing(SyntaxKind, GreenNode, GreenNode)"/>
        internal static SyntaxToken MissingToken(GreenNode leading, SyntaxKind kind, GreenNode trailing)
            => SyntaxToken.CreateMissing(kind, leading, trailing);


        internal static SyntaxToken Identifier(string text)
        {
            //return Identifier(SyntaxKind.IdentifierToken, null, text, text, null);
            return SyntaxToken.Identifier(text);
        }

        //internal static SyntaxToken Identifier(GreenNode leading, string text, GreenNode trailing)
        //{
        //    return Identifier(SyntaxKind.IdentifierToken, leading, text, text, trailing);
        //}

        //internal static SyntaxToken Identifier(SyntaxKind contextualKind, GreenNode leading, string text, string valueText, GreenNode trailing)
        //{
        //    return SyntaxToken.Identifier(contextualKind, leading, text, valueText, trailing);            
        //}



        internal static SyntaxToken Literal(string text, uint value)
        {
            return SyntaxToken.WithValue(SyntaxKind.NumericLiteralToken, text, value);
        }
        
        internal static SyntaxToken BadToken(string text)
            => SyntaxToken.WithValue( SyntaxKind.BadToken, text, text);





        /// <inheritdoc cref="SyntaxToken.GetWellKnownTokens()"/>
        public static IEnumerable<SyntaxToken> GetWellKnownTokens() 
            => SyntaxToken.GetWellKnownTokens();


        public static IEnumerable<SyntaxToken> ParseTokens(string text)
        {
            SyntaxToken token;
            using (var lexer = new Lexer(Microsoft.CodeAnalysis.Text.SourceText.From(text)))
            {
                token = lexer.Lex();
            }

            if(token == null) return Enumerable.Empty<SyntaxToken>();

            return new SyntaxToken[] { token };
        }

    }
}
