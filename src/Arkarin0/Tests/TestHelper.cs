using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

#if SDK
namespace Arkarin0.CodeAnalysis
#else
namespace BGC.CodeAnalysis
#endif
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
            DiagnosticInfo diagnostic = CreateDiagnosticInfo(int.MaxValue);
            DiagnosticInfo[] old = obj.GetDiagnostics(), expected = new[] { diagnostic };


            var obj2 = obj.SetDiagnostics(new[] { diagnostic });

            Assert.Equal(expected,obj2.GetDiagnostics());
            Assert.NotEqual(expected, old);

            Assert.IsType<T>(obj2);

            return obj2 as T;
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

        public static void AssertUnreachableExceptionIsThrown(Action testCode)
        {
            var text = "This program location is thought to be unreachable.";
            var ex = Assert.Throws<InvalidOperationException>(testCode);
            Assert.True(ex.Message == text, "The exception message is wrong.");
        }
        public static void AssertUnexpectedValueExceptionIsThrown(Action testCode)
        {
            //var text = "Unexpected value '{0}' of type '{1}'";
            var text = @"/Unexpected value (?:'[\w|.]*') of type (?:'[\w|.]*')$/gm";
            var ex = Assert.Throws<InvalidOperationException>(testCode);
            Assert.True(Regex.IsMatch(ex.Message, text), "The exception message is wrong.");
        }

        #region DiagnosticInfo Tests
        public static T ErrorCodeEquals<T>(this T diagnosticInfo, int expectedErrorCode) where T : DiagnosticInfo
        {
            Assert.Equal(expectedErrorCode,diagnosticInfo.Code);

            return diagnosticInfo;
        }

        public static void CTorDiagnosticInfo(DiagnosticInfo @object, CommonMessageProvider messageProvider, int errorCode)
        => CTorDiagnosticInfo(@object, messageProvider, errorCode, Array.Empty<object>());

        public static void CTorDiagnosticInfo(DiagnosticInfo @object, CommonMessageProvider messageProvider, int errorCode, object[] arguments)
        {
            Assert.NotNull(@object);
            Assert.Equal(messageProvider, @object.MessageProvider);
            @object.ErrorCodeEquals(errorCode);
            Assert.Equal(arguments, @object.Arguments);
        }
        #endregion

        #region SyntaxDiagnosticInfo Tests
        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, CommonMessageProvider messageProvider,int offset, int width, int errorCode, object[] arguments)
        {
            Assert.Equal(offset, @object.Offset);
            Assert.Equal(width, @object.Width);

            CTorDiagnosticInfo(@object, messageProvider, errorCode, arguments);
        }

        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, CommonMessageProvider messageProvider, int offset, int width, int errorCode)
        => CTorSyntaxDiagnosticInfo(@object, messageProvider, offset,width, errorCode, Array.Empty<object>());

        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, CommonMessageProvider messageProvider, int errorCode, object[] arguments)
        => CTorSyntaxDiagnosticInfo(@object, messageProvider, 0, 0, errorCode, arguments);

        public static void CTorSyntaxDiagnosticInfo(SyntaxDiagnosticInfo @object, CommonMessageProvider messageProvider, int errorCode)
        => CTorSyntaxDiagnosticInfo(@object, messageProvider, 0, 0, errorCode);
        #endregion

    }
}
