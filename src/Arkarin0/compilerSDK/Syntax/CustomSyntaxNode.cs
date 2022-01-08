using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis.Syntax
{
    public abstract class CustomSyntaxNode:GreenNode
    {
        protected CustomSyntaxNode(ushort kind) : base(kind) { }

        protected CustomSyntaxNode(ushort kind, int fullWidth) : base(kind, fullWidth) { }
    }
}
