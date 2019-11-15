using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DFALexer;

namespace Parser
{
    public class Parser
    {
        public Lexer Lex { get; set; }
        public int Index { get; set; }

        public Stack<string> OperatorsStack = new Stack<string>();
        public List<string> ONP { get; set; } = new List<string>();

        public ReadOnlyCollection<string> OperatorsSet = new ReadOnlyCollection<string>(new List<string> { "-", "+", "*", "/" });

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

        public int CalculateSumONP()
        {
            Stack<string> digitsStack = new Stack<string>();
            int sum = 0;

            foreach (var symbol in ONP)
            {
                if (OperatorsSet.Contains(symbol))
                {
                    int valueA = Int32.Parse(digitsStack.Pop());
                    int valueB = Int32.Parse(digitsStack.Pop());
                    int result = 0;

                    if (symbol == "+")
                        result = valueB + valueA;

                    else if (symbol == "-")
                        result = valueB - valueA;

                    else if (symbol == "*")
                        result = valueB * valueA;

                    else if (symbol == "/")
                        result = valueB / valueA;

                    digitsStack.Push(result.ToString());
                }
                else
                {
                    digitsStack.Push(symbol);
                }     
            }

            sum = Int32.Parse(digitsStack.Pop());

            return sum;
        }

        public bool Analyze()
        {
            bool analyzeResult = W();

            Console.Write("Wynik parsowania: ");

            if (analyzeResult)
            {
                string onpString = "";

                if (OperatorsStack.Count != 0)
                {
                    for (int i = 0; i <= OperatorsStack.Count; i++)
                    {
                        string singleOperator = OperatorsStack.Pop();
                        this.ONP.Add(singleOperator);
                    }
                }

                foreach (var symbol in ONP)
                {
                    onpString += symbol + " ";
                }

                int onpSum = CalculateSumONP();

                Console.WriteLine("pozytywny");
                Console.WriteLine("-------------------");

                Console.WriteLine("Reprezentacja ONP: ");
                Console.WriteLine(onpString);
                Console.WriteLine("-------------------");

                Console.Write("Wartość wyrażenia ONP: ");
                Console.WriteLine(onpSum);
            }
            else
            {
                Console.WriteLine("negatywny");
            }

            return analyzeResult;
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
                OperatorsStack.Push(this.Lex.TokenList[this.Index - 1].Argument);

                bool resultW = W();
                bool resultRightBracket = this.LoadSymbolWithConcreteArgument(new Token(TokenType.Nawias, ")", 0));

                if (resultRightBracket)
                {
                    while (OperatorsStack.Peek().ToString() != "(")
                    {
                        string singleOperator = OperatorsStack.Pop();

                        this.ONP.Add(singleOperator);
                    }
                    if (OperatorsStack.Peek().ToString() == "(")
                    {
                        OperatorsStack.Pop();
                    }
                }

                return resultW && resultRightBracket;
            }
            else if (this.LoadSymbol(new Token(TokenType.Liczba)))
            {
                this.ONP.Add(this.Lex.TokenList[this.Index - 1].Argument);

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
                while (OperatorsStack.Count != 0 && OperatorsStack.Peek().ToString() != "*" && OperatorsStack.Peek().ToString() != "/")
                {
                    string singleOperator = OperatorsStack.Pop();
                    this.ONP.Add(singleOperator);
                }

                OperatorsStack.Push(this.Lex.TokenList[this.Index - 1].Argument);

                return S() && WI();
            }
            else if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Operator, "-", 0)))
            {
                while (OperatorsStack.Count != 0 && OperatorsStack.Peek().ToString() != "*" && OperatorsStack.Peek().ToString() != "/")
                {
                    string singleOperator = OperatorsStack.Pop();
                    this.ONP.Add(singleOperator);
                }

                OperatorsStack.Push(this.Lex.TokenList[this.Index - 1].Argument);

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
                while (OperatorsStack.Count != 0 && (OperatorsStack.Peek().ToString() == "*" || OperatorsStack.Peek().ToString() == "/"))
                {
                    string singleOperator = OperatorsStack.Pop();
                    this.ONP.Add(singleOperator);
                }

                OperatorsStack.Push(this.Lex.TokenList[this.Index - 1].Argument);

                return C() && SI();
            }
            else if (this.LoadSymbolWithConcreteArgument(new Token(TokenType.Operator, "/", 0)))
            {
                while (OperatorsStack.Count != 0 && (OperatorsStack.Peek().ToString() == "*" || OperatorsStack.Peek().ToString() == "/"))
                {
                    string singleOperator = OperatorsStack.Pop();
                    this.ONP.Add(singleOperator);
                }

                OperatorsStack.Push(this.Lex.TokenList[this.Index - 1].Argument);

                return C() && SI();
            }

            return true;
        }
    }
}

