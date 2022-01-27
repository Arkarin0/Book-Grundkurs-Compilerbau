using Xunit;
using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arkarin0.CodeAnalysis.TestHelper;

namespace Arkarin0.CodeAnalysis.Tests
{
    public class SyntaxDiagnosticInfoTests
    {
        [Fact()]
        public void CtorTest()
        {
            CommonMessageProvider provider = null;
            int errorcode = 10;
            int offset = 1;
            int width = 2;
            object[] args = { errorcode, offset, width };

            var obj = new MySyntaxDiagnosticInfo(provider, offset, width, errorcode, args);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, provider, offset, width, errorcode, args);

            obj = new MySyntaxDiagnosticInfo(provider, offset, width, errorcode);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, provider, offset, width, errorcode);

            obj = new MySyntaxDiagnosticInfo(provider, errorcode, args);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, provider, errorcode, args);

            obj = new MySyntaxDiagnosticInfo(provider, errorcode);
            TestHelper.CTorSyntaxDiagnosticInfo(obj, provider, errorcode);
        }

    }
}