using Xunit;
using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis.Tests
{
    public class DiagnosticInfoTests
    {
        [Fact()]
        public void CtorTest()
        {
            CommonMessageProvider provider = null;
            int errorcode = 10;
            object[] args = { errorcode };

            var obj = new DiagnosticInfo(errorcode);
            TestHelper.CTorDiagnosticInfo(obj, provider, errorcode);
        }

        [Fact()]
        public void ToStringTest()
        {
            int errorcode = 10;
            var obj = new DiagnosticInfo(errorcode);
            var str = obj.ToString();

            var expected = "error:" + errorcode;
            Assert.Equal(expected, str);            
        }

        [Fact()]
        public void GetResolvedInfo_ThrowsUnreachableExceptionTest()
        {
            int errorcode = 10;
            var obj = new DiagnosticInfo(errorcode);
            TestHelper.AssertUnreachableExceptionIsThrown(()=>obj.GetResolvedInfo());
        }
    }
}