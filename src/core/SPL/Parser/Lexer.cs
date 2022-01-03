using BGC.Core.Syntax.InternalSyntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.Core.Parser
{
    public class Lexer
    {
        public IEnumerable<SyntaxNode> Lex(string source)
        {
            List<SyntaxNode> list = new List<SyntaxNode>();

            var reader = new SlidingTextWindow(source);

            string text = "";

            while (!reader.IsReallyAtEnd())
            {
                
                var c = reader.NextChar();

                text += c;

            }

            var node = new Syntax.SyntaxNode(SyntaxKind.BadToken, text);
            list.Add(node);

            return list;
        }
    }
}
