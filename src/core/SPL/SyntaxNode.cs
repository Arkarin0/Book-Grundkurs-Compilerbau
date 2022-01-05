using BGC.CodeAnalysis.SPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.Core
{
    public abstract partial class SyntaxNode
    {            
        public SyntaxKind Kind { get; init; }

        public string ValueText { get; init; }
    }
}
