using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.Tests
{
    public class SyntaxIdentifierTests
    {
        [Fact]
        public void CTorTest()
        {
            var text = "abcde";
            var obj= new SyntaxToken.SyntaxIdentifier(text);

            TestHelper.AssertCTorWithDiagnostics(diagnostics => new SyntaxToken.SyntaxIdentifier(text, diagnostics));

            Assert.Equal(SyntaxKind.IdentifierToken, obj.Kind());
            Assert.Equal(text, obj.Text);
            Assert.Equal(text, obj.Value);
            Assert.Equal(text, obj.ValueText);
            Assert.Equal(text.Length, obj.ValueText.Length);
        }


        [Fact()]
        public void SetDiagnosticsTest()
        {
            var obj = new SyntaxToken.SyntaxIdentifier("abc");
            TestHelper.AssertSetDiagnostics(obj);
        }
    }
}