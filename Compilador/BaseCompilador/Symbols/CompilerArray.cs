using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;

namespace BaseCompilador.Symbols
{
    public class CompilerArray : CompilerType
    {
        public CompilerType of;
        public int size = 1;

        public CompilerArray(int sz, CompilerType p) : base("[]", CompilerTag.INDEX, sz * p.Width)
        {
            size = sz;
            of = p;
        }
        public new string ToString()
        {
            return "[" + size + "]" + of.ToString();
        }
    }
}
