using DFALexer;
using System;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string phrase = "1 + 3 * 5";

            Lexer lex = new Lexer(phrase);
            var tokens = lex.StartLexicalAnalysis();

            ConsoleHelper.DisplayTokenListOnConsole(tokens);

            Parser pars = new Parser(lex);
            var parserResult = pars.Analyze();

            Console.WriteLine(parserResult);

            Console.ReadLine();
        }
    }
}
