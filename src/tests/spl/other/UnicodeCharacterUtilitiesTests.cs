using Xunit;
using Sonea.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonea.Utilities.Tests
{
    public class UnicodeCharacterUtilitiesTests
    {
        [Fact()]
        public void IsIdentifierStartCharacterTest()
        {
            var valid = new List<char>();
            for (char c = 'A'; c <= 'Z'; c++) valid.Add(c);
            for (char c = 'a'; c <= 'z'; c++) valid.Add(c);
            valid.Add('_');


            for (char c = Char.MinValue; c < Char.MaxValue; c++)
            {
                Assert.True(valid.Contains(c)== UnicodeCharacterUtilities.IsIdentifierStartCharacter(c), $"Character \'{c}\' (dez{(byte)c}) failed.");
            }
        }

        [Fact()]
        public void IsIdentifierPartCharacterTest()
        {
            var valid = new List<char>();
            for (char c = 'A'; c <= 'Z'; c++) valid.Add(c);
            for (char c = 'a'; c <= 'z'; c++) valid.Add(c);
            for (char c = '0'; c <= '9'; c++) valid.Add(c);
            valid.Add('_');


            for (char c = Char.MinValue; c < Char.MaxValue; c++)
            {
                Assert.True(valid.Contains(c) == UnicodeCharacterUtilities.IsIdentifierPartCharacter(c), $"Character \'{c}\' (dez{(int)c}) failed.");
            }
        }

        [Theory()]
        [InlineData("abc",true)]
        [InlineData("_abc", true)]
        [InlineData("_0", true)]
        [InlineData("_0A", true)]
        [InlineData("_A0", true)]
        [InlineData("_A_0", true)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("\0", false)]
        [InlineData(null, false)]
        [InlineData("abc+def", false)]
        public void IsValidIdentifierTest(string text, bool expectedResult)
        {
            Assert.True(expectedResult== UnicodeCharacterUtilities.IsValidIdentifier(text), $"Text \'{text}\' failed.");
        }
    }
}