using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFALexer
{
    public class Lexer
    {
        public List<Token> TokenList { get; set; } = new List<Token>();

        public string Stream { get; set; }
        public int GlobalIterator { get; set; }

        //StateMachines
        public BracketSM bracketSM { get; set; }
        public DigitSM digitSM { get; set; }
        public OperatorSM operatorSM { get; set; }
        public WhiteCharactersSM whiteCharactersSM { get; set; }

        public List<StateMachine> stateMachines { get; set; }

        public Lexer(string stream)
        {
            this.Stream = stream;
            this.GlobalIterator = 0;

            this.bracketSM = new BracketSM(stream, GlobalIterator);
            this.digitSM = new DigitSM(stream, GlobalIterator);
            this.operatorSM = new OperatorSM(stream, GlobalIterator);
            this.whiteCharactersSM = new WhiteCharactersSM(stream, GlobalIterator);

            this.stateMachines = new List<StateMachine>();

            stateMachines.Add(bracketSM);
            stateMachines.Add(digitSM);
            stateMachines.Add(operatorSM);
            stateMachines.Add(whiteCharactersSM);
        }

        public List<Token> StartLexicalAnalysis()
        {
            while (this.GlobalIterator != Stream.Length)
            {
                foreach (var stateMachine in this.stateMachines)
                {
                    stateMachine.Analyze();
                }

                var results = this.stateMachines.Where(z => z.HasMatch).OrderByDescending(z => z.Match.Length).ToList();

                if (results.Count() != 0)
                {
                    Token token = new Token(
                        results.FirstOrDefault().GetTypeOfMachineGeneratedToken(),
                        results.FirstOrDefault().Match,
                        this.GlobalIterator
                        );
    
                    TokenList.Add(token);

                    this.GlobalIterator += token.Argument.Length;
                }
                else
                {
                    string errorMessage = string.Format("Nierozpoznano znaku na pozycji: {0}", this.GlobalIterator);
                    ConsoleHelper.DisplayError(errorMessage);

                    this.GlobalIterator++;
                }

                foreach (var stateMachine in this.stateMachines)
                {
                    stateMachine.ResetStateMachine(this.GlobalIterator);
                }
            }

            return TokenList;
        }
    }
}
