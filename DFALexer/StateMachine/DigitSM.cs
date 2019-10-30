using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DFALexer
{
    public class DigitSM : StateMachine
    {
        public ReadOnlyCollection<char> A = new ReadOnlyCollection<char>(new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }); // alfabet symboli -> cyfry

        public DigitSM(string stream, int globalIterator) : base(stream, globalIterator)
        {
        }

        public override Q d()
        {
            if (this.Stream.Length > this.LocalIterator && this.Stream[this.LocalIterator] == '-')
            {
                if (this.Stream.Length > this.LocalIterator + 1)
                {
                    if (this.A.Contains(Stream[this.LocalIterator + 1]))
                    {
                        this.LocalIterator++;

                        return Q.s1;
                    }

                    return Q.s2;
                }

                return Q.s2;
            }
            else if (this.Stream.Length > this.LocalIterator && this.A.Contains(Stream[this.LocalIterator]))
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
            return TokenType.Liczba;
        }
    }
}
