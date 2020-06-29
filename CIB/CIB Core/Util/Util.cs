using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CIB.Core.Util
{
    internal static class Util
    {
        public static void VertifyFileExt(string file, string ext, string extFullName)
        {
            bool match = Path.GetExtension(file).ToUpper().Equals(ext.ToUpper());
            if (!match) throw new ArgumentException("The file '" + file + '\'' + " must be a " + extFullName + " (." + ext.ToLower() + ") file!");
        }
        public static void VertifyFileExists(string file)
        {
            if (!File.Exists(file)) throw new ArgumentException("The file '" + file + '\'' + " does not exist!");
        }
    }
}
