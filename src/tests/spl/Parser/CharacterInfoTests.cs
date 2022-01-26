using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BGC.CodeAnalysis.SPL;
using Microsoft.CodeAnalysis.Text;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;

namespace BGC.SPL.Parser.Tests
{
    public class CharacterInfoTests
    {
        
        [Theory]
        [InlineData('0')]
        [InlineData('1')]
        [InlineData('2')]
        [InlineData('3')]
        [InlineData('4')]
        [InlineData('5')]
        [InlineData('6')]
        [InlineData('7')]
        [InlineData('8')]
        [InlineData('9')]
        [InlineData('A')]
        [InlineData('a')]
        [InlineData('B')]
        [InlineData('b')]
        [InlineData('C')]
        [InlineData('c')]
        [InlineData('D')]
        [InlineData('d')]
        [InlineData('E')]
        [InlineData('e')]
        [InlineData('F')]
        [InlineData('f')]
        public void IsHexDigitTest(char c)
        {
            Assert.True(SyntaxFacts.IsHexDigit(c),$"The \' {c}\'-char was not accepted");
        }

        [Theory]
        [InlineData('0')]
        [InlineData('1')]
        [InlineData('2')]
        [InlineData('3')]
        [InlineData('4')]
        [InlineData('5')]
        [InlineData('6')]
        [InlineData('7')]
        [InlineData('8')]
        [InlineData('9')]
        public void IsDecDigitTest(char c)
        {
            Assert.True(SyntaxFacts.IsDecDigit(c), $"The \' {c}\'-char was not accepted");
        }
    }
}
