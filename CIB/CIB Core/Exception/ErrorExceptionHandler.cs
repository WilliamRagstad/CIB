using System;
using Console = EzConsole.EzConsole;

namespace CIB.Core.Exception
{
    internal static class ErrorExceptionHandler
    {
        public static void Throw(System.Exception exception) => Console.WriteLine(exception.StackTrace, ConsoleColor.Red);
    }
}
