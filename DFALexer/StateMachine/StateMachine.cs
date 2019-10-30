using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DFALexer
{
    public abstract class StateMachine
    {
        public string Stream { get; set; }
        public int GlobalIterator { get; set; }
        public int LocalIterator { get; set; }

        public string Match { get; set; }
        public bool HasMatch { get; set; }

        public enum Q // zbiór dopuszczalnych stanów
        {
            s0, // Stan początkowy
            s1, // Stan końcowy akceptujący
            s2, // Error - stan nieakceptujący
        }

        public Q q; // stan bieżący

        public const Q q0 = Q.s0; // stan początkowy

        public ReadOnlyCollection<Q> F = new ReadOnlyCollection<Q>(new List<Q> { Q.s1 });

        public StateMachine(string stream, int globalIterator)
        {
            this.Stream = stream;

            this.GlobalIterator = globalIterator;
            this.LocalIterator = this.GlobalIterator;

            this.q = StateMachine.q0; // ustalenie stanu początkowego
        }

        public abstract Q d();
        public abstract string GetTypeOfMachineGeneratedToken();

        public void StateMachineHasMatch()
        {
            if (this.GlobalIterator == this.LocalIterator)
            {
                this.HasMatch = false;
            }
            else
            {
                this.HasMatch = true;
            }
        }

        public void Analyze()
        {
            while (this.q != Q.s2)
            {
                this.q = d();
            }

           StateMachineHasMatch();

            if (this.HasMatch)
            {
                this.Match = Stream.Substring(this.GlobalIterator, this.LocalIterator - this.GlobalIterator);
            }
        }

        public void ResetStateMachine(int globalIterator)
        {
            this.GlobalIterator = globalIterator;
            this.LocalIterator = this.GlobalIterator;

            this.Match = null;
            this.HasMatch = false;

            this.q = Q.s0;
        }
    }
}
