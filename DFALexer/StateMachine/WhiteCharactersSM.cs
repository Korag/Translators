using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DFALexer
{
    public class WhiteCharactersSM : StateMachine
    {
        public ReadOnlyCollection<char> A = new ReadOnlyCollection<char>(new List<char> { ' ' }); // alfabet symboli -> white characters

        public WhiteCharactersSM(string stream, int globalIterator) : base(stream, globalIterator)
        {
        }

        public override Q d()
        {
            if (this.Stream.Length > this.LocalIterator && this.A.Contains(Stream[LocalIterator]))
            {
                this.LocalIterator++;

                return Q.s1;
            }
            else
            {
                return Q.s2;
            }
        }

        public override string GetTypeOfMachineGeneratedToken()
        {
            return TokenType.BialyZnak;
        }
    }
}
