using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    public enum SyntaxKind
    {
        none,
        BadToken,

        
        SingleLineCommentToken,
        MultilineCommentToken,
        DezimalLiteralToken,
        HexaDezimalLiteralToken,
        CharacterLiteralToken,

        ArrayKeyword,
        IfKeyword,
        ElseKeyword,
        WhileKeyword,
        ProcedureKeyword,
        TypeKeyword,
        VarKeyword,
        ReferenceKeyword,
        OfKeyword,

        IntTypeKeyword,

        IdentifierSyntaxKind,

        EndOfLineTrivia,
        WhitespaceTrivia,
    }
}
