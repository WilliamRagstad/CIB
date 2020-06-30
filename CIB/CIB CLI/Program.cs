using System;
using System.Collections.Generic;
using System.IO;
using ArgumentsUtil;
using CIB.Core;
using CIB.Core.Assembler;
using CIB.Core.Disassembler;
using CIB.Core.VM;

namespace CIB.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { "-a", @"C:\Users\willi\odrive\Google Drive\Programming Projects\- C#\Compact Intermediate Bytecode (CIB)\Examples\hello world.casm" };

            ArgumentsTemplate argumentsTemplate = GetArgumentsTemplate();
            Arguments arguments = Arguments.Parse(args, (char)KeySelector.Linux);
            if (arguments.Length == 0 || arguments.Keyless.Contains("help")) { argumentsTemplate.ShowManual(HelpFormatting.ShowVersion); return; }

            try
            {
                if (arguments.ContainsKey("a"))
                {
                    // Assemble .casm files
                    VertifyArrayIsPopulated(arguments["a"], "files", "assemble");
                    for (int i = 0; i < arguments["a"].Length; i++) new Assembler(arguments["a"][i]).Assemble(
                        arguments.ContainsKey("out") ?
                            arguments["out"][0] :
                            Path.GetDirectoryName(arguments["a"][i])
                        );
                }
                else if (arguments.ContainsKey("d"))
                {
                    // Assemble .casm files
                    VertifyArrayIsPopulated(arguments["d"], "files", "disassemble");
                    for (int i = 0; i < arguments["d"].Length; i++) new Disassembler(arguments["a"][i]).Disassemble(Console.Out);
                }
                else
                {
                    // Execute .cib files
                    for (int i = 0; i < arguments.Keyless.Count; i++) new Interpreter(arguments.Keyless[i]).Interpret();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static ArgumentsTemplate GetArgumentsTemplate()
        {
            List<ArgumentOption> options = new List<ArgumentOption>();
            // Add parameter -p for parallelization
            options.Add(new ArgumentOption("a", "Creates an executable (" + Meta.ExecutableFileExt + ") file from " + Meta.AssemblyFileName + " (" + Meta.AssemblyFileExt + ") files", new List<ArgumentParameter> {
                new ArgumentParameter("files", typeof(string), "A list of one or more files", false),
                new ArgumentParameter("-out [folder]", typeof(string), "Read below", true)
            }));
            options.Add(new ArgumentOption("out", "Output folder for the assembled " + Meta.AssemblyFileName + " (" + Meta.AssemblyFileExt + ") files", new List<ArgumentParameter> {
                new ArgumentParameter("folder", typeof(string), "The target output folder", false)
            }));
            options.Add(new ArgumentOption("d", "Disassemble a " + Meta.ExecutableFileName + " (" + Meta.ExecutableFileExt + ") file", new List<ArgumentParameter> {
                new ArgumentParameter("file", typeof(string), "File to disassemble", false)
            }));

            List <ArgumentCommand> commands = new List<ArgumentCommand>();
            commands.Add(new ArgumentCommand("(files)", "List of (" + Meta.ExecutableFileExt + ") files to interpret"));

            return new ArgumentsTemplate(options, false, commands, false, null, "CIB", (char)KeySelector.Linux, Meta.Version);
        }

        #region Util
        static void VertifyArrayIsPopulated<T>(List<T> array, string unit, string action) => VertifyArrayIsPopulated(array.ToArray(), unit, action);
        static void VertifyArrayIsPopulated(Array array, string unit, string action)
        {
            if (array.Length == 0) throw new ArgumentException("Please provide any " + unit + " to " + action + "!");
        }
        #endregion
    }
}
