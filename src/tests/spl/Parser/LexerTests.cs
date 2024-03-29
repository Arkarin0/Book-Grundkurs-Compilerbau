﻿using Xunit;
using BGC.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BGC.CodeAnalysis.SPL;
using Microsoft.CodeAnalysis.Text;
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;

namespace BGC.Core.Parser.Tests
{
    public class LexerTests
    {
        Lexer CreateValidInstance( string text="")
        {
            var lexer = new Lexer(SourceText.From(text));
            return lexer;
        }

        SyntaxToken Lex(string text)
        {
            var lexer = CreateValidInstance(text);

            return lexer.Lex();
        }


        [Fact()]
        public void LexSingleLineCommentTest()
        {
            LexCommentTestHelper(SyntaxKind.SingleLineCommentToken, "//abc");
            LexCommentTestHelper( SyntaxKind.SingleLineCommentToken, "//abcdef//fgh");
            LexCommentTestHelper( SyntaxKind.SingleLineCommentToken, "//abcdef     //fgh");
            LexCommentTestHelper( SyntaxKind.SingleLineCommentToken, "//abcdef\r//fgh");
            LexCommentTestHelper( SyntaxKind.SingleLineCommentToken, "//abcdef\n", "//ghij");
            LexCommentTestHelper( SyntaxKind.SingleLineCommentToken, "//abcdef\n", "//ghij\r\n");

        }
        [Fact()]
        public void LexMultiLineCommentTest()
        {
            


            LexCommentTestHelper( SyntaxKind.MultilineCommentToken, "/*abc*/");
            LexCommentTestHelper( SyntaxKind.MultilineCommentToken, "/*abcdef  def//fgh*/");
            LexCommentTestHelper( SyntaxKind.MultilineCommentToken, "/*abcdef\r\nfgh*/");
            LexCommentTestHelper( SyntaxKind.MultilineCommentToken, "/*abcdef\r\n//fgh*/");
            LexCommentTestHelper( SyntaxKind.MultilineCommentToken, "/*abcdef\n*/", "/*ghij*/");
            

        }
        private void LexCommentTestHelper(SyntaxKind kind, params string[] text)
        {
            string expected = string.Join("", text);
            var lexer = this.CreateValidInstance(expected);
            var actual = lexer.Lex();

            int index = 0;
            //Assert.All(actual, (item) =>
            //{
            //    var expectedText = text[index++];
            //    Assert.Equal(expectedText, item.ValueText);
            //    Assert.Equal(kind, item.Kind);
            //});
        }

        [Theory()]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("\u0085")]
        [InlineData("\u2028")]
        [InlineData("\u2029")]
        public void LexEndOfLineTriviaTest(string text)
        {
            
            var actualToken = Lex(text);

            Assert.Equal(SyntaxKind.EndOfLineTrivia, actualToken.Kind);
            Assert.Equal(text, actualToken.ToString());

        }
    }
}