using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BGC.CodeAnalysis.SPL;
using Microsoft.CodeAnalysis.Text;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using BGC.CodeAnalysis;

namespace BGC.Core.Lexical.Tests
{
    public class LexicalTests
    {

        //private IEnumerable<SyntaxToken> Lex(string text, CSharpParseOptions options = null)
        private IEnumerable<SyntaxToken> Lex(string text)
        {
            //return SyntaxFactory.ParseTokens(text, options: options);
            return SyntaxFactory.ParseTokens(text);
        }

        //private SyntaxToken LexToken(string text, CSharpParseOptions options = null)
        private SyntaxToken LexToken(string text)
        {
            SyntaxToken result = default(SyntaxToken);
            //foreach (var token in Lex(text, options))
            foreach (var token in Lex(text))
            {
                if (result.Kind() == SyntaxKind.None)
                {
                    result = token;
                }
                else if (token.Kind() == SyntaxKind.EndOfFileToken)
                {
                    continue;
                }
                else
                {
                    Assert.True(false, "More than one token was lexed: " + token);
                }
            }
            if (result.Kind() == SyntaxKind.None)
            {
                Assert.True(false, "No tokens were lexed");
            }
            return result;
        }

        [Fact]
        [Trait("Feature", "Keywords")]
        public void TestAllLanguageKeywords()
        {
            foreach (var keyword in SyntaxFacts.GetKeywordKinds())
            {
                if (SyntaxFacts.IsKeyword(keyword))
                {
                    var text = SyntaxFacts.GetText(keyword);
                    var token = LexToken(text);

                    Assert.NotEqual(default, token);
                    Assert.True(SyntaxFacts.IsKeyword(token.Kind()),$"IsKeyword(): failed\nKeyword:{keyword}\nText:\"{text}\"");
                    Assert.Equal(text, token.Text);
                    var errors = token.Errors();
                    Assert.Equal(0, errors.Length);
                    Assert.Equal(text, token.ValueText);
                }
            }
        }

        [Fact]
        [Trait("Feature", "Punctuation")]
        public void TestAllLanguagePunctuation()
        {
            //punctuation
            TestPunctuation(SyntaxKind.PlusToken);
            TestPunctuation(SyntaxKind.MinusToken);
            TestPunctuation(SyntaxKind.AsteriskToken);
            TestPunctuation(SyntaxKind.SlashToken);
            TestPunctuation(SyntaxKind.ColonToken);
            TestPunctuation(SyntaxKind.SemicolonToken);
            TestPunctuation(SyntaxKind.CommaToken);
            TestPunctuation(SyntaxKind.LessThanToken);
            TestPunctuation(SyntaxKind.GreaterThanToken);            
            TestPunctuation(SyntaxKind.EqualsToken);
            TestPunctuation(SyntaxKind.HashToken);
            TestPunctuation(SyntaxKind.OpenParenToken);
            TestPunctuation(SyntaxKind.CloseParenToken);
            TestPunctuation(SyntaxKind.OpenBraceToken);
            TestPunctuation(SyntaxKind.CloseBraceToken);
            TestPunctuation(SyntaxKind.OpenBracketToken);
            TestPunctuation(SyntaxKind.CloseBracketToken);
            //TestPunctuation(SyntaxKind.SingleQuoteToken);// literal Character
            TestPunctuation(SyntaxKind.UnderscoreToken);
            
            //compound punctuation
            TestPunctuation(SyntaxKind.ColonEqualToken);
            TestPunctuation(SyntaxKind.LessThenEqualToken);
            TestPunctuation(SyntaxKind.GreaterThenEqualToken);
        }
        private void TestPunctuation(SyntaxKind kind)
        {
            var text = SyntaxFacts.GetText(kind);
            var token = LexToken(text);

            Assert.NotEqual(default, token);
            Assert.Equal(kind, token.Kind());
            Assert.Equal(text, token.Text);
            var errors = token.Errors();
            Assert.Equal(0, errors.Length);
            Assert.Equal(text, token.ValueText);
        }

        [Fact]
        [Trait("Feature", "Literals")]
        public void TestInvalidCharacterLiteral()
        {
            var value = "@";
            var text = value;
            var token = LexToken(text);

            Assert.NotEqual(default, token);
            Assert.Equal(SyntaxKind.BadToken, token.Kind());
            Assert.Equal(text, token.Text);
            var errors = token.Errors();
            Assert.Equal(1, errors.Length);
            Assert.Equal(value, token.ValueText);
        }

        //[Fact]
        //[Trait("Feature", "Literals")]
        //public void TestCharacterLiteral()
        //{
        //    var value = "x";
        //    var text = "'" + value + "'";
        //    var token = LexToken(text);

        //    Assert.NotEqual(default, token);
        //    Assert.Equal(SyntaxKind.CharacterLiteralToken, token.Kind());
        //    Assert.Equal(text, token.Text);
        //    var errors = token.Errors();
        //    Assert.Equal(0, errors.Length);
        //    Assert.Equal(value, token.ValueText);
        //}

        //[Fact]
        //[Trait("Feature", "Literals")]
        //public void TestCharacterLiteralEscape_R()
        //{
        //    var value = "\r";
        //    var text = "'\\r'";
        //    var token = LexToken(text);

        //    Assert.NotEqual(default, token);
        //    Assert.Equal(SyntaxKind.CharacterLiteralToken, token.Kind());
        //    Assert.Equal(text, token.Text);
        //    var errors = token.Errors();
        //    Assert.Equal(0, errors.Length);
        //    Assert.Equal(value, token.ValueText);
        //}


        [Fact]
        [Trait("Feature", "Literals")]
        public void TestNumericLiteral()
        {
            var value = 123u;
            var text = "123";
            var token = LexToken(text);

            Assert.NotEqual(default, token);
            Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind());
            var errors = token.Errors();
            Assert.Equal(0, errors.Length);
            Assert.Equal(text, token.Text);
            Assert.Equal(value, token.Value);
        }

        [Theory]
        [Trait("Feature", "Literals")]
        [InlineData("0x123",0x123u)]
        [InlineData("0xA", 0xA)]
        [InlineData("0xa", 0xa)]
        [InlineData("0xB", 0xB)]
        [InlineData("0xb", 0xb)]
        [InlineData("0xC", 0xC)]
        [InlineData("0xc", 0xc)]
        [InlineData("0xD", 0xD)]
        [InlineData("0xd", 0xd)]
        [InlineData("0xE", 0xE)]
        [InlineData("0xe", 0xe)]
        [InlineData("0xF", 0xF)]
        [InlineData("0xf", 0xF)]
        public void TestHexNumericLiteral(string text, uint value)
        {
            var token = LexToken(text);

            Assert.NotEqual(default, token);
            Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind());
            var errors = token.Errors();
            Assert.Equal(0, errors.Length);
            Assert.Equal(text, token.Text);
            Assert.Equal(value, token.Value);
        }


        [Fact]
        [Trait("Feature", "Literals")]
        public void TestNumericLiteral_UInt32_ErrIntOverflow()
        {
            var value = 0u;
            var text = uint.MaxValue.ToString() + "1";
            var token = LexToken(text);

            Assert.NotEqual(default, token);
            Assert.Equal(SyntaxKind.NumericLiteralToken, token.Kind());
            var errors = token.Errors();
            Assert.NotEqual(0, errors.Length);
            Assert.Equal(text, token.Text);
            Assert.Equal(value, token.Value);

            var error = errors.ElementAtOrDefault(0);
            error.ErrorCodeEquals(ErrorCode.ERR_IntOverflow);
        }
    }
}