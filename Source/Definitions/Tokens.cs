using System;
using System.Collections.Generic;
using System.Text;

namespace Automav2.Source.Definitions
{

    internal enum TokenType
    {
        // Basic types
        Token_String, // "strings"
        Token_Int, // 123
        Token_Boolean, //true or false
        // Braces and parenthesis
        Token_LBrace, // {
        Token_RBrace, // }
        Token_RParen, // (
        Token_LParen, // )
        // keywords and identifiers
        Token_Identifier,
        Token_Keyword,
        Token_SemiColon, // ;
        // Math Operations
        Token_Add,// +
        Token_Minus, //-
        Token_Divide, // /
        Token_Multiply, // *
        // logical operations
        Token_EqualTo, // ==
        Token_NotEqualTo, // !=
        Token_GreaterThan, // >
        Token_LessThan, // <
        Token_LTE, // <=
        Token_GTE, // >=
        Token_Not // !

    }

    record struct Tokens(TokenType Type, string Content = "")
    {
        public bool ToBool()
            => bool.TryParse(Content, out bool b) ? b : throw new InvalidCastException("Token Content is not a boolean");


        public int ToInt()
            => int.TryParse(Content, out int i) ? i : throw new InvalidCastException("Token Content is not an Integer");
    }

}
