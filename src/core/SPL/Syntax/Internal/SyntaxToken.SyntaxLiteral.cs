// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using System;
using System.Globalization;
using Arkarin0.CodeAnalysis;
using Roslyn.Utilities;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal partial class SyntaxToken
    {
        internal class SyntaxTokenWithValue<T> : SyntaxToken
        {
            protected readonly string textField;
            protected readonly T valueField;

            internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value)
            : base(kind, text.Length)
            {
                this.textField = text;
                this.valueField = value;
            }

            internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value, DiagnosticInfo[] diagnostics)
            : base(kind,text.Length, diagnostics)
            {
                this.textField = text;
                this.valueField = value;
            }

           
            static SyntaxTokenWithValue()
            {
                //ObjectBinder.RegisterTypeReader(typeof(MissingTokenWithTrivia), r => new MissingTokenWithTrivia(r));
            }

            public override string Text
            {
                get { return this.textField; }
            }

            public override object Value
            {
                get { return this.valueField; }
            }

            public override string ValueText => Convert.ToString(this.valueField, CultureInfo.InvariantCulture);

            /// <inheritdoc/>
            public override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
            {
                return new SyntaxTokenWithValue<T>(this.Kind, this.textField, this.valueField, diagnostics);
            }
        }
    }
}
