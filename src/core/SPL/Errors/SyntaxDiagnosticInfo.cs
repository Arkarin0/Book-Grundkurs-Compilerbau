using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal class SyntaxDiagnosticInfo : Arkarin0.CodeAnalysis.SyntaxDiagnosticInfo
    {
        public SyntaxDiagnosticInfo(ErrorCode code) : this(0,0, code)
        {
        }

        public SyntaxDiagnosticInfo(ErrorCode code, params object[] args) : this(0,0, code, args)
        {
        }

        public SyntaxDiagnosticInfo(int offset, int width, ErrorCode code) : this(offset, width, code, Array.Empty<object>())
        {
        }

        public SyntaxDiagnosticInfo(int offset, int width, ErrorCode code, params object[] args) : base(SPL.MessageProvider.Instance, offset, width, (int)code, args)
        {
        }
    }
}
