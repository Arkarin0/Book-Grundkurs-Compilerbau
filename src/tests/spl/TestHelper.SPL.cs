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

using SyntaxDiagnosticInfo = BGC.CodeAnalysis.SPL.SyntaxDiagnosticInfo;


namespace BGC.CodeAnalysis
{
    static internal partial class TestHelper
    {
        private static DiagnosticInfo CreateDiagnosticInfo(int errorcode)
        {
            return new SyntaxDiagnosticInfo((ErrorCode)errorcode);
        }
        private static DiagnosticInfo CreateDiagnosticInfo(ErrorCode errorcode)
        {
            return new SyntaxDiagnosticInfo(errorcode);
        }


        public static T AssertCTor<T>(bool hasFullWidth = true, bool isMissing=false) where T : SPLSyntaxNode
        {
            SyntaxKind kind = SyntaxKind.OfKeyword;
            var obj = CreateInstance<T>(kind);
            Assert.Equal(kind, obj.Kind);
            
            if (isMissing) AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.IsNotMissing);
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

        public static T AssertCTorWithDiagnostics<T>(bool hasFullWidth=true, bool isMissing = false) where T : SPLSyntaxNode
        {
            SyntaxKind kind = SyntaxKind.None;
            DiagnosticInfo[] emptyDiagnostics = Array.Empty<DiagnosticInfo>();
            DiagnosticInfo[] filledDiagnostics = new DiagnosticInfo[] { CreateDiagnosticInfo(10) };


            kind = SyntaxKind.OfKeyword;
            var obj = CreateInstance<T>(kind, emptyDiagnostics);
            Assert.Equal(kind, obj.Kind);
            AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            if(isMissing) AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.IsNotMissing);
            else AssertAreFlagsSet(obj, GreenNode.NodeFlags.IsNotMissing);

            kind = SyntaxKind.OpenBracketToken;
            obj = CreateInstance<T>(kind, filledDiagnostics);
            Assert.Equal(kind, obj.Kind);
            AssertAreFlagsSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            if (isMissing) AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.IsNotMissing);
            else AssertAreFlagsSet(obj, GreenNode.NodeFlags.IsNotMissing);

            if (hasFullWidth)
            {
                kind = SyntaxKind.OpenParenToken;
                int length = 12;
                obj = CreateInstance<T>(kind, length, emptyDiagnostics);
                Assert.Equal(kind, obj.Kind);
                Assert.Equal(length, obj.FullWidth);
                AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);

                kind = SyntaxKind.PlusToken;
                length = 13;
                obj = CreateInstance<T>(kind, length, filledDiagnostics);
                Assert.Equal(kind, obj.Kind);
                Assert.Equal(length, obj.FullWidth);
                AssertAreFlagsSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            }

            return obj;
        }

        /// <summary>
        /// Asserts that the <see cref="DiagnosticInfo"/>[] argument of the constructor works properly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createInstance">The create instance.</param>
        /// <exception cref="System.ArgumentNullException">createInstance</exception>
        /// <remarks>
        /// It also tests the <see cref="GreenNode.Flags"/> for <see cref="GreenNode.NodeFlags.ContainsDiagnostics"/>-flag;
        /// The <see cref="GreenNode.ContainsDiagnostics"/>-property.
        /// </remarks>
        public static void AssertCTorWithDiagnostics<T>( Func<DiagnosticInfo[],T> createInstance) where T : SPLSyntaxNode
        {
            if(createInstance == null) throw new ArgumentNullException(nameof(createInstance));

            var code = ErrorCode.ERR_IntOverflow;
            var error = CreateDiagnosticInfo(code);
            DiagnosticInfo[] emptyDiagnostics = Array.Empty<DiagnosticInfo>();
            DiagnosticInfo[] filledDiagnostics = new DiagnosticInfo[] { error };


            var obj = createInstance(emptyDiagnostics);            
            AssertAreFlagsNotSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            Assert.False(obj.ContainsDiagnostics);

            obj = createInstance(filledDiagnostics);
            AssertAreFlagsSet(obj, GreenNode.NodeFlags.ContainsDiagnostics);
            Assert.True(obj.ContainsDiagnostics);

            var diagnostics = obj.GetDiagnostics();
            Assert.True(1== diagnostics.Length,"diagnostig.length is wrong.");
            Assert.Contains(error, diagnostics);
            diagnostics.ElementAt(0).ErrorCodeEquals(code);
        }



        public static void AssertTextAndValue<T>(SyntaxToken.SyntaxTokenWithValue<T> @object, SyntaxKind kind , string text, T value)
        {
            var valueText= Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture);

            Assert.NotNull(@object);
            Assert.IsType<SyntaxToken.SyntaxTokenWithValue<T>>(@object);
            Assert.Equal(kind, @object.Kind);
            Assert.Equal(text, @object.Text);
            Assert.Equal(value, @object.Value);
            Assert.Equal(value, @object.GetValue());
            Assert.IsType<T>(@object.Value);
            Assert.Equal(valueText, @object.ValueText);
            Assert.Equal(valueText, @object.GetValueText());
        }

        public static T ErrorCodeEquals<T>(this T diagnosticInfo, ErrorCode expectedErrorCode) where T : DiagnosticInfo
        {
            Assert.Equal(expectedErrorCode, (ErrorCode)diagnosticInfo.Code);

            return diagnosticInfo;
        }

        #region SyntaxDiagnosticInfo Tests
        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, int offset, int width, ErrorCode errorCode, object[] arguments)
        {
            Assert.Equal(errorCode, (ErrorCode)@object.Code);

            CTorSyntaxDiagnosticInfo(@object, MessageProvider.Instance, offset, width, (int)errorCode, arguments);
        }

        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, int offset, int width, ErrorCode errorCode)
        => CTorSyntaxDiagnosticInfo(@object, offset, width, errorCode, Array.Empty<object>());

        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, ErrorCode errorCode, object[] arguments)
        => CTorSyntaxDiagnosticInfo(@object, 0, 0, errorCode, arguments);

        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, ErrorCode errorCode)
        => CTorSyntaxDiagnosticInfo(@object, 0, 0, errorCode);
        #endregion
    }
}
