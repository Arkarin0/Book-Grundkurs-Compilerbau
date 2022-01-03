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

        [Fact()]
        public void LexSingleLineCommentTest()
        {
            (var lexer, var reader) = this.CreateValidInstance();


            LexCommentTestHelper(lexer, SyntaxKind.SingleLineCommentToken, "//abc");
            LexCommentTestHelper(lexer, SyntaxKind.SingleLineCommentToken, "//abcdef//fgh");
            LexCommentTestHelper(lexer, SyntaxKind.SingleLineCommentToken, "//abcdef     //fgh");
            LexCommentTestHelper(lexer, SyntaxKind.SingleLineCommentToken, "//abcdef\r//fgh");
            LexCommentTestHelper(lexer, SyntaxKind.SingleLineCommentToken, "//abcdef\n", "//ghij");
            LexCommentTestHelper(lexer, SyntaxKind.SingleLineCommentToken, "//abcdef\n", "//ghij\r\n");

        }
        [Fact()]
        public void LexMultiLineCommentTest()
        {
            (var lexer, var reader) = this.CreateValidInstance();


            LexCommentTestHelper(lexer, SyntaxKind.MultilineCommentToken, "/*abc*/");
            LexCommentTestHelper(lexer, SyntaxKind.MultilineCommentToken, "/*abcdef  def//fgh*/");
            LexCommentTestHelper(lexer, SyntaxKind.MultilineCommentToken, "/*abcdef\r\nfgh*/");
            LexCommentTestHelper(lexer, SyntaxKind.MultilineCommentToken, "/*abcdef\r\n//fgh*/");
            LexCommentTestHelper(lexer, SyntaxKind.MultilineCommentToken, "/*abcdef\n*/", "/*ghij*/");
            

        }
        private void LexCommentTestHelper(Lexer lexer,SyntaxKind kind, params string[] text)
        {
            string expected = string.Join("", text);
            var actual = lexer.Lex(expected);

            int index = 0;
            Assert.All(actual, (item) =>
            {
                var expectedText = text[index++];
                Assert.Equal(expectedText, item.ValueText);
                Assert.Equal(kind, item.Kind);
            });
        }
    }
}