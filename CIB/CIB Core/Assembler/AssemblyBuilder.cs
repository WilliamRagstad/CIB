using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIB.Core.Assembler
{
    public class AssemblyBuilder
    {
        private List<byte[]> code;
        public AssemblyBuilder()
        {
            code = new List<byte[]>();
        }
        public void Add(params byte[] segments) => code.Add(segments);
        public byte[] Code { get
            {
                List<byte> result = new List<byte>();

                for (int i = 0; i < code.Count; i++)
                    for (int j = 0; j < code[i].Length; j++)
                        result.Add(code[i][j]);

                return result.ToArray();
            } }
    }
}
