// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using System;
using Arkarin0.CodeAnalysis;


namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal partial class SyntaxToken
    {
        public class SyntaxIdentifier : SyntaxToken
        {

            protected readonly string TextField;

            internal SyntaxIdentifier(string text)
                : base(SyntaxKind.IdentifierToken, text.Length)
            {
                this.TextField = text;
            }

            internal SyntaxIdentifier(string text, DiagnosticInfo[] diagnostics)
                : base(SyntaxKind.IdentifierToken, text.Length, diagnostics)
            {
                this.TextField = text;
            }


            public override string Text
            {
                get { return this.TextField; }
            }

            public override object Value
            {
                get { return this.TextField; }
            }

            public override string ValueText
            {
                get { return this.TextField; }
            }


            public override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
            {
                return new SyntaxIdentifier(this.Text, diagnostics);
            }
        }
    }
}
