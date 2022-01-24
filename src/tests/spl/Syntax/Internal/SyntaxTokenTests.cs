using Xunit;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkarin0.CodeAnalysis;
using BGC.SPL;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.Tests
{
    public class SyntaxTokenTests
    {
        private SyntaxToken CreateValidInstance(SyntaxKind kind= SyntaxKind.AsteriskToken)
        {
            return new SyntaxToken(kind);
        }

        [Fact()]
        public void CtorTest()
        {
            TestHelper.AssertCTor<SyntaxToken>();
        }



        [Fact()]
        public void Ctor_WithDiagnosticInfoTest()
        {            
            TestHelper.AssertCTorWithDiagnostics<SyntaxToken>();
        }

        [Fact()]
        public void ToStringTest()
        {
            var expected = "*";
            var obj = CreateValidInstance(SyntaxKind.AsteriskToken);

            Assert.Equal(expected, obj.ToString());
        }

        [Fact()]
        public void ValueTest()
        {
            var expected = "*";
            var obj = CreateValidInstance(SyntaxKind.AsteriskToken);

            Assert.Equal(expected, obj.Value);
        }

        [Fact()]
        public void TextTest()
        {
            var expected = "*";
            var obj = CreateValidInstance(SyntaxKind.AsteriskToken);

            Assert.Equal(expected, obj.Text);
        }

        [Fact()]
        public void SetDiagnosticsTest()
        {
            TestHelper.AssertSetDiagnostics(CreateValidInstance());
        }

        [Fact()]
        public void Create_TokenIsKnownTest()
        {
            var expectedKind= SyntaxKind.AsteriskToken;

            var obj= SyntaxToken.Create(expectedKind);

            Assert.IsType<SyntaxToken>(obj);
            Assert.Equal(expectedKind, obj.Kind);
        }

        [Theory()]
        [InlineData(SyntaxKind.IdentifierToken)]
        public void Create_ReturnsTokenIsMissingTest(SyntaxKind expectedKind)
        {
            //any token after EndOfFileToken should result in a MissingToken.

            var obj = SyntaxToken.Create(expectedKind);

            Assert.IsType<SyntaxToken.MissingTokenWithTrivia>(obj);
            Assert.True(obj.IsMissing);
            Assert.Equal(expectedKind, obj.Kind);
        }

        [Theory()]
        [InlineData(SyntaxKind.AsteriskToken)]
        [InlineData(SyntaxKind.ElseKeyword)]
        public void FullWidthTest(SyntaxKind expectedKind)
        {
            var expectedText = SyntaxFacts.GetText(expectedKind);
            var expectedwidth = expectedText.Length;

            var obj = SyntaxToken.Create(expectedKind);
            
            Assert.NotEmpty(obj.Text);
            Assert.Equal(expectedText, obj.Text);
            Assert.Equal(expectedwidth, obj.FullWidth);
        }

    }
}