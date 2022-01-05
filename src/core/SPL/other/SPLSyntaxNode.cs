using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal abstract class SPLSyntaxNode:GreenNode
    {
        public SyntaxKind Kind { get; private set; }

        public SPLSyntaxNode( SyntaxKind kind)
        {
            this.Kind = kind;
        }


    }
}
