using CIB.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Assembler.CXE
{
    internal class CXEAssemblyStrategy : IAssemblyStrategy
    {
        public string GetTargetPlatformName() => "Cross-platform";
        public string GetTargetFileType() => "Executable CIB file";
        public string GetTargetFileExtension() => Meta.ExecutableFileExt;
        public byte[] Assemble(Node ast)
        {
            CXEAssemblyBuilder ab = new CXEAssemblyBuilder();

            throw new NotImplementedException();

            return ab.Code;
        }

    }
}
