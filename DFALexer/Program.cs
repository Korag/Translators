﻿using System;

namespace DFALexer
{
    class Program
    {
        static void Main(string[] args)
        {
            string phrase = "1 +  - ( - -200 24 53";

            Lexer lex = new Lexer(phrase);
            var tokens = lex.StartLexicalAnalysis();

            ConsoleHelper.DisplayTokenListOnConsole(tokens);

            Console.ReadLine();
        }
    }
}