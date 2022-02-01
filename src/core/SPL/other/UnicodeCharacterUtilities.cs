using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonea.Utilities
{
    /// <summary>
    /// Defines a set of helper methods to classify Unicode characters.
    /// </summary>
    internal static partial class UnicodeCharacterUtilities
    {
        public static bool IsIdentifierStartCharacter(char ch)
        {
            // identifier-start-character:
            //   letter-character
            //   _ (the underscore character U+005F)

            if (ch < 'a') // '\u0061'
            {
                if (ch < 'A') // '\u0041'
                {
                    return false;
                }

                return ch <= 'Z'  // '\u005A'
                    || ch == '_'; // '\u005F'
            }

            if (ch <= 'z') // '\u007A'
            {
                return true;
            }

            if (ch <= '\u007F') // max ASCII
            {
                return false;
            }

            return IsLetterChar(CharUnicodeInfo.GetUnicodeCategory(ch));
        }

        /// <summary>
        /// Returns true if the Unicode character can be a part of an identifier.
        /// </summary>
        /// <param name="ch">The Unicode character.</param>
        public static bool IsIdentifierPartCharacter(char ch)
        {
            // identifier-part-character:
            //   letter-character
            //   decimal-digit-character

            if (ch < 'a') // '\u0061'
            {
                if (ch < 'A') // '\u0041'
                {
                    return ch >= '0'  // '\u0030'
                        && ch <= '9'; // '\u0039'
                }

                return ch <= 'Z'  // '\u005A'
                    || ch == '_'; // '\u005F'
            }

            if (ch <= 'z') // '\u007A'
            {
                return true;
            }

            if (ch <= '\u007F') // max ASCII
            {
                return false;
            }

            //UnicodeCategory cat = CharUnicodeInfo.GetUnicodeCategory(ch);
            //return IsLetterChar(cat)
            //    || IsDecimalDigitChar(cat);

            //allow only ascii chars.
            return false;
        }

        /// <summary>
        /// Check that the name is a valid Unicode identifier.
        /// </summary>
        public static bool IsValidIdentifier([NotNullWhen(returnValue: true)] string? name)
        {
            if (SoneaString.IsNullOrEmpty(name))
            {
                return false;
            }

            if (!IsIdentifierStartCharacter(name[0]))
            {
                return false;
            }

            int nameLength = name.Length;
            for (int i = 1; i < nameLength; i++) //NB: start at 1
            {
                if (!IsIdentifierPartCharacter(name[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsLetterChar(UnicodeCategory cat)
        {
            // letter-character:
            //   A Unicode character of classes Lu, Ll, Lt, Lm, Lo, or Nl 
            //   A Unicode-escape-sequence representing a character of classes Lu, Ll, Lt, Lm, Lo, or Nl

            //switch (cat)
            //{
            //    case UnicodeCategory.UppercaseLetter:
            //    case UnicodeCategory.LowercaseLetter:
            //        return true;
            //}

            return false;
        }

        private static bool IsDecimalDigitChar(UnicodeCategory cat)
        {
            // decimal-digit-character:
            //   A Unicode character of the class Nd 
            //   A unicode-escape-sequence representing a character of the class Nd

            return cat == UnicodeCategory.DecimalDigitNumber;
        }
    }
}
