using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal static class SyntaxTokenExtensions
    {
        public static SyntaxKind Kind(this Syntax.InternalSyntax.SyntaxToken token) => token?.Kind ?? SyntaxKind.None;

        public static DiagnosticInfo[] Errors(this Syntax.InternalSyntax.SyntaxToken token) => token.GetDiagnostics();
    }
}
