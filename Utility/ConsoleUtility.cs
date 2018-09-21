using System;

namespace Solidoc.Utility
{
    public static class ConsoleUtility
    {
        public static void WriteException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}