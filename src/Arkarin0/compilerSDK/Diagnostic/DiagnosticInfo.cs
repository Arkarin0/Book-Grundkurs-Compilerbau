using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis
{

    /// <summary>
    /// A DiagnosticInfo object has information about a diagnostic, but without any attached location information.
    /// </summary>
    /// <remarks>
    /// More specialized diagnostics with additional information (e.g., ambiguity errors) can derive from this class to
    /// provide access to additional information about the error, such as what symbols were involved in the ambiguity.
    /// </remarks>
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    public class DiagnosticInfo
    {
        private readonly int _errorCode;

        /// <summary>
        /// The error code, as an integer.
        /// </summary>
        public int Code { get { return _errorCode; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticInfo"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public DiagnosticInfo(int error)
        {
            _errorCode = error;
        }

        public override string ToString()
        {
            return "error:" + this._errorCode;
        }

        private string GetDebuggerDisplay()
        {
            return this.GetType().Name + " " + this.ToString();
        }
    }
}
