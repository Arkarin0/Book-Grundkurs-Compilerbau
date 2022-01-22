using Arkarin0.CodeAnalysis;
using Arkarin0.CodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal abstract class SPLSyntaxNode:CustomSyntaxNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SPLSyntaxNode"/> class.
        /// </summary>
        /// <param name="kind">The kind.</param>
        public SPLSyntaxNode(SyntaxKind kind):base((ushort)kind)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SPLSyntaxNode"/> class.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <param name="fullWidth">The full width.</param>
        protected SPLSyntaxNode(SyntaxKind kind, int fullWidth) : base((ushort)kind, fullWidth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPLSyntaxNode"/> class.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <param name="diagnostics">The diagnostics.</param>
        protected SPLSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics) : base((ushort)kind, diagnostics)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPLSyntaxNode"/> class.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <param name="diagnostics">The diagnostics.</param>
        /// <param name="fullWidth">The full width.</param>
        protected SPLSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics, int fullWidth) : base((ushort)kind, diagnostics, fullWidth)
        {
        }

        /// <inheritdoc/>
        public sealed override string Language => "Simple Programming Language";

        /// <summary>
        /// Gets the kind of this SyntaxNode.
        /// </summary>
        public SyntaxKind Kind
        {
            get { return (SyntaxKind)this.RawKind; }
        }

        /// <inheritdoc/>
        public override string KindText => this.Kind.ToString();

        /// <inheritdoc/>
        public bool IsTriviaWithEndOfLine()
        {
            return this.Kind == SyntaxKind.EndOfLineTrivia
                || this.Kind == SyntaxKind.SingleLineCommentTrivia;
        }

    }
}
