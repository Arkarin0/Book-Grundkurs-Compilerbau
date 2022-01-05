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
        /// Returns true if the Unicode character is a newline character.
        /// </summary>
        /// <param name="ch">The Unicode character.</param>
        public static bool IsNewLine(char ch)
        {
            // new-line-character:
            //   Carriage return character (U+000D)
            //   Line feed character (U+000A)
            //   Next line character (U+0085)
            //   Line separator character (U+2028)
            //   Paragraph separator character (U+2029)

            return ch == '\r'
                || ch == '\n'
                || ch == '\u0085'
                || ch == '\u2028'
                || ch == '\u2029';
        }
    }
}
