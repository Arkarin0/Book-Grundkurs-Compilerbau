using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis.Syntax
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public abstract class CustomSyntaxNode:GreenNode
    {
        private string GetDebuggerDisplay()
        {
            return this.GetType().Name + " " + this.KindText + " " + this.ToString();
        }

        /// <inheritdoc/>
        protected CustomSyntaxNode(ushort kind) : base(kind) { }

        /// <inheritdoc/>
        protected CustomSyntaxNode(ushort kind, int fullWidth) : base(kind, fullWidth) { }

        /// <inheritdoc/>
        protected CustomSyntaxNode(ushort kind, DiagnosticInfo[] diagnostics) : base(kind, diagnostics)
        {
        }

        /// <inheritdoc/>
        protected CustomSyntaxNode(ushort kind, DiagnosticInfo[] diagnostics, int fullWidth) : base(kind, diagnostics, fullWidth)
        {
        }
    }
}
