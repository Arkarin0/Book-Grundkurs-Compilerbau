using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        internal class MySyntaxDiagnosticInfo:SyntaxDiagnosticInfo
        {
            public MySyntaxDiagnosticInfo(int errorcode = 10) : base(null, errorcode) { }

            public MySyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int code) : base(messageProvider, code)
            {
            }

            public MySyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int code, params object[] args) : base(messageProvider, code, args)
            {
            }

            public MySyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int offset, int width, int code) : base(messageProvider, offset, width, code)
            {
            }

            public MySyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int offset, int width, int code, params object[] args) : base(messageProvider, offset, width, code, args)
            {
            }
        }

        private static DiagnosticInfo CreateDiagnosticInfo(int errorcode)
        {
            return new DiagnosticInfo(errorcode);
        }
        public static MySyntaxDiagnosticInfo CreateSyntaxDiagnosticInfo(int errorcode=10)
        {
            return new MySyntaxDiagnosticInfo(errorcode);
        }

    }
}
