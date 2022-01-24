
# Generating Tokens
This is a complete list of valid tokens generated by the Lexer.

## Textless tokens
|utf-8|SyntaxKind|Description|sample|
|:--:|:--:|:--:|:--:|
|**punctuation**|
|+|PlusToken|-|-|
|-|MinusToken|-|-|
|*|AsterixToken|-|-|
|/|SlashToken|-|-|
|:|ColonToken|-|-|
|;|SemicolonToken|-|-|
|,|ComaToken|-|-|
|<|LessThenToken|-|-|
|>|GreaterThenToken|-|-|
|=|EqualsToken|-|-|
|#|HashToken|-|-|
|(|OpenParenToken|-|-|
|)|CloseParenToken|-|-|
|{|OpenBraceToken|-|-|
|}|CloseBraceToken|-|-|
|[|OpenBracketToken|-|-|
|]|CloseBracketToken|-|-|
|'|SingleQuoteToken|-|-|
|_|UnderscoreToken|-|-|
|**compound punctuation**|
|:=|ColonEqualToken|-|-|
|<=|LessThenEqualsToken|-|-|
|>=|GreaterThenEqualsToken|-|-|
|**keywords**|
|array|ArrayKeyword|represents an array of any type.| `type vector = array [10] of int`|
|if|IfKeyword|open an conditional branch, which is executed only when the condion is matched|`if(a=b){...} else {...}`|
|else|ElseKeyword|use thiys keyword the execute code when the leading `if` condition is not matched. Every `if` has only one `else` keyword.|`if(a=b){...} else {...}`|
|while|WhileKeyword|start a loop which runs so long the condition is true.|`while(true){//do somthing}`|
|proc|ProcedureKeyword|define a sub procedure also know as function.|`proc doSomthing(){//some code }`|
|var|VarKeyword|declare a lokal variable|`var v1: int;`|
|ref|ReferenceKeyword| used to refer to a paramerter by refernce.|`proc add( x:int, y: int, ref result:int){//some code }`|
|of|OfKeyword|used to define the type of an array|`type vector = array [10] of int`|
|int|IntKeyword|define a numerical value between 0 and 2^32 -1| `var x : int;`|
|**other**|
||EndOfFile|Represents the end of a file.|

The **EndOfFile** - Token is assumed to be the last textless token.

</br>

## List of Tokens containing a value.
Folowing the textless tokens. The tokens with a value are listed.
|utf-8|SyntaxKind|Description|sample|
|:--:|:--:|:--:|:--:|
|**tokens with text**|
||BadToken|This is text which cannot be recognized as a token.|-|
|abc</br>_0Abc_1|IdentifierToken|Represents a valid identifier for a `proc`, parameter, field and so on.|`var abc : int;`</br>`proc _0Abc_1 (){}`|
|123</br>0x5F|NumericalLiteralToken|Represents a numerical value. Tha valid range is 0 to 2<sup>32</sup> -1.| `var x:int; x := 123; x:= 0x5F;`|
|'a'|CharacterLiteralToken|Represents a single character. The Character is stored in an `int` type variable. The value Represents the **ASCII** value. |var c:int; c:='a'; c:= '\n';`
|**Trivia**|
|"\r", "\n", "\r\n"|EndOfLineTrivia|Represents the end of a textline.|-|
|" ", "\t"|WhitspaceTrivia|Represents one or more whitespaces. |-|
|//|SingleLineCommentTrivia|Represents a comment. The comment spans over the complete line|`//here goes the comment`|


