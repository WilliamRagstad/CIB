using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Data
{
    public enum TokenType
    {
        Instruction,
        Register,
        Variable,
        Directive,
        Literal,
        String,
        Char,
        FunctionDeclaration,
        FunctionBody,
        Operation,
        Number,
        Label,
        Comment
    }
    public class Token
    {
        public Token(TokenType type, string data)
        {
            Type = type;
            Data = data;
        }

        public TokenType Type { get; }
        public string Data { get; }

        public override string ToString() => Enum.GetName(typeof(TokenType), Type) + (!string.IsNullOrEmpty(Data) ? ": " + Data : "");
    }
}
