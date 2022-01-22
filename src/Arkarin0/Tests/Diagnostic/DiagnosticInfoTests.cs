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
        public void DiagnosticInfoTest()
        {
            int errorcode = 10;

            var obj = new DiagnosticInfo(errorcode);

            Assert.Equal(errorcode, obj.Code);
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
    }
}