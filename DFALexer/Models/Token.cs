using System;
using System.Collections.Generic;
using System.Text;

namespace DFALexer
{
    class Token
    {
        public string Type { get; set; }
        public string Argument { get; set; }
        public int Index { get; set; }
    }
}
