using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis
{
    public abstract class AbstractLexer : IDisposable
    {

        protected readonly SlidingTextWindow TextWindow;


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
