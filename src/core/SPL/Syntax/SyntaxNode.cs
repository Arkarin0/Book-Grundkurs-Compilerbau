using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.Core.Syntax
{
    internal class SyntaxNode: BGC.Core.SyntaxNode
    {
        public SyntaxNode(SyntaxKind kind, string value)
        {
            this.Kind = kind;
            this.ValueText = value;
        }
    }
}
