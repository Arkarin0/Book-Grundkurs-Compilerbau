using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    using BGC.CodeAnalysis.Syntax.InternalSyntax;
    internal partial class SyntaxToken:SPLSyntaxNode
    {
        public SyntaxToken(SyntaxKind kind)
            :base(kind)
        {

        }


    }
}
