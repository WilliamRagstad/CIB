using CIB.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIB.Core.Assembler
{
    public class Assembler
    {
        private readonly string sourceFile;

        public Assembler(string sourceFile)
        {
            this.sourceFile = sourceFile;
            Util.Util.VertifyFileExists(sourceFile);
            Util.Util.VertifyFileExt(sourceFile, Meta.AssemblyFileExt, Meta.AssemblyFileName);
        }

        public void Assemble(string outputDirectory)
        {
            Lexer lexer = new Lexer(File.OpenRead(sourceFile));
            Parser parser = new Parser(lexer.Tokenize());
            List<byte> code = new List<byte>();



            string infile = Path.GetFileName(sourceFile);
            string outfile = Path.GetFileNameWithoutExtension(sourceFile) + ".cib";
            Console.WriteLine("Successfully assembled " + infile + " to " + outfile);
            File.WriteAllBytes(Path.Combine(outputDirectory, outfile), code.ToArray());
        }
    }
}
