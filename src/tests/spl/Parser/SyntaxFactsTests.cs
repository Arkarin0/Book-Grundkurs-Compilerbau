using Xunit;
using BGC.CodeAnalysis.SPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;

namespace BGC.CodeAnalysis.SPL.Tests
{
    public class SyntaxFactsTests
    {
        private SyntaxKind[] GetKindsFromTo(SyntaxKind from, SyntaxKind to)
        {
            var list = new List<SyntaxKind>();
            for (int kind = (int)from; kind <= (int)to; kind++)
                list.Add((SyntaxKind)kind);

            return list.ToArray();
        }

        [Fact()]
        public void IsPunctuationTest()
        {
            Assert.All(
                SyntaxFacts.GetPunctuationKinds(),
                kind => Assert.True(SyntaxFacts.IsPunctuation(kind))
                );
        }

        [Fact()]
        public void GetKeywordKindsTest()
        {
            var expected = GetKindsFromTo(SyntaxKind.ArrayKeyword, SyntaxKind.IntKeyword);

            var actual = SyntaxFacts.GetKeywordKinds().ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void IsKeywordTest()
        {
            Assert.All(
                SyntaxFacts.GetKeywordKinds(),
                kind => Assert.True(SyntaxFacts.IsKeyword(kind))
                );
        }

        [Fact()]
        public void GetPunctuationKindsTest()
        {
            var expected = GetKindsFromTo(SyntaxKind.PlusToken, SyntaxKind.GreaterThenEqualToken);

            var actual = SyntaxFacts.GetPunctuationKinds().ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void IsPunctuationOrKeywordTest()
        {
            Assert.All(
                SyntaxFacts.GetPunctuationKinds(),
                kind=> Assert.True(SyntaxFacts.IsPunctuationOrKeyword(kind))
                );

            Assert.All(
                SyntaxFacts.GetKeywordKinds(),
                kind => Assert.True(SyntaxFacts.IsPunctuationOrKeyword(kind))
                );
        }

        [Fact()]
        public void IsLiteralTest()
        {
            var list = new[]
            {
                SyntaxKind.IdentifierToken,
                SyntaxKind.CharacterLiteralToken,
                SyntaxKind.NumericLiteralToken,
            };

            Assert.All(
                list,
                kind => Assert.True(SyntaxFacts.IsLiteral(kind))
                );
        }

        [Fact()]
        public void IsPredefinedTypeTest()
        {
            var list = new[]
            {
                SyntaxKind.IntKeyword,
            };

            Assert.All(
                list,
                kind => Assert.True(SyntaxFacts.IsPredefinedType(kind))
                );
        }

        [Fact()]
        public void GetTextTest()
        {
            //punctuation
            GetTextTestHelper(SyntaxKind.PlusToken, "+");
            GetTextTestHelper(SyntaxKind.MinusToken, "-");
            GetTextTestHelper(SyntaxKind.AsteriskToken, "*");
            GetTextTestHelper(SyntaxKind.SlashToken, "/");
            GetTextTestHelper(SyntaxKind.ColonToken, ":");
            GetTextTestHelper(SyntaxKind.SemicolonToken, ";");
            GetTextTestHelper(SyntaxKind.CommaToken, ",");
            GetTextTestHelper(SyntaxKind.LessThanToken, "<");
            GetTextTestHelper(SyntaxKind.GreaterThanToken, ">");
            GetTextTestHelper(SyntaxKind.EqualsToken, "=");
            GetTextTestHelper(SyntaxKind.HashToken, "#");
            GetTextTestHelper(SyntaxKind.OpenParenToken, "(");
            GetTextTestHelper(SyntaxKind.CloseParenToken, ")");
            GetTextTestHelper(SyntaxKind.OpenBraceToken, "{");
            GetTextTestHelper(SyntaxKind.CloseBraceToken, "}");
            GetTextTestHelper(SyntaxKind.OpenBracketToken, "[");
            GetTextTestHelper(SyntaxKind.CloseBracketToken, "]");
            GetTextTestHelper(SyntaxKind.SingleQuoteToken, "'");
            GetTextTestHelper(SyntaxKind.UnderscoreToken, "_");
            //compound punctuation
            GetTextTestHelper(SyntaxKind.ColonEqualToken, ":=");
            GetTextTestHelper(SyntaxKind.LessThenEqualToken, "<=");
            GetTextTestHelper(SyntaxKind.GreaterThenEqualToken, ">=");
            //keywords
            GetTextTestHelper(SyntaxKind.ArrayKeyword, "array");
            GetTextTestHelper(SyntaxKind.IfKeyword, "if");
            GetTextTestHelper(SyntaxKind.ElseKeyword, "else");
            GetTextTestHelper(SyntaxKind.WhileKeyword, "while");
            GetTextTestHelper(SyntaxKind.ProcedureKeyword, "proc");
            GetTextTestHelper(SyntaxKind.VarKeyword, "var");
            GetTextTestHelper(SyntaxKind.RefKeyword, "ref");
            GetTextTestHelper(SyntaxKind.OfKeyword, "of");
            GetTextTestHelper(SyntaxKind.IntKeyword, "int");
        }
        private void GetTextTestHelper(SyntaxKind kind, string expected)
        {
            var text = SyntaxFacts.GetText(kind);
            Assert.Equal(expected, text);
        }
    }
}