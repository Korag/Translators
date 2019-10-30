using System;
using System.Collections.Generic;
using System.Text;

namespace Lexer
{
    class Lexer
    {
        public List<Token> TokenList { get; set; }


        public Lexer()
        {
            this.TokenList = new List<Token>();
        }

        public bool S(string stream, int startPosition)
        {
            return B(stream, startPosition);
        }

        public bool B(string stream, int startPosition)
        {
            if (stream[startPosition] == ' ' && B(stream, startPosition+1))
            {
                return true;
            }
            else if (stream[startPosition] == ' ')
            {
                return true;
            }
            else
            {
                return false;
            }

            // brakuje tutaj dopisywania symboli do kolekcji
            // kolejne reguly analogicznie na zasadzie operacji OR
            // trzeba zapętlić dla całości 
        }

        public bool O(string stream, int startPosition)
        {
            if (stream[startPosition] == '+' || stream[startPosition] == '-' || stream[startPosition] == '*' || stream[startPosition] == '/')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool N(string stream, int startPosition)
        {
            if (stream[startPosition] == '(' || stream[startPosition] == ')')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
