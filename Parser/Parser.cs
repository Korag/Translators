using System;
using System.Collections.Generic;
using System.Text;
using DFALexer;

namespace Parser
{
    public class Parser
    {
        public Lexer Lex { get; set; }
        public int Index { get; set; }


        public Parser(Lexer lex)
        {
            this.Lex = lex;
            this.Index = 0;
        }

        public bool LoadSymbol(Token token)
        {
            if (this.Index == this.Lex.TokenList.Count)
            {
                return false;
            }
            else if (this.Lex.TokenList[this.Index].Type == token.Type)
            {
                this.Index++;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoadSymbolWithConcreteArgument(Token token)
        {
            if (this.Index == this.Lex.TokenList.Count)
            {
                return false;
            }
            else if (this.Lex.TokenList[this.Index].Type == token.Type && this.Lex.TokenList[Index].Argument == token.Argument)
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
            if (this.LoadSymbol(new Token(TokenType.BialyZnak)))
            {
                return C();
            }
            if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Nawias, "(", 0)))
            {
                return W() && this.LoadSymbolWithConcreteArgument(new Token(TokenType.Nawias, ")", 0));
            }
            else if (this.LoadSymbol(new Token(TokenType.Liczba)))
            {
                return true;
            }

            return false;
        }
        public bool WI()
        {
            if (this.LoadSymbol(new Token(TokenType.BialyZnak)))
            {
                return WI();
            }
            else if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Operator, "+", 0)))
            {
                return S() && WI();
            }
            else if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Operator, "-", 0)))
            {
                return S() && WI();
            }

            return true;
        }

        public bool SI()
        {
            if (this.LoadSymbol(new Token(TokenType.BialyZnak)))
            {
                return SI();
            }
            else if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Operator, "*", 0)))
            {
                return C() && SI();
            }
            else if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Operator, "/", 0)))
            {
                return C() && SI();
            }

            return true;
        }
    }
}

