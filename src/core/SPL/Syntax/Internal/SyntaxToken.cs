using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    //using BGC.CodeAnalysis.Syntax.InternalSyntax;
    internal partial class SyntaxToken : SPLSyntaxNode
    {

 


        //public override GreenNode GetSlot(int index)
        //{
        //    throw Sonea.Utilities.ExceptionUtilities.Unreachable;
        //}

        internal static SyntaxToken Create(SyntaxKind kind)
        {
            if (kind > LastTokenWithWellKnownText)
            {
                if (!SyntaxFacts.IsAnyToken(kind))
                {
                    throw new ArgumentException($"This methode can only be used to create Tokens based on {typeof(SyntaxKind)}", nameof(kind));
                }

                return CreateMissing(kind, null, null);

            }

            return s_tokensWithNoTrivia[(int)kind].Value;
        }

        //internal static SyntaxToken Create(SyntaxKind kind, GreenNode leading, GreenNode trailing)
        //{
        //    if (kind > LastTokenWithWellKnownText)
        //    {
        //        if (!SyntaxFacts.IsAnyToken(kind))
        //        {
        //            throw new ArgumentException($"This methode can only be used to create Tokens based on {typeof(SyntaxKind)}", nameof(kind));
        //        }

        //        return CreateMissing(kind, leading, trailing);
        //    }

        //    if (leading == null)
        //    {
        //        if (trailing == null)
        //        {
        //            return s_tokensWithNoTrivia[(int)kind].Value;
        //        }
        //        else if (trailing == SyntaxFactory.Space)
        //        {
        //            return s_tokensWithSingleTrailingSpace[(int)kind].Value;
        //        }
        //        else if (trailing == SyntaxFactory.CarriageReturnLineFeed)
        //        {
        //            return s_tokensWithSingleTrailingCRLF[(int)kind].Value;
        //        }
        //    }


        //    return new SyntaxTokenWithTrivia(kind, leading, trailing);
        //}

        internal static SyntaxToken CreateMissing(SyntaxKind kind, GreenNode leading, GreenNode trailing)
        {
            return new MissingTokenWithTrivia(kind/*, leading, trailing*/);
        }



        internal const SyntaxKind FirstTokenWithWellKnownText = SyntaxKind.PlusToken;
        internal const SyntaxKind LastTokenWithWellKnownText = SyntaxKind.EndOfFileToken;

        private static readonly ArrayElement<SyntaxToken>[] s_tokensWithNoTrivia = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];
        private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingSpace = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];
        private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingCRLF = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];
        private static readonly SyntaxKind[] s_WellKnownTokenKinds = Array.Empty<SyntaxKind>();

        static SyntaxToken()
        {
            //ObjectBinder.RegisterTypeReader(typeof(SyntaxToken), r => new SyntaxToken(r));

            //prebuild wellknown Tokens
            List<SyntaxKind> syntaxKinds = new List<SyntaxKind>();
            for (var kind = FirstTokenWithWellKnownText; kind <= LastTokenWithWellKnownText; kind++)
            {
                s_tokensWithNoTrivia[(int)kind].Value = new SyntaxToken(kind);
                syntaxKinds.Add(kind);

                //s_tokensWithSingleTrailingSpace[(int)kind].Value = new SyntaxTokenWithTrivia(kind, null, SyntaxFactory.Space);
                //s_tokensWithSingleTrailingCRLF[(int)kind].Value = new SyntaxTokenWithTrivia(kind, null, SyntaxFactory.CarriageReturnLineFeed);
            }
            s_WellKnownTokenKinds = syntaxKinds.ToArray();
        }

        public SyntaxToken(SyntaxKind kind): base(kind)
        {
            FullWidth = Text.Length;
            this.flags |= NodeFlags.IsNotMissing;
        }


        public SyntaxToken(SyntaxKind kind, DiagnosticInfo[] diagnostics) : base(kind, diagnostics)
        {
            FullWidth = Text.Length;
            this.flags |= NodeFlags.IsNotMissing;
        }

        internal SyntaxToken(SyntaxKind kind, int fullwidth) : base(kind,fullwidth)
        {
            this.flags |= NodeFlags.IsNotMissing;
        }
        internal SyntaxToken(SyntaxKind kind,int fullwidth, DiagnosticInfo[] diagnostics) : base(kind, diagnostics, fullwidth)
        {
            this.flags |= NodeFlags.IsNotMissing;
        }


        /// <inheritdoc cref="ToString"/>        
        public virtual string Text
        {
            get { return SyntaxFacts.GetText(this.Kind); }
        }

        /// <summary>
        /// Returns the string representation of this token, not including its leading and trailing trivia.
        /// </summary>
        /// <returns>The string representation of this token, not including its leading and trailing trivia.</returns>
        /// <remarks>The length of the returned string is always the same as Span.Length</remarks>
        public override string ToString()
        {
            return this.Text;
        }

        /// <inheritdoc/>
        public override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
        {
            return new SyntaxToken(this.Kind, diagnostics);
        }

        public virtual object Value
        {
            get
            {
                switch (this.Kind)
                {
                    //case SyntaxKind.TrueKeyword:
                    //    return Boxes.BoxedTrue;
                    //case SyntaxKind.FalseKeyword:
                    //    return Boxes.BoxedFalse;
                    //case SyntaxKind.NullKeyword:
                    //    return null;
                    default:
                        return this.Text;
                }
            }
        }

        /// <inheritdoc/>
        public virtual string ValueText
        {
            get { return this.Text; }
        }

        /// <inheritdoc/>
        public override object GetValue()
        {
            return this.Value;
        }

        /// <inheritdoc/>
        public override string GetValueText()
        {
            return this.ValueText;
        }

        public static IEnumerable<SyntaxKind> GetWellKnownTokenKinds()
        {
            return s_WellKnownTokenKinds;
        }

        internal static IEnumerable<SyntaxToken> GetWellKnownTokens()
        {
            foreach (var element in s_tokensWithNoTrivia)
            {
                if (element.Value != null)
                {
                    yield return element.Value;
                }
            }


            foreach (var element in s_tokensWithSingleTrailingSpace)
            {
                if (element.Value != null)
                {
                    yield return element.Value;
                }
            }

            foreach (var element in s_tokensWithSingleTrailingCRLF)
            {
                if (element.Value != null)
                {
                    yield return element.Value;
                }
            }
        }
    }
}
