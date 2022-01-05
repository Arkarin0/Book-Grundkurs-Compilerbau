using Xunit;
using BGC.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BGC.CodeAnalysis.SPL;
using Microsoft.CodeAnalysis.Text;
namespace BGC.CodeAnalysis.SPL.Syntax.InternalSyntax.Tests
{
    public class SyntaxFactoryTests
    {
        [Theory]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        [InlineData("abc")]
        public void EndOfLineTest(string expected)
        {
            var expectedKind = SyntaxKind.EndOfLineTrivia;
            var trivia= SyntaxFactory.EndOfLine(expected);
            Assert.Equal(expected, trivia.Text);
            Assert.Equal(expectedKind, trivia.Kind);
        }
    }
}
