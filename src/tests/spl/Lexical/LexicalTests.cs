using Xunit;
using BGC.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BGC.CodeAnalysis.SPL;
using Microsoft.CodeAnalysis.Text;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;

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
                    Assert.True(SyntaxFacts.IsKeyword(token.Kind()));
                    Assert.Equal(text, token.Text);
                    var errors = token.Errors();
                    Assert.Equal(0, errors.Length);
                    //Assert.Equal(text, token.ValueText);//TODO: enable
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
            TestPunctuation(SyntaxKind.SingleQuoteToken);// literal Character
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
            //TODO: enable
            //Assert.Equal(text, token.ValueText);
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


        //        [Fact()]
        //        public void LexSingleLineCommentTest()
        //        {
        //            var kind = SyntaxKind.SingleLineCommentTrivia;
        //            LexCommentTestHelper(kind, "//abc");
        //            LexCommentTestHelper(kind, "//abcdef//fgh");
        //            LexCommentTestHelper(kind, "//abcdef     //fgh");
        //            LexCommentTestHelper(kind, "//abcdef\r//fgh");
        //            LexCommentTestHelper(kind, "//abcdef\n", "//ghij");
        //            LexCommentTestHelper(kind, "//abcdef\n", "//ghij\r\n");

        //        }
        //        [Fact()]
        //        public void LexMultiLineCommentTest()
        //        {
        //            var kind = SyntaxKind.MultiLineCommentTrivia;


        //            LexCommentTestHelper(kind, "/*abc*/");
        //            LexCommentTestHelper(kind, "/*abcdef  def//fgh*/");
        //            LexCommentTestHelper(kind, "/*abcdef\r\nfgh*/");
        //            LexCommentTestHelper(kind, "/*abcdef\r\n//fgh*/");
        //            LexCommentTestHelper(kind, "/*abcdef\n*/", "/*ghij*/");


        //        }
        //        private void LexCommentTestHelper(SyntaxKind kind, params string[] text)
        //        {
        //            string expected = string.Join("", text);
        //            var lexer = this.CreateValidInstance(expected);
        //            var actual = lexer.Lex();

        //            int index = 0;
        //            //Assert.All(actual, (item) =>
        //            //{
        //            //    var expectedText = text[index++];
        //            //    Assert.Equal(expectedText, item.ValueText);
        //            //    Assert.Equal(kind, item.Kind);
        //            //});
        //        }

        //        [Theory()]
        //        [InlineData("\r")]
        //        [InlineData("\n")]
        //        [InlineData("\r\n")]
        //        [InlineData("\u0085")]
        //        [InlineData("\u2028")]
        //        [InlineData("\u2029")]        
        //        public void LexEndOfLineTriviaTest(string text)
        //        {

        //            var actualToken = Lex(text);

        //            Assert.Equal(SyntaxKind.EndOfLineTrivia, actualToken.Kind);
        //            Assert.Equal(text, actualToken.ToString());

        //        }
    }
}