using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Not : Logical
    {
        public Not(CompilerToken tok, Express x2) : base(tok, x2, x2)
        {

        }

        public new void Jump(int t, int f)
        {
            expr2.Jump(f, t);
        }

        public override string ToString ()
        {
            return Op.ToString() + " " + expr2.ToString();
        }
    }
}
