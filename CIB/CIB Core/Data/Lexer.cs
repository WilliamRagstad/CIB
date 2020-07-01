using CIB.Core.Exception;
using CIB.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CIB.Core.Data
{
    public class Lexer
    {
        private readonly StreamReader sourceStream;

        public Lexer(StreamReader instream)
        {
            this.sourceStream = instream;
        }

        private LineColumn lc;
        private List<Token> tokens;
        private string ct = string.Empty; // Current Token
        public Token[] Tokenize()
        {
            lc = new LineColumn();
            tokens = new List<Token>();

            bool isBody = false,
                isInstruction = false,
                isEOF = false;

            int r;
            char cc; // Current Character
            while (true)
            {
                r = sourceStream.Read();
                cc = (char)r;

                if (r == -1)
                {
                    cc = '\r'; // End of file is represented as a clear line
                    isEOF = true;
                }

                lc.NextColumn();

                switch (cc)
                {
                    case ' ':
                    case '\n':
                    case '\t':
                    case '\r':
                        addPrevToken(isBody, ref isInstruction);

                        if (cc == '\n')
                        {
                            lc.NextLine();
                            isInstruction = true;
                        }
                        else if (isEOF) break;

                        continue;
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '&':   //Bitwise and
                    case '|':   // or
                    case '^':   // xor
                        addPrevToken(isBody, ref isInstruction);
                        addToken(TokenType.Operation, cc);
                        continue;
                    case '.':
                        if (!isBody) { readWord(); addToken(TokenType.Directive); }
                        else { addToken(TokenType.Variable); }
                        isBody = false;
                        isInstruction = false;
                        continue;
                    case '$':
                        if (isBody) { readRegister(); addToken(TokenType.Register); }
                        continue;
                    case '"':
                        readString();
                        addToken(TokenType.String);
                        continue;
                    case '\'':
                        readChar();
                        addToken(TokenType.Char);
                        continue;
                    case ':':
                        if (isBody) addToken(TokenType.Label);
                        continue;
                    case ';':
                        readComment();
                        addToken(TokenType.Comment);
                        continue;
                    case '(':
                        addToken(TokenType.FunctionDeclaration);
                        isBody = false;
                        isInstruction = false;
                        continue;
                    case ')':
                        if (ct.Length > 0) addToken(TokenType.Literal); // Add the last argument name
                        addToken(TokenType.FunctionBody);               // Add token for start of function body
                        isBody = true;
                        isInstruction = true;
                        continue;
                    default:
                        if (ct.Length == 0 && char.IsDigit(cc)) { readNumber(cc); addToken(TokenType.Number); continue; }

                        break;
                }

                if (isEOF) break;
                ct += cc;
            }

             return tokens.ToArray();
        }


        #region Lexer Functions
        void addToken(TokenType type)
        {
            tokens.Add(new Token(type, ct));
            ct = string.Empty;
        }

        void addToken(TokenType type, string data)
        {
            tokens.Add(new Token(type, data));
        }
        void addToken(TokenType type, char data) => addToken(type, data.ToString());

        void addPrevToken(bool isBody, ref bool isInstruction)
        {
            if (ct.Length > 0)
            {
                if (isBody && isInstruction) { addToken(TokenType.Instruction); isInstruction = false; }
                else addToken(TokenType.Literal);
            }
        }

        private void readWord()
        {
            while (char.IsLetter((char)sourceStream.Peek())) ct += (char)sourceStream.Read();
        }

        private void readNumber(char start)
        {
            ct += start;

            bool next = true;
            char c;
            do
            {
                if (sourceStream.Peek() == -1) return;
                c = (char)sourceStream.Read();
                if (!(
                    char.IsDigit(c) ||
                    c == 'x' ||
                    c == 'b' ||
                    c == 'f'
                    )) next = false;
                if (next) ct += c;
            }
            while (next);
        }

        private void readRegister()
        {
            Int16 depth = 0;
            char c;
            while(true)
            {
                if (sourceStream.Peek() == -1) return;
                c = (char)sourceStream.Read();
                ct += c;
                switch (c)
                {
                    case '[':
                        depth++;
                        break;
                    case ']':
                        depth--;
                        if (depth == 0) return;
                        break;
                }
            }
        }

        private void readComment()
        {
            char c;
            while (true)
            {
                if (sourceStream.Peek() == -1) return;
                c = (char)sourceStream.Read();
                if (c == '\n' || c == '\r') break;
                else if (ct.Length == 0 && (c == ' ' || c == '\t')) continue;
                
                ct += c;
            }
        }

        private void readTillNextChar(char c)
        {
            int p;
            while (true)
            {
                p = sourceStream.Read();
                if (p == -1) return;
                else if (p == c) break;
                ct += (char)p;
            }
        }
        private void readChar()
        {
            readTillNextChar('\'');
            if (ct.Length != 1) ErrorExceptionHandler.Throw(LexerException.InvalidToken("Can only store one char", ct, lc));
        }

        private void readString() => readTillNextChar('"');

        #endregion
    }
}
