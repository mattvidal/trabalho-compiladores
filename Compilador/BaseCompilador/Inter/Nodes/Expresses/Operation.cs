using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Operation : Express
    {
        public Operation(CompilerToken tok, Symbols.CompilerType p) : base(tok, p)
        {
        }

        public new Express Reduce()
        {
            Express e = Generate();
            Temp t = new Temp(type);
            EmitStatement(t.ToString() + " = " + e.ToString());
            return t;
        }
    }
}
