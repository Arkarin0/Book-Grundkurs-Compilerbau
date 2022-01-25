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
using BGC.CodeAnalysis.SPL.Syntax.InternalSyntax;

namespace BGC.Core.Parser.Tests
{
    public class LexerTests
    {
        Lexer CreateValidInstance(string text = "")
        {
            var lexer = new Lexer(SourceText.From(text));
            return lexer;
        }

       
    }
}