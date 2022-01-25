using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGC.CodeAnalysis.SPL
{
    public enum SyntaxKind: ushort
    {
        None = 0,
        List = GreenNode.ListKind,

        //###########################################
        //### start of token assignemt
        //###########################################

        // punctuation
        /// <summary>Represents <c>+</c> token.</summary>
        PlusToken = 8000,
        /// <summary>Represents <c>-</c> token.</summary>
        MinusToken = 8001,
        /// <summary>Represents <c>*</c> token.</summary>
        AsteriskToken = 8002,
        /// <summary>Represents <c>/</c> token.</summary>
        SlashToken = 8003,
        /// <summary>Represents <c>:</c> token.</summary>
        ColonToken = 8004,
        /// <summary>Represents <c>;</c> token.</summary>
        SemicolonToken = 8005,
        /// <summary>Represents <c>,</c> token.</summary>
        CommaToken = 8006,
        /// <summary>Represents <c>&lt;</c> token.</summary>
        LessThanToken = 8007,
        /// <summary>Represents <c>&gt;</c> token.</summary>
        GreaterThanToken = 8008,
        /// <summary>Represents <c>=</c> token.</summary>
        EqualsToken = 8009,
        /// <summary>Represents <c>#</c> token.</summary>
        HashToken = 8010,
        /// <summary>Represents <c>(</c> token.</summary>
        OpenParenToken = 8011,
        /// <summary>Represents <c>)</c> token.</summary>
        CloseParenToken = 8012,
        /// <summary>Represents <c>{</c> token.</summary>
        OpenBraceToken = 8013,
        /// <summary>Represents <c>}</c> token.</summary>
        CloseBraceToken = 8014,
        /// <summary>Represents <c>[</c> token.</summary>
        OpenBracketToken = 8015,
        /// <summary>Represents <c>]</c> token.</summary>
        CloseBracketToken = 8016,
        /// <summary>Represents <c>'</c> token.</summary>
        SingleQuoteToken = 8017,
        /// <summary>Represents <c>_</c> token.</summary>
        UnderscoreToken = 8018,
        //
        //compound punctuation
        //
        /// <summary>Represents <c>:=</c> token.</summary>
        ColonEqualToken = 8101,
        /// <summary>Represents <c>&lt;=</c> token.</summary>
        LessThenEqualToken = 8102,
        /// <summary>Represents <c>&gt;=</c> token.</summary>
        GreaterThenEqualToken = 8103,
        //
        //keywords
        //8200 to 8300
        ///
        /// <summary>Represents <see langword="array"/>.</summary>
        ArrayKeyword = 8200,
        /// <summary>Represents <see langword="if"/>.</summary>
        IfKeyword = 8201,
        /// <summary>Represents <see langword="else"/>.</summary>
        ElseKeyword = 8202,
        /// <summary>Represents <see langword="while"/>.</summary>
        WhileKeyword = 8203,
        /// <summary>Represents <see langword="proc"/>.</summary>
        ProcedureKeyword = 8204,
        /// <summary>Represents <see langword="var"/>.</summary>
        VarKeyword = 8205,
        /// <summary>Represents <see langword="ref"/>.</summary>
        RefKeyword = 8206,
        /// <summary>Represents <see langword="of"/>.</summary>
        OfKeyword = 8207,
        /// <summary>Represents <see langword="int"/>.</summary>
        IntKeyword = 8208,
        //
        // Other
        //
        /// <summary>Represents the end of a file.</summary>
        EndOfFileToken = 8300, //NB: this is assumed to be the last textless token

        // tokens with text
        BadToken = 8500,
        IdentifierToken = 8501,
        NumericLiteralToken = 8502,
        CharacterLiteralToken = 8503,

        // trivia
        EndOfLineTrivia = 8550,
        WhitespaceTrivia = 8551,
        SingleLineCommentTrivia = 8552,
        MultiLineCommentTrivia = 8553,

        // names & type-names
        IdentifierName = 8600,

        //###########################################
        //### end of token assignemt
        //###########################################
    }
}
