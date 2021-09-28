using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Arith : Operation
    {
        public Express expr1, expr2;

        public Arith(CompilerToken tok, Express x1, Express x2) : base(tok, null)
        {
            expr1 = x1;
            expr2 = x2;
            type = Symbols.CompilerType.Max(expr1.type, expr2.type);
            if (type == null)
                PrintError("type error");
        }

        public override Express Generate()
        {
            return new Arith(Op, expr1.Reduce(), expr2.Reduce());
        }

        public override String ToString()
        {
            return expr1.ToString() + " " + Op.ToString() + " " + expr2.ToString();
        }
    }
}
