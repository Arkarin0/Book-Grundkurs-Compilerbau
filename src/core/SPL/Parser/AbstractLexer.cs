using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal class AbstractLexer : IDisposable
    {

        internal readonly SlidingTextWindow TextWindow;



        protected AbstractLexer(SourceText text)
        {
            this.TextWindow = new SlidingTextWindow(text);
        }

        public virtual void Dispose() => this.TextWindow.Dispose();


        protected void Start()
        {
            TextWindow.Start();
            //_errors = null;
        }

    }
}
