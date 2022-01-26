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
    public class LexerTests
    {
        Lexer CreateValidInstance(string text = "")
        {
            var lexer = new Lexer(SourceText.From(text));
            return lexer;
        }

        [Fact]
        public void Create_AtNumericLliteralAndUnkownValueKind_ThrowsUnkownValueException()
        {
            var obj = CreateValidInstance();
            var token = new Lexer.TokenInfo();
            var text = "Unexpected value '";

            //find a value which is not in the enum
            int val = 0;
            while(Enum.IsDefined(typeof(Lexer.SpecialType),val)) val++;

            token.Kind = SyntaxKind.NumericLiteralToken;
            token.ValueKind = (Lexer.SpecialType)val;
            
            var ex =Assert.Throws<InvalidOperationException>(() => obj.CreateTestHelper(ref token));
            Assert.True(ex.Message.StartsWith(text), "The exception does not start with:\n" + text);
        }
       
    }
}