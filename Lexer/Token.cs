using System;
using System.Collections.Generic;
using System.Text;

namespace Lexer
{
    class Token
    {
        public string Type { get; set; }
        public string Argument { get; set; }
        public int Index { get; set; }

        public Token(string type, string argument, int index)
        {
            this.Type = type;
            this.Argument = argument;
            this.Index = index;
        }
    }
}
