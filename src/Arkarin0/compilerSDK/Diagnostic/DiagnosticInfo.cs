using Sonea.Utilities;
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
        private string GetDebuggerDisplay()
        {
            return this.GetType().Name + " " + this.ToString();
        }

        private readonly int _errorCode;
        private readonly CommonMessageProvider _messageProvider;
        private readonly object[] _arguments= Array.Empty<object>();

        /// <summary>
        /// The error code, as an integer.
        /// </summary>
        public int Code { get { return _errorCode; } }


        internal DiagnosticInfo(int errorcode)
            :this(null, errorcode)
        {
        }

        protected DiagnosticInfo(CommonMessageProvider messageProvider, int errorCode)
        {
            _messageProvider = messageProvider;
            _errorCode = errorCode;
            //_defaultSeverity = messageProvider.GetSeverity(errorCode);
            //_effectiveSeverity = _defaultSeverity;
            _arguments = Array.Empty<object>();
        }

        // Only the compiler creates instances.
        protected DiagnosticInfo(CommonMessageProvider messageProvider, int errorCode, params object[] arguments)
            : this(messageProvider, errorCode)
        {
            //AssertMessageSerializable(arguments);

            _arguments = arguments;
        }

        public object[] Arguments
        {
            get { return _arguments; }
        }

        public CommonMessageProvider MessageProvider
        {
            get { return _messageProvider; }
        }


        //#####################
        //overrides


        public override string ToString()
        {
            return "error:" + this._errorCode;
        }

        public sealed override int GetHashCode()
        {
            int hashCode = _errorCode;
            for (int i = 0; i < _arguments.Length; i++)
            {
                hashCode = Hash.Combine(_arguments[i], hashCode);
            }

            return hashCode;
        }

        public sealed override bool Equals(object? obj)
        {
            DiagnosticInfo? other = obj as DiagnosticInfo;

            bool result = false;

            if (other != null &&
                other._errorCode == _errorCode &&
                other.GetType() == this.GetType())
            {
                if (_arguments.Length == other._arguments.Length)
                {
                    result = true;
                    for (int i = 0; i < _arguments.Length; i++)
                    {
                        if (!object.Equals(_arguments[i], other._arguments[i]))
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// For a DiagnosticInfo that is lazily evaluated, this method evaluates it
        /// and returns a non-lazy DiagnosticInfo.
        /// </summary>
        internal virtual DiagnosticInfo GetResolvedInfo()
        {
            // We should never call GetResolvedInfo on a non-lazy DiagnosticInfo
            throw ExceptionUtilities.Unreachable;
        }

    }
}
