using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Unary : Operation
    {
        public Express expr;

        public Unary(CompilerToken tok, Express x) : base(tok, null)
        {
            expr = x;
            type = Symbols.CompilerType.Max(Symbols.CompilerType.Int, expr.type);
            if (type == null)
                PrintError("type error");
        }

        public override Express Generate()
        {
            return new Unary(Op, expr.Reduce());
        }

        public override String ToString()
        {
            return Op.ToString() + " " + expr.ToString();
        }
    }
}
