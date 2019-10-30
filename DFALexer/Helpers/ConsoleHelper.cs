using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFALexer
{
    static class ConsoleHelper
    {
        public static void ResetConsoleStyle()
        {
            Console.ResetColor();
        }

        public static void DisplayError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);

            ResetConsoleStyle();
        }

        public static void DisplayTokenListOnConsole(List<Token> tokenList)
        {
            var table = new ConsoleTable("Token", "Type", "Index in stream");

            foreach (var token in tokenList)
            {
                table.AddRow(token.Argument, token.Type, token.Index);
            };

            table.Write();
            Console.WriteLine();
        }
    }
}
