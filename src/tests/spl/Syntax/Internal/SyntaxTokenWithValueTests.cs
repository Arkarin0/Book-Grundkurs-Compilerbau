using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGC.SPL;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.Tests
{
    public class SyntaxTokenWithValueTests
    {
        private SyntaxToken.SyntaxTokenWithValue<T> CreateValidInstance<T>(T value =default)
        {
            return new SyntaxToken.SyntaxTokenWithValue<T>(SyntaxKind.IntKeyword,string.Empty,value);
        }

        [Fact()]
        public void SetDiagnosticsTest()
        {
            var obj=CreateValidInstance<uint>();
            TestHelper.AssertSetDiagnostics(obj);
        }

        [Fact()]
        public void TextAndValueAreSetAcordinglyTest()
        {
            TestCreateTokenWithValue(SyntaxKind.CharacterLiteralToken, "a", (byte)'a');
            TestCreateTokenWithValue(SyntaxKind.IntKeyword, "123", (UInt32)123);
        }

        private void TestCreateTokenWithValue<T>(SyntaxKind kind, string text, T value)
        {
            var obj = (SyntaxToken.SyntaxTokenWithValue<T>)SyntaxToken.WithValue<T>(kind, text, value);
            TestHelper.AssertTextAndValue(obj, kind, text, value);
        }
    }
}