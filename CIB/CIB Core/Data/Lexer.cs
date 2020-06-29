using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIB.Core.Data
{
    public class Lexer
    {
        #region Declarations

        public const int EOF = 1;

        #endregion

        private readonly Stream sourceStream;

        public Lexer(Stream instream)
        {
            this.sourceStream = instream;
        }

        #region Helper Functions


        internal static bool IsDigit(char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        #endregion

        public Token[] Tokenize()
        {
            List<Token> tokens = new List<Token>();


            return tokens.ToArray();
        }
    }
}
