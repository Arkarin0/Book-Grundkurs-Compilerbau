using Arkarin0.CodeAnalysis;
using BGC.CodeAnalysis.SPL;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BGC.SPL
{
    static internal partial class TestHelper
    {
        public static T AssertSetDiagnostics<T>(T node) where T : GreenNode
        {
            var obj = node;
            DiagnosticInfo diagnostic = new DiagnosticInfo(int.MaxValue);
            DiagnosticInfo[] old = obj.GetDiagnostics(), expected = new[] { diagnostic };


            var obj2 = obj.SetDiagnostics(new[] { diagnostic });

            Assert.Equal(expected,obj2.GetDiagnostics());
            Assert.NotEqual(expected, old);

            Assert.IsType<T>(obj2);

            return obj2 as T;
        }

        public static T AssertCTor<T>() where T : SPLSyntaxNode
        {
            SyntaxKind kind = SyntaxKind.OfKeyword;
            var obj = (T)Activator.CreateInstance(typeof(T),kind);
            Assert.Equal(kind, obj.Kind);

            kind = SyntaxKind.SemicolonToken;
            int length = 12;
            obj = (T)Activator.CreateInstance(typeof(T), kind, length);
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(length, obj.FullWidth);

            return obj;
        }

        public static T AssertCTorWithDiagnostics<T>() where T : SPLSyntaxNode
        {
            SyntaxKind kind = SyntaxKind.OfKeyword;
            DiagnosticInfo[] nullDiagnostics = Array.Empty<DiagnosticInfo>();
            var obj = (T)Activator.CreateInstance(typeof(T), kind, nullDiagnostics);
            Assert.Equal(kind, obj.Kind);
            Assert.NotEqual(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            kind = SyntaxKind.OpenBracketToken;
            obj = (T)Activator.CreateInstance(typeof(T), kind, new DiagnosticInfo[] { new DiagnosticInfo(10) });
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);



            kind = SyntaxKind.OpenParenToken;
            int length = 12;
            obj = (T)Activator.CreateInstance(typeof(T), kind, nullDiagnostics, length);
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(length, obj.FullWidth);
            Assert.NotEqual(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            kind = SyntaxKind.PlusToken;
            length = 13;
            obj = (T)Activator.CreateInstance(typeof(T), kind, new DiagnosticInfo[] { new DiagnosticInfo(10) }, length);
            Assert.Equal(kind, obj.Kind);
            Assert.Equal(length, obj.FullWidth);
            Assert.Equal(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            return obj;
        }
    }
}
