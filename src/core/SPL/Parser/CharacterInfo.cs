using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    public static partial class SyntaxFacts
    {
        /// <summary>
        /// Returns true if the Unicode character is a hexadecimal digit.
        /// </summary>
        /// <param name="c">The Unicode character.</param>
        /// <returns>true if the character is a hexadecimal digit 0-9, A-F, a-f.</returns>
        internal static bool IsHexDigit(char c)
        {
            return (c >= '0' && c <= '9') ||
                   (c >= 'A' && c <= 'F') ||
                   (c >= 'a' && c <= 'f');
        }

        /// <summary>
        /// Returns true if the Unicode character is a decimal digit.
        /// </summary>
        /// <param name="c">The Unicode character.</param>
        /// <returns>true if the Unicode character is a decimal digit.</returns>
        internal static bool IsDecDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}
