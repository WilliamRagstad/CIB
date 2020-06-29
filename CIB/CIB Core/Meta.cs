using System;
using System.Collections.Generic;
using System.Text;

namespace CIB.Core
{
    public static class Meta
    {
        public static Version Version = new Version(0, 1, 1);
        public static string AssemblyFileExt = ".casm";
        public static string AssemblyFileName = "Compact Assembly";
        public static string ExecutableFileExt = ".cxe";
        public static string ExecutableFileName = "Compact Executable";
        public static string IntermediateBytecodeExt = ".cib";
        public static string IntermediateBytecodeName = "Compact Intermediate Byte-Code";
    }
}
