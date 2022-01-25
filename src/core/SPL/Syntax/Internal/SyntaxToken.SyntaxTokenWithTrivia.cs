﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using System;
using Arkarin0.CodeAnalysis;
using Roslyn.Utilities;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal partial class SyntaxToken
    {
        internal class SyntaxTokenWithTrivia : SyntaxToken
        {
            static SyntaxTokenWithTrivia()
            {
                //ObjectBinder.RegisterTypeReader(typeof(SyntaxTokenWithTrivia), r => new SyntaxTokenWithTrivia(r));
            }

            //protected readonly GreenNode LeadingField;
            //protected readonly GreenNode TrailingField;

            internal SyntaxTokenWithTrivia(SyntaxKind kind, DiagnosticInfo[] diagnostics):base(kind,diagnostics)
            {

            }

            //internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing)
            //    : base(kind)
            //{
            //    if (leading != null)
            //    {
            //        //this.AdjustFlagsAndWidth(leading);
            //        this.LeadingField = leading;
            //    }
            //    if (trailing != null)
            //    {
            //        //this.AdjustFlagsAndWidth(trailing);
            //        this.TrailingField = trailing;
            //    }
            //}

            //internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
            //    : base(kind/*, diagnostics, annotations*/)
            //{
            //    if (leading != null)
            //    {
            //        this.AdjustFlagsAndWidth(leading);
            //        this.LeadingField = leading;
            //    }
            //    if (trailing != null)
            //    {
            //        this.AdjustFlagsAndWidth(trailing);
            //        this.TrailingField = trailing;
            //    }
            //}



            //public override GreenNode GetLeadingTrivia()
            //{
            //    return this.LeadingField;
            //}

            //public override GreenNode GetTrailingTrivia()
            //{
            //    return this.TrailingField;
            //}

            //public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
            //{
            //    return new SyntaxTokenWithTrivia(this.Kind, trivia, this.TrailingField, this.GetDiagnostics(), this.GetAnnotations());
            //}

            //public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
            //{
            //    return new SyntaxTokenWithTrivia(this.Kind, this.LeadingField, trivia, this.GetDiagnostics(), this.GetAnnotations());
            //}

            /// <inheritdoc/>
            public override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
            {
                //return new SyntaxTokenWithTrivia(this.Kind, this.LeadingField, this.TrailingField, diagnostics, this.GetAnnotations());
                return new SyntaxTokenWithTrivia(this.Kind, diagnostics);
            }

            //internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
            //{
            //    return new SyntaxTokenWithTrivia(this.Kind, this.LeadingField, this.TrailingField, this.GetDiagnostics(), annotations);
            //}
        }
    }
}