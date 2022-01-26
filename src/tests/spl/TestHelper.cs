using Arkarin0.CodeAnalysis;
using BGC.CodeAnalysis.SPL;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BGC.SPL
{
    static internal partial class TestHelper
    {
#nullable enable
        private static T CreateInstance<T>(params object?[]? args)
#nullable disable
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            
            return (T)Activator.CreateInstance(typeof(T),flags, binder:null,args, culture:null);
        }


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

        public static T AssertCTor<T>(bool hasFullWidth = true) where T : SPLSyntaxNode
        {
            bool isMissingToken = typeof(T).IsAssignableTo(typeof(SyntaxToken.MissingTokenWithTrivia));
            bool isSyntaxToken = typeof(T).IsAssignableFrom(typeof(SyntaxToken));

            SyntaxKind kind = SyntaxKind.OfKeyword;
            var obj = CreateInstance<T>(kind);
            Assert.Equal(kind, obj.Kind);
            if (isMissingToken) AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.IsNotMissing);
            else AssertAreFlagsSet(obj, GreenNode.NodeFlags.IsNotMissing);


            if (hasFullWidth)
            {
                kind = SyntaxKind.SemicolonToken;
                int length = 12;
                obj = CreateInstance<T>(kind, length);
                Assert.Equal(kind, obj.Kind);
                Assert.Equal(length, obj.FullWidth);
            }

            return obj;
        }

        public static T AssertCTorWithDiagnostics<T>(bool hasFullWidth=true) where T : SPLSyntaxNode
        {
            bool isMissingToken = typeof(T).IsAssignableTo(typeof(SyntaxToken.MissingTokenWithTrivia));
            bool isSyntaxToken= typeof(T).IsAssignableTo(typeof(SyntaxToken));

            SyntaxKind kind = SyntaxKind.OfKeyword;
            DiagnosticInfo[] nullDiagnostics = Array.Empty<DiagnosticInfo>();
            var obj = CreateInstance<T>(kind, nullDiagnostics);
            Assert.Equal(kind, obj.Kind);
            AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            if(isMissingToken) AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.IsNotMissing);
            else AssertAreFlagsSet(obj, GreenNode.NodeFlags.IsNotMissing);

            kind = SyntaxKind.OpenBracketToken;
            obj = CreateInstance<T>(kind, new DiagnosticInfo[] { new DiagnosticInfo(10) });
            Assert.Equal(kind, obj.Kind);
            AssertAreFlagsSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            if (isMissingToken) AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.IsNotMissing);
            else AssertAreFlagsSet(obj, GreenNode.NodeFlags.IsNotMissing);

            if (hasFullWidth)
            {
                kind = SyntaxKind.OpenParenToken;
                int length = 12;
                obj = CreateInstance<T>(kind, length, nullDiagnostics);
                Assert.Equal(kind, obj.Kind);
                Assert.Equal(length, obj.FullWidth);
                AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);

                kind = SyntaxKind.PlusToken;
                length = 13;
                obj = CreateInstance<T>(kind, length, new DiagnosticInfo[] { new DiagnosticInfo(10) });
                Assert.Equal(kind, obj.Kind);
                Assert.Equal(length, obj.FullWidth);
                AssertAreFlagsSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            }

            return obj;
        }

        public static void AssertAreFlagsSet(GreenNode node, GreenNode.NodeFlags flags)
        {
            var actual = node.Flags.HasFlag(flags);
            Assert.True(actual, $"The {flags}-flag(s) are not set.");
        }
        public static void AssertAreFlagsNotSet(GreenNode node, GreenNode.NodeFlags flags)
        {
            var actual = !node.Flags.HasFlag(flags);
            Assert.True(actual, $"The {flags}-flag(s) are set.");
        }
    }
}
