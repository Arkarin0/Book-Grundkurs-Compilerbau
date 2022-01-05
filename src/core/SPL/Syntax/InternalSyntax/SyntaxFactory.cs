#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    using BGC.CodeAnalysis.Syntax.InternalSyntax;

    internal static partial class SyntaxFactory
    {
        private const string CrLf = "\r\n";
        internal static readonly SyntaxTrivia CarriageReturnLineFeed = EndOfLine(CrLf);
        internal static readonly SyntaxTrivia LineFeed = EndOfLine("\n");
        internal static readonly SyntaxTrivia CarriageReturn = EndOfLine("\r");
        internal static readonly SyntaxTrivia Space = Whitespace(" ");
        internal static readonly SyntaxTrivia Tab = Whitespace("\t");



        internal static SyntaxTrivia EndOfLine(string text)
        {
            SyntaxTrivia trivia = null;

            // use predefined trivia
            switch (text)
            {
                case "\r":
                    trivia =  SyntaxFactory.CarriageReturn;
                    break;
                case "\n":
                    trivia =  SyntaxFactory.LineFeed;
                    break;
                case "\r\n":
                    trivia =  SyntaxFactory.CarriageReturnLineFeed;
                    break;
            }

            // note: predefined trivia might not yet be defined during initialization
            if (trivia != null)
            {
                return trivia;
            }

            trivia = SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, text);


            return trivia;
        }

        internal static SyntaxTrivia Whitespace(string text)
        {
            var trivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, text);
            return trivia;

        }

        //internal static SyntaxTrivia Comment(string text)
        //{
        //    if (text.StartsWith("/*", StringComparison.Ordinal))
        //    {
        //        return SyntaxTrivia.Create(SyntaxKind.MultiLineCommentTrivia, text);
        //    }
        //    else
        //    {
        //        return SyntaxTrivia.Create(SyntaxKind.SingleLineCommentTrivia, text);
        //    }
        //}



        //public static SyntaxToken Token(SyntaxKind kind)
        //{
        //    return SyntaxToken.Create(kind);
        //}

        //internal static SyntaxToken Token(GreenNode leading, SyntaxKind kind, GreenNode trailing)
        //{
        //    return SyntaxToken.Create(kind, leading, trailing);
        //}


    }
}
