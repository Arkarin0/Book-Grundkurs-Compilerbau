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
        /// <summary>Represents <c>~</c> token.</summary>
        TildeToken = 8193,
        /// <summary>Represents <c>!</c> token.</summary>
        ExclamationToken = 8194,
        /// <summary>Represents <c>$</c> token.</summary>
        /// <remarks>This is a debugger special punctuation and not related to string interpolation.</remarks>
        DollarToken = 8195,
        /// <summary>Represents <c>%</c> token.</summary>
        PercentToken = 8196,
        /// <summary>Represents <c>^</c> token.</summary>
        CaretToken = 8197,
        /// <summary>Represents <c>&amp;</c> token.</summary>
        AmpersandToken = 8198,
        /// <summary>Represents <c>*</c> token.</summary>
        AsteriskToken = 8199,
        /// <summary>Represents <c>(</c> token.</summary>
        OpenParenToken = 8200,
        /// <summary>Represents <c>)</c> token.</summary>
        CloseParenToken = 8201,
        /// <summary>Represents <c>-</c> token.</summary>
        MinusToken = 8202,
        /// <summary>Represents <c>+</c> token.</summary>
        PlusToken = 8203,
        /// <summary>Represents <c>=</c> token.</summary>
        EqualsToken = 8204,
        /// <summary>Represents <c>{</c> token.</summary>
        OpenBraceToken = 8205,
        /// <summary>Represents <c>}</c> token.</summary>
        CloseBraceToken = 8206,
        /// <summary>Represents <c>[</c> token.</summary>
        OpenBracketToken = 8207,
        /// <summary>Represents <c>]</c> token.</summary>
        CloseBracketToken = 8208,
        /// <summary>Represents <c>|</c> token.</summary>
        BarToken = 8209,
        /// <summary>Represents <c>\</c> token.</summary>
        BackslashToken = 8210,
        /// <summary>Represents <c>:</c> token.</summary>
        ColonToken = 8211,
        /// <summary>Represents <c>;</c> token.</summary>
        SemicolonToken = 8212,
        /// <summary>Represents <c>"</c> token.</summary>
        DoubleQuoteToken = 8213,
        /// <summary>Represents <c>'</c> token.</summary>
        SingleQuoteToken = 8214,
        /// <summary>Represents <c>&lt;</c> token.</summary>
        LessThanToken = 8215,
        /// <summary>Represents <c>,</c> token.</summary>
        CommaToken = 8216,
        /// <summary>Represents <c>&gt;</c> token.</summary>
        GreaterThanToken = 8217,
        /// <summary>Represents <c>.</c> token.</summary>
        DotToken = 8218,
        /// <summary>Represents <c>?</c> token.</summary>
        QuestionToken = 8219,
        /// <summary>Represents <c>#</c> token.</summary>
        HashToken = 8220,
        /// <summary>Represents <c>/</c> token.</summary>
        SlashToken = 8221,


        

        //keywords
        //8300 to 8500
        /// <summary>Represents <see langword="int"/>.</summary>
        IntKeyword = 8309,
        /// <summary>Represents <see langword="if"/>.</summary>
        IfKeyword = 8325,
        /// <summary>Represents <see langword="else"/>.</summary>
        ElseKeyword = 8326,
        /// <summary>Represents <see langword="while"/>.</summary>
        WhileKeyword = 8327,
        /// <summary>Represents <see langword="ref"/>.</summary>
        RefKeyword = 8360,
        /// <summary>Represents <see langword="type"/>.</summary>
        TypeKeyword = 8411,



        // Other
        /// <summary>Represents <see langword="var"/>.</summary>
        VarKeyword = 8490,
        /// <summary>Represents <c>_</c> token.</summary>
        UnderscoreToken = 8491,
        /// <summary>Represents <see langword="of"/>.</summary>
        OfKeyword = 8492,
        /// <summary>Represents <see langword="proc"/>.</summary>
        ProcedureKeyword = 8493,
        /// <summary>Represents <see langword="array"/>.</summary>
        ArrayKeyword = 8494,
        /// <summary>Represents the end of a file.</summary>
        EndOfFileToken = 8496, //NB: this is assumed to be the last textless token

        // tokens with text
        BadToken = 8507,
        IdentifierToken = 8508,
        NumericLiteralToken = 8509,
        CharacterLiteralToken = 8510,

        // trivia
        EndOfLineTrivia = 8539,
        WhitespaceTrivia = 8540,
        SingleLineCommentTrivia = 8541,
        MultiLineCommentTrivia = 8542,

        // names & type-names
        IdentifierName = 8616,

        //###########################################
        //### end of token assignemt
        //###########################################
    }
}
