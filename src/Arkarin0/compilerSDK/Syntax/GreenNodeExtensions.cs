using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis
{
    public static class GreenNodeExtensions
    {
        /// <summary>
        /// Add Diagnosticinfo to a node.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <param name="node">The node whom to add diagnosticinfo.</param>
        /// <param name="diagnostics">The diagnostics to add.</param>
        public static TNode WithDiagnosticsGreen<TNode>(this TNode node, DiagnosticInfo[]? diagnostics) where TNode : GreenNode
        {
            return (TNode)node.SetDiagnostics(diagnostics);
        }

        /// <summary>
        /// Get the same node, but without diagnostic info.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <param name="node">The source node.</param>
        /// <returns>The same node but without diagnostic info.</returns>
        public static TNode WithoutDiagnosticsGreen<TNode>(this TNode node) where TNode : GreenNode
        {
            var current = node.GetDiagnostics();
            if (current == null || current.Length == 0)
            {
                return node;
            }

            return (TNode)node.SetDiagnostics(null);
        }
    }
}
