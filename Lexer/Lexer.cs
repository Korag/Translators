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

        public AnalyzeResult S(string stream, int startPosition)
        {
            AnalyzeResult result = new AnalyzeResult();

            if ((result = B(stream, startPosition)).Result)
            {
                return result;
            }
            if ((result = O(stream, startPosition)).Result)
            {
                return result;
            }
            if ((result = N(stream, startPosition)).Result)
            {
                return result;
            }
            if ((result = L(stream, startPosition)).Result)
            {
                return result;
            }

            return result;
        }

        public AnalyzeResult B(string stream, int startPosition)
        {
            AnalyzeResult result = new AnalyzeResult();
            AnalyzeResult additionalResult = new AnalyzeResult();

            if (stream[startPosition] == ' ' && (additionalResult = B(stream, startPosition+1)).Result)
            {
                result.Result = true;
                result.Token = new Token(TokenType.BialyZnak, " " + additionalResult.Token.Argument, startPosition);

                return result;
            }
            else if (stream[startPosition] == ' ')
            {
                result.Result = true;
                result.Token = new Token(TokenType.BialyZnak, " ", startPosition);

                return result;
            }
            else
            {
                result.Result = false;

                return result;
            }
        }

        public AnalyzeResult O(string stream, int startPosition)
        {
            AnalyzeResult result = new AnalyzeResult();
            AnalyzeResult additionalResult = new AnalyzeResult();

            if (stream[startPosition] == '+' || stream[startPosition] == '-' || stream[startPosition] == '*' || stream[startPosition] == '/')
            {
                result.Result = true;
                result.Token = new Token(TokenType.Operator, stream[startPosition].ToString(), startPosition);

                return result;
            }
            else
            {
                result.Result = false;

                return result;
            }
        }

        public AnalyzeResult N(string stream, int startPosition)
        {
            AnalyzeResult result = new AnalyzeResult();
            AnalyzeResult additionalResult = new AnalyzeResult();

            if (stream[startPosition] == '(' || stream[startPosition] == ')')
            {
                result.Result = true;
                result.Token = new Token(TokenType.Nawias, stream[startPosition].ToString(), startPosition);

                return result;
            }
            else
            {
                result.Result = false;

                return result;
            }
        }

        public AnalyzeResult L(string stream, int startPosition)
        {
            AnalyzeResult result = new AnalyzeResult();
            AnalyzeResult additionalResult = new AnalyzeResult();

            if (stream[startPosition] == '-' && (additionalResult = W(stream, startPosition + 1)).Result)
            {
                result.Result = true;
                result.Token = new Token(TokenType.Liczba, "-" + additionalResult.Token.Argument, startPosition);

                return result;
            }
            else if ((additionalResult = W(stream, startPosition)).Result)
            {
                result.Result = true;
                result.Token = new Token(TokenType.Liczba, additionalResult.Token.Argument, startPosition);

                return result;
            }
            else
            {
                result.Result = false;

                return result;
            }
        }

        public AnalyzeResult W(string stream, int startPosition)
        {
            AnalyzeResult result = new AnalyzeResult();
            AnalyzeResult additionalResult = new AnalyzeResult();

            if ("123456789".Contains(stream[startPosition]) && (additionalResult = W(stream, startPosition + 1)).Result)
            {
                result.Result = true;
                result.Token = new Token(TokenType.Liczba, additionalResult.Token.Argument, startPosition);

                return result;
            }
            else if ("123456789".Contains(stream[startPosition]))
            {
                result.Result = true;
               
                result.Token = new Token(TokenType.Liczba, stream[startPosition].ToString(), startPosition);

                return result;
            }
            else
            {
                result.Result = false;

                return result;
            }
        }

        public bool Analyze(string query)
        {
            int i = 0;
            AnalyzeResult result = new AnalyzeResult();

            while (i < query.Length && (result = S(query, i)).Result)
            {
                this.TokenList.Add(result.Token);
                i = i + result.Token.Argument.Length; 
            }

            return result.Result;
        }

    }
}
