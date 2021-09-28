using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Relation : Logical
    {
        public Relation(CompilerToken tok, Express x1, Express x2) : base(tok, x1, x2)
        {

        }

        public new CompilerType Check(CompilerType p1, CompilerType p2)
        {
            if (p1 is Symbols.CompilerArray || p2 is Symbols.CompilerArray )
                return null;
            else if (p1 == p2)
                return Symbols.CompilerType.Bool;
            else
                return null;
        }

        public new void Jump(int t, int f)
        {
            Express a = expr1.Reduce();
            Express b = expr2.Reduce();
            String test = a.ToString() + " " + Op.ToString() + " " + b.ToString();
            EmitJumps(test, t, f);
        }
    }
}
