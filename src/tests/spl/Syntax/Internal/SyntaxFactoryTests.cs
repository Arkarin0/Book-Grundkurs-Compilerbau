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
using BGC.SPL;

namespace BGC.CodeAnalysis.SPL.Syntax.Internal.Tests
{
    public class SyntaxFactoryTests
    {
        //[Theory]
        //[InlineData("\r")]
        //[InlineData("\n")]
        //[InlineData("\r\n")]
        //[InlineData("abc")]
        //public void EndOfLineTest(string expected)
        //{
        //    var expectedKind = SyntaxKind.EndOfLineTrivia;
        //    var trivia = SyntaxFactory.EndOfLine(expected);
        //    Assert.Equal(expected, trivia.Text);
        //    Assert.Equal(expectedKind, trivia.Kind);
        //}

        [Fact]
        public void Token_WithKindOnlyTest()
        {
            var tokens = SyntaxToken.GetWellKnownTokenKinds();

            Assert.All(tokens, token => {
                var t= SyntaxFactory.Token(token); ;

                Assert.NotNull(t);
                Assert.Equal(token, t.Kind);
            });
        }

        [Fact]
        public void GetWellKnownTokens_AllTokensArePresentTest()
        {
            var tokens = SyntaxToken.GetWellKnownTokenKinds();
            var actualList = SyntaxFactory.GetWellKnownTokens();

            Assert.All(tokens, token => {
                Assert.Single(actualList, item => item.Kind == token);                
            });
        }

        [Fact()]
        public void CreateLiteralUInt32Test()
        {
            var kind = SyntaxKind.NumericLiteralToken;
            var value = 123u;
            var text = "123";
            var obj = (SyntaxToken.SyntaxTokenWithValue<uint>)SyntaxFactory.Literal(text, value);
            TestHelper.AssertTextAndValue(obj,kind, text, value);
        }
    }
}
