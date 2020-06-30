using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Exception
{
    internal class ParseException : System.Exception
    {
        public ParseException(string message) : base(message) { }
    }
}
