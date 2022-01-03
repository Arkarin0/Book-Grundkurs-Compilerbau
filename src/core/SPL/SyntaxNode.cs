using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.Core
{
    public abstract partial class SyntaxNode
    {            
        SyntaxKind Kind { get; init; }

        string ValueText { get; init; }
    }
}
