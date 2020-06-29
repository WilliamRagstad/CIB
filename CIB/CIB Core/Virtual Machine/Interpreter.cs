using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIB.Core.VM
{
    public class Interpreter
    {
        private readonly string sourceFile;
        private readonly byte[] source;

        public Interpreter(string sourceFile)
        {
            this.sourceFile = sourceFile;
            Util.Util.VertifyFileExists(sourceFile);
            Util.Util.VertifyFileExt(sourceFile, Meta.ExecutableFileExt, Meta.ExecutableFileName);
            this.source = File.ReadAllBytes(sourceFile);
        }

        public void Interpret()
        {

        }
    }
}
