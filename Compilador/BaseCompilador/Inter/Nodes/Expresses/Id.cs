using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Id : Express
    {
        public int offset;

        public Id(CompilerWord id, Symbols.CompilerType p, int b) : base(id, p)
        {
            offset = b;
        }
    }
}
