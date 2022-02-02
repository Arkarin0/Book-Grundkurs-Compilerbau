// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

// #define COLLECT_STATS

using System;

namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax
{
    internal partial class LexerCache
    {
        internal const int MaxKeywordLength = 10;

        static LexerCache()
        {

        }

        internal LexerCache()
        {

        }

        internal void Free()
        {

        }

        internal bool TryGetKeywordKind(string key, out SyntaxKind kind)
        {
            if (key.Length > MaxKeywordLength)
            {
                kind = SyntaxKind.None;
                return false;
            }

            kind = SyntaxFacts.GetKeywordKind(key);
            return kind != SyntaxKind.None;
        }
    }
}
