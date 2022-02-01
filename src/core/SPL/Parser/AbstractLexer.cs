using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal class AbstractLexer : Arkarin0.CodeAnalysis.AbstractLexer
    {
        public AbstractLexer(SourceText text) : base(text)
        {

        }

        protected void AddError(ErrorCode code)
        {
            this.AddError(MakeError(code));
        }

        protected void AddError(ErrorCode code, params object[] args)
        {
            this.AddError(MakeError(code, args));
        }

        protected static SyntaxDiagnosticInfo MakeError(ErrorCode code)
        {
            return new SyntaxDiagnosticInfo(code);
        }

        protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args)
        {
            return new SyntaxDiagnosticInfo(code, args);
        }
    }
}
