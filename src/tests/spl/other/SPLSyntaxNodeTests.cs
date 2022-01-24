using Xunit;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkarin0.CodeAnalysis;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.Tests
{
    public class SPLSyntaxNodeTests
    {
        internal class SPLSyntaxNode : Syntax.InternalSyntax.SPLSyntaxNode
        {
            public SPLSyntaxNode(SyntaxKind kind) : base(kind)
            {
            }

            public SPLSyntaxNode(SyntaxKind kind, int fullWidth) : base(kind, fullWidth)
            {
            }

            public SPLSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics) : base(kind, diagnostics)
            {
            }

            public SPLSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics, int fullWidth) : base(kind, diagnostics, fullWidth)
            {
            }

            public override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
            {
                throw new NotImplementedException();
            }
        }

        private SPLSyntaxNode CreateValidInstance(SyntaxKind kind= SyntaxKind.None)
        {
            return new SPLSyntaxNode(kind);
        }


        [Fact()]
        public void CtorTest()
        {
            SyntaxKind kind = SyntaxKind.OfKeyword;
            var obj = new SPLSyntaxNode(kind);
            Assert.Equal(kind, obj.Kind);

            kind = SyntaxKind.SemicolonToken;
            int length = 12;
            obj = new SPLSyntaxNode(kind, length);
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(length, obj.FullWidth);
        }



        [Fact()]
        public void Ctor_WithDiagnosticInfoTest()
        {
            SyntaxKind kind = SyntaxKind.OfKeyword;
            var obj = new SPLSyntaxNode(kind, null);
            Assert.Equal(kind, obj.Kind);
            Assert.NotEqual(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            kind = SyntaxKind.OpenBracketToken;
            obj = new SPLSyntaxNode(kind, new DiagnosticInfo[] { new DiagnosticInfo(10) });
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);



            kind = SyntaxKind.OpenParenToken;
            int length = 12;
            obj = new SPLSyntaxNode(kind, null, length);
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(length, obj.FullWidth);
            Assert.NotEqual(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            kind = SyntaxKind.PlusToken;
            length = 13;
            obj = new SPLSyntaxNode(kind, new DiagnosticInfo[] { new DiagnosticInfo(10) }, length);
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(length, obj.FullWidth);
            Assert.Equal(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);
        }

        [Theory()]
        [InlineData(SyntaxKind.EndOfLineTrivia, true)]
        [InlineData(SyntaxKind.SingleLineCommentTrivia, true)]
        [InlineData(SyntaxKind.IdentifierName, false)]
        public void IsTriviaWithEndOfLineTest(SyntaxKind kind, bool expected)
        {
            var obj = new SPLSyntaxNode(kind);

            var actual = obj.IsTriviaWithEndOfLine();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LanguageNameTest()
        {
            var text = "Simple Programming Language";
            var obj = CreateValidInstance();

            Assert.Equal(text, obj.Language);

        }

        [Theory]
        [InlineData(SyntaxKind.EndOfLineTrivia)]
        [InlineData(SyntaxKind.PlusToken)]
        [InlineData(SyntaxKind.OfKeyword)]
        public void KindTest(SyntaxKind expected)
        {            
            var obj = CreateValidInstance(expected);

            var actualKind = obj.Kind;
            var actualKindText = obj.KindText;

            Assert.Equal(expected, actualKind);
            Assert.Equal(expected.ToString(), actualKindText);

        }
    }
}