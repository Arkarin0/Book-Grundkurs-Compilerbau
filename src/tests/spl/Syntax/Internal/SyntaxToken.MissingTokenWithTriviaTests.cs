using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkarin0.CodeAnalysis;
using static BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.SyntaxToken;
using BGC.SPL;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.Tests
{
    public class MissingTokenWithTriviaTests
    {
        private MissingTokenWithTrivia CreateValidInstance(SyntaxKind kind = SyntaxKind.AsteriskToken)
        {
            return new MissingTokenWithTrivia(kind, null);
        }

        [Fact()]
        public void CTorTest()
        {
            var obj = TestHelper.AssertCTorWithDiagnostics<MissingTokenWithTrivia>(hasFullWidth:false);
            var flag = GreenNode.NodeFlags.IsNotMissing;

            Assert.False((obj.Flags & flag) != 0, $"The {flag}-flag is set.");
            Assert.True(obj.IsMissing, $"The {nameof(obj.IsMissing)}-property was not set.");
        }

        [Fact()]
        public void Text_ShouldBeEmptyText()
        {
            var obj = CreateValidInstance();

            Assert.Equal(string.Empty, obj.Text);
        }

        [Theory()]
        [InlineData(SyntaxKind.IdentifierToken,"")]
        [InlineData(SyntaxKind.AsteriskToken, null)]
        public void Value_ShouldBeEmptyText( SyntaxKind kind, object expected)
        {
            var obj = CreateValidInstance(kind);

            Assert.Equal(expected, obj.Value);
        }

        [Fact()]
        public void SetDiagnosticsTest()
        {
            TestHelper.AssertSetDiagnostics(CreateValidInstance());
        }
    }
}