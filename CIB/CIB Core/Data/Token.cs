using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Data
{
    public enum TokenType
    {
        Instruction,
        Type,
        Register,
        Variable,
        Constant,
        Directive
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
    }
}
