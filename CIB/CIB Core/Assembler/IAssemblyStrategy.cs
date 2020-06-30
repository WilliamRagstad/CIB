using CIB.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core.Assembler
{
    public interface IAssemblyStrategy
    {
        /// <summary>
        /// The target platform that the assembled file will be executable on.
        /// </summary>
        /// <returns>Target platform name</returns>
        string GetTargetPlatformName();
        /// <summary>
        /// Description of what kind of file that will be produced.
        /// </summary>
        /// <returns>Name of executable file type</returns>
        string GetTargetFileType();
        /// <summary>
        /// File extension for the produced file.
        /// </summary>
        /// <returns>File extension</returns>
        string GetTargetFileExtension();

        /// <summary>
        /// Assemble the program source code using the produced AST from the lexer.
        /// </summary>
        /// <param name="ast">Abstract syntax tree to assemble</param>
        /// <returns>Executable program source code</returns>
        byte[] Assemble(Node ast);
    }
}
