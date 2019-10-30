using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DFALexer
{
    public class BracketSM : StateMachine
    {
        public ReadOnlyCollection<char> A = new ReadOnlyCollection<char>(new List<char> { '(', ')' }); // alfabet symboli -> nawiasy

        public BracketSM(string stream, int globalIterator) : base(stream, globalIterator)
        {
        }

        public override Q d()
        {
            if (this.A.Contains(Stream[this.LocalIterator]))
            {
                this.LocalIterator++;

                return Q.s2;
            }
            else
            {
                return Q.s2;
            }
        }

        public override string GetTypeOfMachineGeneratedToken()
        {
            return TokenType.Nawias; 
        }
    }
}
