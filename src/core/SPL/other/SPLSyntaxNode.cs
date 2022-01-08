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
        private readonly string _KindText;

        public SPLSyntaxNode( SyntaxKind kind):base((ushort)kind)
        {

        }



        public override string Language => "Simple Programming Language";

        public SyntaxKind Kind
        {
            get { return (SyntaxKind)this.RawKind; }
        }

        public override string KindText => this.Kind.ToString();

    }
}
