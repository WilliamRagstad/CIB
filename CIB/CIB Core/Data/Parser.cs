using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Data
{
    class Parser
    {
        private readonly Token[] tokens;

        public Parser(Token[] tokens)
        {
            this.tokens = tokens;
        }
    }
}
