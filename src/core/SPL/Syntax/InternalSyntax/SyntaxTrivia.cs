﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal class SyntaxTrivia : SPLSyntaxNode
    {
        public readonly string Text;

        internal SyntaxTrivia(SyntaxKind kind, string text/*, DiagnosticInfo[]? diagnostics = null, SyntaxAnnotation[]? annotations = null*/)
            : base(kind/*, diagnostics, annotations, text.Length*/)
        {
            this.Text = text;            
        }

        internal static SyntaxTrivia Create(SyntaxKind kind, string text)
        {
            return new SyntaxTrivia(kind, text);
        }

        //public override string ToFullString()
        //{
        //    return this.Text;
        //}

        public override string ToString()
        {
            return this.Text;
        }
    }
}
