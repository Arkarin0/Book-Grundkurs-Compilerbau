using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis
{
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


        protected GreenNode(ushort kind)
        {
            _kind = kind;
        }

        protected GreenNode(ushort kind, int fullWidth)
        {
            _kind = kind;
            _fullWidth = fullWidth;
        }

        #region Kind 

        public abstract string Language { get; }
        public int RawKind
        {
            get { return _kind; }
        }

        public bool IsList
        {
            get
            {
                return RawKind == ListKind;
            }
        }

        public abstract string KindText { get; }

        public virtual bool IsStructuredTrivia => false;
        public virtual bool IsDirective => false;
        public virtual bool IsToken => false;
        public virtual bool IsTrivia => false;
        public virtual bool IsSkippedTokensTrivia => false;
        public virtual bool IsDocumentationCommentTrivia => false;

        #endregion

        #region Slots 
        public int SlotCount
        {
            get
            {
                int count = _slotCount;
                if (count == byte.MaxValue)
                {
                    count = GetSlotCount();
                }

                return count;
            }

            protected set
            {
                _slotCount = (byte)value;
            }
        }

        public abstract GreenNode? GetSlot(int index);

        //internal GreenNode GetRequiredSlot(int index)
        //{
        //    var node = GetSlot(index);
        //    RoslynDebug.Assert(node is object);
        //    return node;
        //}

        // for slot counts >= byte.MaxValue
        protected virtual int GetSlotCount()
        {
            return _slotCount;
        }

        //public virtual int GetSlotOffset(int index)
        //{
        //    int offset = 0;
        //    for (int i = 0; i < index; i++)
        //    {
        //        var child = this.GetSlot(i);
        //        if (child != null)
        //        {
        //            offset += child.FullWidth;
        //        }
        //    }

        //    return offset;
        //}

        //internal Syntax.InternalSyntax.ChildSyntaxList ChildNodesAndTokens()
        //{
        //    return new Syntax.InternalSyntax.ChildSyntaxList(this);
        //}

        /// <summary>
        /// Enumerates all nodes of the tree rooted by this node (including this node).
        /// </summary>
        //internal IEnumerable<GreenNode> EnumerateNodes()
        //{
        //    yield return this;

        //    var stack = new Stack<Syntax.InternalSyntax.ChildSyntaxList.Enumerator>(24);
        //    stack.Push(this.ChildNodesAndTokens().GetEnumerator());

        //    while (stack.Count > 0)
        //    {
        //        var en = stack.Pop();
        //        if (!en.MoveNext())
        //        {
        //            // no more down this branch
        //            continue;
        //        }

        //        var current = en.Current;
        //        stack.Push(en); // put it back on stack (struct enumerator)

        //        yield return current;

        //        if (!current.IsToken)
        //        {
        //            // not token, so consider children
        //            stack.Push(current.ChildNodesAndTokens().GetEnumerator());
        //            continue;
        //        }
        //    }
        //}

        /// <summary>
        /// Find the slot that contains the given offset.
        /// </summary>
        /// <param name="offset">The target offset. Must be between 0 and <see cref="FullWidth"/>.</param>
        /// <returns>The slot index of the slot containing the given offset.</returns>
        /// <remarks>
        /// The base implementation is a linear search. This should be overridden
        /// if a derived class can implement it more efficiently.
        /// </remarks>
        //public virtual int FindSlotIndexContainingOffset(int offset)
        //{
        //    Debug.Assert(0 <= offset && offset < FullWidth);

        //    int i;
        //    int accumulatedWidth = 0;
        //    for (i = 0; ; i++)
        //    {
        //        Debug.Assert(i < SlotCount);
        //        var child = GetSlot(i);
        //        if (child != null)
        //        {
        //            accumulatedWidth += child.FullWidth;
        //            if (offset < accumulatedWidth)
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    return i;
        //}

        #endregion
    }
}

