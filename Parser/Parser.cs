using System;
using System.Collections.Generic;
using System.Text;
using DFALexer;

namespace Parser
{
    class Parser
    {
        public Lexer l { get; set; } // lekser ma zawierać wypełnioną tablicę symboli
        public int Index { get; set; }


        public Parser(Lexer l)
        {
            this.l = l;
            this.Index = 0;
        }

        public bool LoadSymbol(Token token)
        {
            if (this.l.TokenList[this.Index].Type == token.Type && this.l.TokenList[Index].Argument == token.Argument)
            {
                this.Index++;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Analyze()
        {
            return W();
        }

        public bool W()
        {
            return S() && WI();
        }

        public bool S()
        {
            return C() && SI();
        }

        public bool C()
        {
            if (this.LoadSymbol(new Token(TokenType.Nawias, "(", 0)));
            {
                W() && this.LoadSymbol(new Token(TokenType.Nawias, ")", 0));
            }
        }
    }
}

obliczyć wynik i przekształcić na formę polską