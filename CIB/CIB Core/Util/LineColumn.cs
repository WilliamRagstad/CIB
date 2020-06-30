using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Util
{
    internal class LineColumn
    {
        public LineColumn()
        {
            line = 0;
            column = 0;
        }

        private int line;
        private int column;

        public int Line { get { return line; } }
        public int Column { get { return column; } }

        public void NextColumn() => column++;
        public void NextLine() { line++; column = 0; }
        public override string ToString() => "line " + line + ", column " + column;
    }
}
