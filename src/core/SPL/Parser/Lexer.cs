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

            TokenHelper token = new TokenHelper();

            var c = reader.PeekChar();

            while (!reader.IsReallyAtEnd())
            {
                switch (c)
                {
                    case '/':
                        if (reader.PeekChar(1) == '/') LexComment(ref token, reader);
                        else if (reader.PeekChar(1) == '*') LexMultilineComment(ref token, reader);
                        else goto default;
                        break;
                    default:
                        reader.AdvanceChar();
                        break;
                }

                list.Add(CreateNode(token));
            }

            
            

            return list;
        }

        private void LexComment(ref TokenHelper token, SlidingTextWindow reader)
        {
            var text = "";
            var c = '\0';

            do
            {
                c = reader.NextChar();
                text += c;

            } while (!reader.IsReallyAtEnd() && c != '\n');

            token.Kind = SyntaxKind.SingleLineCommentToken;
            token.Text = text;
        }
        private void LexMultilineComment(ref TokenHelper token, SlidingTextWindow reader)
        {
            var text = "";
            var c = '\0';

            do
            {
                c = reader.NextChar();
                text += c;

            } while (!reader.IsReallyAtEnd() && !text.EndsWith("*/"));

            token.Kind = SyntaxKind.MultilineCommentToken;
            token.Text = text;
        }

        private SyntaxNode CreateNode(TokenHelper token)
        {
            var node = new Syntax.SyntaxNode(token.Kind, token.Text);
            return node;
        }

        private class TokenHelper
        {
            public SyntaxKind Kind= SyntaxKind.none;
            public string Text;
        }

    }
}
