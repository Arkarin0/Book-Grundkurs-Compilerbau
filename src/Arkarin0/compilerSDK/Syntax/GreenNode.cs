using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace Arkarin0.CodeAnalysis
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public abstract class GreenNode
    {
        private string GetDebuggerDisplay()
        {
            return this.GetType().Name + " " + this.KindText + " " + this.ToString();
        }

        public const int ListKind = 1;

        private readonly ushort _kind;
        private byte _slotCount;
        private int _fullWidth;
        private NodeFlags flags;

        private readonly static Dictionary<GreenNode, DiagnosticInfo[]> s_diagnosticsTable = 
            new Dictionary<GreenNode, DiagnosticInfo[]>();

        private readonly static DiagnosticInfo[] s_noDiagnostics= Array.Empty<DiagnosticInfo>();


        protected GreenNode(ushort kind)
        {
            _kind = kind;
        }

        protected GreenNode(ushort kind, int fullWidth)
        {
            _kind = kind;
            _fullWidth = fullWidth;
        }

        protected GreenNode(ushort kind, DiagnosticInfo[]? diagnostics, int fullWidth)
        {
            _kind = kind;
            _fullWidth = fullWidth;
            if (diagnostics?.Length > 0)
            {
                this.flags |= NodeFlags.ContainsDiagnostics;
                s_diagnosticsTable.Add(this, diagnostics);
            }
        }

        protected GreenNode(ushort kind, DiagnosticInfo[]? diagnostics)
        {
            _kind = kind;
            if (diagnostics?.Length > 0)
            {
                this.flags |= NodeFlags.ContainsDiagnostics;
                s_diagnosticsTable.Add(this, diagnostics);
            }
        }

        #region Kind         
        /// <summary>
        /// Gets the kind represented as an integer
        /// </summary>        
        public int RawKind
        {
            get { return _kind; }
        }

       

        /// <summary>
        /// Gets the language this token is designed for.
        /// </summary>        
        public abstract string Language { get; }

        /// <summary>
        /// Gets the kind text.
        /// </summary>
        public abstract string KindText { get; }


        public bool IsList
        {
            get
            {
                return RawKind == ListKind;
            }
        }



        

       
        public virtual bool IsToken => false;
        public virtual bool IsTrivia => false;
       
        #endregion

        #region Spans
        /// <summary>
        /// Gets the text full width.
        /// </summary>        
        public int FullWidth { get { return _fullWidth; } protected set { _fullWidth = value; } }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public virtual int Width { get { return _fullWidth; } }
        #endregion

        #region Flags
        [Flags]
        public enum NodeFlags : byte
        {
            /// <summary>
            /// No Flag is set.
            /// </summary>
            None = 0,

            /// <summary>
            /// The node contains diagnostical info.
            /// </summary>
            ContainsDiagnostics = 1 << 0,
            /// <summary>
            /// The node is not missed in the Syntaxtree.
            /// </summary>
            IsNotMissing = 1 << 1,

            /// <summary>
            /// Set all possible flags.
            /// </summary>
            InheritMask = ContainsDiagnostics | IsNotMissing,
        }

        /// <summary>
        /// Gets the currently set flags.
        /// </summary>
        public NodeFlags Flags
        {
            get { return this.flags; }
        }

        /// <summary>
        /// Sets the flags.
        /// </summary>
        /// <param name="flags">The flags.</param>
        internal protected void SetFlags(NodeFlags flags)
        {
            this.flags |= flags;
        }

        /// <summary>
        /// Clears the flags.
        /// </summary>
        /// <param name="flags">The flags.</param>
        internal protected void ClearFlags(NodeFlags flags)
        {
            this.flags &= ~flags;
        }

        /// <summary>
        /// Gets a value indicating whether this token is missed.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this token is missed; otherwise, <see langword="true"/>.        ///   
        /// </value>
        public bool IsMissing
        {
            get
            {
                // flag has reversed meaning hence "=="
                return (this.flags & NodeFlags.IsNotMissing) == 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this token contains diagnostics.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if this token contains diagnostics; otherwise, <see langword="false" />.
        /// </value>
        public bool ContainsDiagnostics
        {
            get
            {
                return (this.flags & NodeFlags.ContainsDiagnostics) != 0;
            }
        }
        #endregion

        #region Diagnostics
        public DiagnosticInfo[] GetDiagnostics()
        {
            if (this.ContainsDiagnostics)
            {
                DiagnosticInfo[]? diags;
                if (s_diagnosticsTable.TryGetValue(this, out diags))
                {
                    return diags;
                }
            }

            return s_noDiagnostics;
        }

        public abstract GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics);

        /// <summary>
        /// Add an error to the given node, creating a new node that is the same except it has no parent,
        /// and has the given error attached to it. The error span is the entire span of this node.
        /// </summary>
        /// <param name="err">The error to attach to this node</param>
        /// <returns>A new node, with no parent, that has this error added to it.</returns>
        /// <remarks>Since nodes are immutable, the only way to create nodes with errors attached is to create a node without an error,
        /// then add an error with this method to create another node.</remarks>
        public GreenNode AddError(DiagnosticInfo err)
        {
            DiagnosticInfo[] errorInfos;

            // If the green node already has errors, add those on.
            if (GetDiagnostics() == null)
            {
                errorInfos = new[] { err };
            }
            else
            {
                // Add the error to the error list.
                errorInfos = GetDiagnostics();
                var length = errorInfos.Length;
                Array.Resize(ref errorInfos, length + 1);
                errorInfos[length] = err;
            }

            // Get a new green node with the errors added on.
            return SetDiagnostics(errorInfos);
        }
        #endregion

        #region Tokens        
        /// <summary>
        /// Gets the value this token represents.
        /// </summary>
        /// <returns>The value this token represents</returns>
        public virtual object? GetValue() { return null; }

        /// <summary>
        /// Gets the textual representation of <see cref="GetValue"/>.
        /// </summary>
        /// <returns></returns>
        public virtual string GetValueText() { return string.Empty; }
        #endregion
    }
}

