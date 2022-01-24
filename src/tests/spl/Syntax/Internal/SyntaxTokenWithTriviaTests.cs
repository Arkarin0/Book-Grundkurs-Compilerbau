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
    public class SyntaxTokenWithTriviaTests
    {
        private SyntaxTokenWithTrivia CreateValidInstance(SyntaxKind kind = SyntaxKind.AsteriskToken)
        {
            return new SyntaxTokenWithTrivia(kind, null);
        }

        [Fact()]
        public void CTorTest()
        {
            TestHelper.AssertCTorWithDiagnostics<SyntaxTokenWithTrivia>(hasWidth:false);
        }

        [Fact()]
        public void SetDiagnosticsTest()
        {
            TestHelper.AssertSetDiagnostics(CreateValidInstance());
        }
    }
}