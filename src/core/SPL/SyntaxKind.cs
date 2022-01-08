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
        /// <summary>Represents <c>..</c> token.</summary>
        DotDotToken = 8222,

        // additional xml tokens
        /// <summary>Represents <c>/&gt;</c> token.</summary>
        SlashGreaterThanToken = 8232, // xml empty element end
        /// <summary>Represents <c>&lt;/</c> token.</summary>
        LessThanSlashToken = 8233, // element end tag start token
        /// <summary>Represents <c>&lt;!--</c> token.</summary>
        XmlCommentStartToken = 8234, // <!--
        /// <summary>Represents <c>--&gt;</c> token.</summary>
        XmlCommentEndToken = 8235, // -->
        /// <summary>Represents <c>&lt;![CDATA[</c> token.</summary>
        XmlCDataStartToken = 8236, // <![CDATA[
        /// <summary>Represents <c>]]&gt;</c> token.</summary>
        XmlCDataEndToken = 8237, // ]]>
        /// <summary>Represents <c>&lt;?</c> token.</summary>
        XmlProcessingInstructionStartToken = 8238, // <?
        /// <summary>Represents <c>?&gt;</c> token.</summary>
        XmlProcessingInstructionEndToken = 8239, // ?>

        // compound punctuation
        /// <summary>Represents <c>||</c> token.</summary>
        BarBarToken = 8260,
        /// <summary>Represents <c>&amp;&amp;</c> token.</summary>
        AmpersandAmpersandToken = 8261,
        /// <summary>Represents <c>--</c> token.</summary>
        MinusMinusToken = 8262,
        /// <summary>Represents <c>++</c> token.</summary>
        PlusPlusToken = 8263,
        /// <summary>Represents <c>::</c> token.</summary>
        ColonColonToken = 8264,
        /// <summary>Represents <c>??</c> token.</summary>
        QuestionQuestionToken = 8265,
        /// <summary>Represents <c>-&gt;</c> token.</summary>
        MinusGreaterThanToken = 8266,
        /// <summary>Represents <c>!=</c> token.</summary>
        ExclamationEqualsToken = 8267,
        /// <summary>Represents <c>==</c> token.</summary>
        EqualsEqualsToken = 8268,
        /// <summary>Represents <c>=&gt;</c> token.</summary>
        EqualsGreaterThanToken = 8269,
        /// <summary>Represents <c>&lt;=</c> token.</summary>
        LessThanEqualsToken = 8270,
        /// <summary>Represents <c>&lt;&lt;</c> token.</summary>
        LessThanLessThanToken = 8271,
        /// <summary>Represents <c>&lt;&lt;=</c> token.</summary>
        LessThanLessThanEqualsToken = 8272,
        /// <summary>Represents <c>&gt;=</c> token.</summary>
        GreaterThanEqualsToken = 8273,
        /// <summary>Represents <c>&gt;&gt;</c> token.</summary>
        GreaterThanGreaterThanToken = 8274,
        /// <summary>Represents <c>&gt;&gt;=</c> token.</summary>
        GreaterThanGreaterThanEqualsToken = 8275,
        /// <summary>Represents <c>/=</c> token.</summary>
        SlashEqualsToken = 8276,
        /// <summary>Represents <c>*=</c> token.</summary>
        AsteriskEqualsToken = 8277,
        /// <summary>Represents <c>|=</c> token.</summary>
        BarEqualsToken = 8278,
        /// <summary>Represents <c>&amp;=</c> token.</summary>
        AmpersandEqualsToken = 8279,
        /// <summary>Represents <c>+=</c> token.</summary>
        PlusEqualsToken = 8280,
        /// <summary>Represents <c>-=</c> token.</summary>
        MinusEqualsToken = 8281,
        /// <summary>Represents <c>^=</c> token.</summary>
        CaretEqualsToken = 8282,
        /// <summary>Represents <c>%=</c> token.</summary>
        PercentEqualsToken = 8283,
        /// <summary>Represents <c>??=</c> token.</summary>
        QuestionQuestionEqualsToken = 8284,
        /// <summary>Represents <c>!!</c> token.</summary>
        ExclamationExclamationToken = 8285,

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
