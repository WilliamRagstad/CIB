using CIB.Core.Assembler.CXE;
using CIB.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Console = EzConsole.EzConsole;

namespace CIB.Core.Assembler
{
    public class Assembler
    {
        private readonly string sourceFile;
        private readonly IAssemblyStrategy strategy;

        public Assembler(string sourceFile) : this(sourceFile, new CXEAssemblyStrategy()) { }
        public Assembler(string sourceFile, IAssemblyStrategy strategy)
        {
            this.sourceFile = sourceFile;
            this.strategy = strategy;
            Util.Util.VertifyFileExists(sourceFile);
            Util.Util.VertifyFileExt(sourceFile, Meta.AssemblyFileExt, Meta.AssemblyFileName);
        }

        public void Assemble(string outputDirectory)
        {
            Lexer lexer = new Lexer(File.OpenText(sourceFile));
            Parser parser = new Parser(lexer.Tokenize());
            Node ast = parser.Parse();
            Console.WriteLine("Building: " + strategy.GetTargetFileType());
            Console.WriteLine("Architecture: " + strategy.GetTargetPlatformName());
            byte[] source = strategy.Assemble(ast); // Handle AST
            string infile = Path.GetFileName(sourceFile);
            string outfile = Path.GetFileNameWithoutExtension(sourceFile) + strategy.GetTargetFileExtension();
            Console.WriteLine("Successfully assembled " + infile + " to " + outfile);
            File.WriteAllBytes(Path.Combine(outputDirectory, outfile), source);
        }
    }
}
