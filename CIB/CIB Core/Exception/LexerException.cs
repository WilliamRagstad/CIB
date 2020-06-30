using CIB.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Exception
{
    internal class LexerException : System.Exception
    {
        public LexerException(string message) : base(message) { }
        public static LexerException UnexpectedToken(string expected, string token, LineColumn lc) => new LexerException("Unexpected token '" + token + "' at " + lc + "! Expected " + expected);

        internal static System.Exception InvalidToken(string reason, string token, LineColumn lc) => new LexerException("Invalid token '" + token + "' at " + lc + "! " + reason);
    }
}
