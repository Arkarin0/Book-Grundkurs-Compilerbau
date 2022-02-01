using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    internal enum ErrorCode
    {
        Void,
        Unkown,

        #region Diagnistics introduced in SPL 1

        
        ERR_IntOverflow = 1000,

        ERR_UnexpectedCharacter = 1100,

        #endregion  Diagnistics introduced in SPL 1
    }
}
