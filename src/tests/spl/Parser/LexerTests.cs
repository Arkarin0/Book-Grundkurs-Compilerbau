using Xunit;
using BGC.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BGC.Core.Parser.Tests
{
    public class LexerTests
    {
        (Lexer, TextReader ) CreateValidInstance( string text="")
        {
            var lexer = new Lexer();
            var reader = new StringReader(text);

            return (lexer, reader);
        }

        [Fact()]
        public void LexTest()
        {
            (var lexer, var reader) = this.CreateValidInstance();

            Assert.NotNull(lexer);
            Assert.NotNull(reader);

            string text = "abc";
            var actual =lexer.Lex(text);

            Assert.NotEmpty(actual);

            var item = actual.First();
            Assert.Equal(text, item.ValueText);
        }
    }
}