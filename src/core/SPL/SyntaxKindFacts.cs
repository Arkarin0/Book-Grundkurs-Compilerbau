using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    public static partial class SyntaxFacts
    {
        public static readonly SyntaxKind FirstTokenKind = SyntaxKind.PlusToken;
        public static readonly SyntaxKind LastTokenKind = SyntaxKind.EndOfLineTrivia;


        /// <summary>
        /// Returns true if the Unicode character is a newline character.
        /// </summary>
        /// <param name="ch">The Unicode character.</param>
        public static bool IsNewLine(char ch)
        {
            // new-line-character:
            //   Carriage return character (U+000D)
            //   Line feed character (U+000A)
            //   Next line character (U+0085)
            //   Line separator character (U+2028)
            //   Paragraph separator character (U+2029)

            return ch == '\r'
                || ch == '\n'
                || ch == '\u0085'
                || ch == '\u2028'
                || ch == '\u2029';
        }

        /// <summary>
        /// Determine if a given <see cref="SyntaxKind"/> is a punctuation.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to check.</param>
        /// <returns><see langword="true"/> when <paramref name="kind"/> is a punctuation; else <see langword="false"/>.</returns>
        public static bool IsPunctuation(SyntaxKind kind)
        {
            return kind >= SyntaxKind.PlusToken && kind <= SyntaxKind.GreaterThenEqualToken;
        }

        /// <summary>
        /// Get all <see cref="SyntaxKind"/>s which are a punctuation.
        /// </summary>
        public static IEnumerable<SyntaxKind> GetPunctuationKinds()
        {
            for (int i = (int)SyntaxKind.PlusToken; i <= (int)SyntaxKind.GreaterThenEqualToken; i++)
            {
                yield return (SyntaxKind)i;
            }
        }

        /// <summary>
        /// Determine if a given <see cref="SyntaxKind"/> is a keyword.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to check.</param>
        /// <returns><see langword="true"/> when <paramref name="kind"/> is a keyword; else <see langword="false"/>.</returns>
        public static bool IsKeyword(SyntaxKind kind)
        {
            return kind >= SyntaxKind.ArrayKeyword && kind <= SyntaxKind.IntKeyword;            
        }

        /// <summary>
        /// Get all <see cref="SyntaxKind"/>s which are keywords.
        /// </summary>
        public static IEnumerable<SyntaxKind> GetKeywordKinds()
        {
            for (int i = (int)SyntaxKind.ArrayKeyword; i <= (int)SyntaxKind.IntKeyword; i++)
            {
                yield return (SyntaxKind)i;
            }
        }

        /// <summary>
        /// Determine if a given <see cref="SyntaxKind"/> is a punctuation or keyword.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to check.</param>
        /// <returns><see langword="true"/> when <paramref name="kind"/> is a punctuation or keyword; else <see langword="false"/>.</returns>
        public static bool IsPunctuationOrKeyword(SyntaxKind kind)
        {
            return kind >= SyntaxToken.FirstTokenWithWellKnownText && kind <= SyntaxToken.LastTokenWithWellKnownText;
        }

        /// <summary>
        /// Determine if a given <see cref="SyntaxKind"/> is a literal token.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to check.</param>
        /// <returns><see langword="true"/> when <paramref name="kind"/> is a literal token; else <see langword="false"/>.</returns>
        internal static bool IsLiteral(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.IdentifierToken:
                case SyntaxKind.CharacterLiteralToken:
                case SyntaxKind.NumericLiteralToken:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determine if a given <see cref="SyntaxKind"/> is a token.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to check.</param>
        /// <returns><see langword="true"/> when <paramref name="kind"/> is a token; else <see langword="false"/>.</returns>
        public static bool IsAnyToken(SyntaxKind kind)
        {
            if (kind >= FirstTokenKind && kind < LastTokenKind) return true;
            switch (kind)
            {
                case SyntaxKind.UnderscoreToken:
                    return true;
                default:
                    return false;
            }
        }

        //public static bool IsTrivia(SyntaxKind kind)
        //{
        //    switch (kind)
        //    {
        //        case SyntaxKind.EndOfLineTrivia:
        //        case SyntaxKind.WhitespaceTrivia:
        //        case SyntaxKind.SingleLineCommentTrivia:
        //        case SyntaxKind.MultiLineCommentTrivia:
        //            return true;
        //        default:
        //            return false;
        //    }
        //}

        //public static bool IsName(SyntaxKind kind)
        //{
        //    switch (kind)
        //    {
        //        case SyntaxKind.IdentifierName:
        //        case SyntaxKind.GenericName:
        //        case SyntaxKind.QualifiedName:
        //        case SyntaxKind.AliasQualifiedName:
        //            return true;
        //        default:
        //            return false;
        //    }
        //}

        /// <summary>
        /// Determine if a given <see cref="SyntaxKind"/> is a predfined type.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to check.</param>
        /// <returns><see langword="true"/> when <paramref name="kind"/> is a predefined type; else <see langword="false"/>.</returns>
        public static bool IsPredefinedType(SyntaxKind kind)
        {
            switch (kind)
            {                
                case SyntaxKind.IntKeyword:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Turn a text into a <c>keyword</c>-<see cref="SyntaxKind"/>.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>Returns a <c>keyword</c>-<see cref="SyntaxKind"/>; else <see cref="SyntaxKind.None"/>.</returns>
        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "array": return SyntaxKind.ArrayKeyword;
                case "if": return SyntaxKind.IfKeyword;
                case "else": return SyntaxKind.ElseKeyword;
                case "while": return SyntaxKind.WhileKeyword;
                case "proc": return SyntaxKind.ProcedureKeyword;
                case "var": return SyntaxKind.VarKeyword;
                case "ref": return SyntaxKind.RefKeyword;
                case "of": return SyntaxKind.OfKeyword;
                case "int": return SyntaxKind.IntKeyword;
                default: return SyntaxKind.None;
            }
        }

        /// <summary>
        /// Turn a <see cref="SyntaxKind"/> into its <see cref="string"/> representation.
        /// </summary>
        /// <param name="kind">The <see cref="SyntaxKind"/> to convert.</param>
        /// <returns>Returns a <see cref="string"/> if the <paramref name="kind"/> exists; else <see cref="string.Empty"/>.</returns>
        public static string GetText(SyntaxKind kind)
        {
            switch (kind)
            {
                //punctuation
                case SyntaxKind.PlusToken:
                    return "+";
                case SyntaxKind.MinusToken:
                    return "-";
                case SyntaxKind.AsteriskToken:
                    return "*";
                case SyntaxKind.SlashToken:
                    return "/";
                case SyntaxKind.ColonToken:
                    return ":";
                case SyntaxKind.SemicolonToken:
                    return ";";
                case SyntaxKind.CommaToken:
                    return ",";
                case SyntaxKind.LessThanToken:
                    return "<";
                case SyntaxKind.GreaterThanToken:
                    return ">";
                case SyntaxKind.EqualsToken:
                    return "=";
                case SyntaxKind.HashToken:
                    return "#";
                case SyntaxKind.OpenParenToken:
                    return "(";
                case SyntaxKind.CloseParenToken:
                    return ")";
                case SyntaxKind.OpenBraceToken:
                    return "{";
                case SyntaxKind.CloseBraceToken:
                    return "}";
                case SyntaxKind.OpenBracketToken:
                    return "[";
                case SyntaxKind.CloseBracketToken:
                    return "]";
                case SyntaxKind.SingleQuoteToken:
                    return "'";
                case SyntaxKind.UnderscoreToken:
                    return "_";
                //compound punctuation
                case SyntaxKind.ColonEqualToken:
                    return ":=";
                case SyntaxKind.LessThenEqualToken:
                    return "<=";
                case SyntaxKind.GreaterThenEqualToken:
                    return ">=";

                //keywords
                case SyntaxKind.ArrayKeyword:
                    return "array";
                case SyntaxKind.IfKeyword:
                    return "if";
                case SyntaxKind.ElseKeyword:
                    return "else";
                case SyntaxKind.WhileKeyword:
                    return "while";
                case SyntaxKind.ProcedureKeyword:
                    return "proc";
                case SyntaxKind.VarKeyword:
                    return "var";
                case SyntaxKind.RefKeyword:
                    return "ref";
                case SyntaxKind.OfKeyword:
                    return "of";
                case SyntaxKind.IntKeyword:
                    return "int";
                default:
                    return string.Empty;
            }
        }


    }
}
