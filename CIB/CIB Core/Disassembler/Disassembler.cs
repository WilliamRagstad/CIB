using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIB.Core.Disassembler
{
    public class Disassembler
    {
        private readonly string sourceFile;
        private readonly byte[] source;

        public Disassembler(string sourceFile)
        {
            this.sourceFile = sourceFile;
            Util.Util.VertifyFileExists(sourceFile);
            Util.Util.VertifyFileExt(sourceFile, Meta.ExecutableFileExt, Meta.ExecutableFileName);
            this.source = File.ReadAllBytes(sourceFile);
        }

        public void Disassemble(TextWriter outstream)
        {
            outstream.Write("Dissasemble:");
        }
    }
}
