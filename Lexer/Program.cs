using System;
using System.Linq;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer l = new Lexer();
            string query = "2 + 12";

            if (l.Analyze(query))
            {
                Console.WriteLine("Zakończonoe analizę leksykalną z sukcesem");
                Console.WriteLine("Rozpoznane leksemy: ");

                foreach (var token in l.TokenList)
                {
                    Console.WriteLine("<{0}, {1}>", token.Type, token.Argument);
                }
            }
            else
            {
                int position = 0;

                if (l.TokenList.Count > 0)
                {
                    position = l.TokenList.Last<Token>().Index + l.TokenList.Last<Token>().Argument.Length;
                }

                string chain = query.Substring(position, query.Length - position > 10 ? 10 : query.Length - position);

                Console.WriteLine("Błąd analizy leksykalnej (na pozycji {0} nierozpoznany łańcuch znaków {1}...)", position, chain);
            }

            Console.ReadLine();
        }
    }
}
