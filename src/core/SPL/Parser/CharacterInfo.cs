using Sonea.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    public static partial class SyntaxFacts
    {

        /// <summary>
        /// Returns true if the Unicode character can be the starting character of a C# identifier.
        /// </summary>
        /// <param name="ch">The Unicode character.</param>
        public static bool IsIdentifierStartCharacter(char ch)
        {
            return UnicodeCharacterUtilities.IsIdentifierStartCharacter(ch);
        }

        /// <summary>
        /// Returns true if the Unicode character can be a part of a C# identifier.
        /// </summary>
        /// <param name="ch">The Unicode character.</param>
        public static bool IsIdentifierPartCharacter(char ch)
        {
            return UnicodeCharacterUtilities.IsIdentifierPartCharacter(ch);
        }

        /// <summary>
        /// Check that the name is a valid identifier.
        /// </summary>
        public static bool IsValidIdentifier([NotNullWhen(true)] string? name)
        {
            return UnicodeCharacterUtilities.IsValidIdentifier(name);
        }



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
