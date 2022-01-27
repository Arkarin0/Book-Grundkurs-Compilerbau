using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis
{
    public abstract class SyntaxDiagnosticInfo : DiagnosticInfo
    {

        protected readonly int _Offset;
        protected readonly int _Width;

        protected SyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int offset, int width, int code, object[] args)
            : base( messageProvider, (int)code, args)
        {
            Debug.Assert(width >= 0);
            this._Offset = offset;
            this._Width = width;
        }

        protected SyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int offset, int width, int code)
            : this(messageProvider,offset, width, code, Array.Empty<object>())
        {
        }

        protected SyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int code, object[] args)
            : this(messageProvider,0, 0, code, args)
        {
        }

        protected SyntaxDiagnosticInfo(CommonMessageProvider messageProvider, int code)
            : this(messageProvider,0, 0, code)
        {
        }

        public int Offset { get { return _Offset;} }
        public int Width { get { return _Width;} }
    }
}
