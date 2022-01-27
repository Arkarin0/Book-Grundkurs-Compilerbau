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

        private List<SyntaxDiagnosticInfo> _errors;


        protected AbstractLexer(SourceText text)
        {
            this.TextWindow = new SlidingTextWindow(text);
        }

        public virtual void Dispose() => this.TextWindow.Dispose();


        protected void Start()
        {
            TextWindow.Start();
            _errors = null;
        }

        protected void AddError(SyntaxDiagnosticInfo error)
        {
            if (error != null)
            {
                if (_errors == null)
                {
                    _errors = new List<SyntaxDiagnosticInfo>(8);
                }

                _errors.Add(error);
            }
        }

        protected bool HasErrors
        {
            get { return _errors != null; }
        }

#if Feature_Trivia
        protected SyntaxDiagnosticInfo[] GetErrors(int leadingTriviaWidth)
#else

        protected SyntaxDiagnosticInfo[] GetErrors()
#endif
        {
            if (_errors != null)
            {
#if Feature_Trivia
                if (leadingTriviaWidth > 0)
                {
                    var array = new SyntaxDiagnosticInfo[_errors.Count];
                    for (int i = 0; i < _errors.Count; i++)
                    {
                        // fixup error positioning to account for leading trivia
                        array[i] = _errors[i].WithOffset(_errors[i].Offset + leadingTriviaWidth);
                    }

                    return array;
                }
                else
#endif
                {
                    return _errors.ToArray();
                }
            }
            else
            {
                return null;
            }
        }

    }
}
