using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal partial class Lexer
    {

        [Conditional("Unittest")]
        [EditorBrowsable( EditorBrowsableState.Never)]
        public void CreateTestHelper(ref TokenInfo info)
        {
            this.Create(ref info, null, null, null);
        }
    }
}
