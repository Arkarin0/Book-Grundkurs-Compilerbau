using Xunit;
using BGC.CodeAnalysis.SPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkarin0.CodeAnalysis;

namespace BGC.CodeAnalysis.SPL.Tests
{
    public class SyntaxDiagnosticInfoTests
    {
        [Fact()]
        public void CtorTest()
        {
            ErrorCode errorcode = ErrorCode.ERR_IntOverflow;
            int offset = 1;
            int width = 2;
            object[] args = { errorcode, offset, width };

            var obj = new SyntaxDiagnosticInfo(offset, width, errorcode, args);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, offset, width, errorcode, args);

            obj = new SyntaxDiagnosticInfo( offset, width, errorcode);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, offset, width, errorcode);

            obj = new SyntaxDiagnosticInfo( errorcode, args);
            TestHelper.CTorSyntaxDiagnosticInfo(obj,  errorcode, args);

            obj = new SyntaxDiagnosticInfo(errorcode);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, errorcode);
        }

        
    }
}