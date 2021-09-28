using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Set : Statement
    {
        public Id id;
        public Express expr;

        public Set(Id i, Express x)
        {
            id = i;
            expr = x;
            if (Check(id.type, expr.type) == null)
                PrintError("type error");
        }

        public CompilerType Check(CompilerType p1, CompilerType p2)
        {
            if (CompilerType.IsNumeric(p1) && CompilerType.IsNumeric(p2))
                return p2;
            else if (p1 == CompilerType.Bool && p2 == CompilerType.Bool)
                return p2;
            else
                return null;
        }

        public override void Generate(int b, int a)
        {
            EmitStatement(id.ToString() + " = " + expr.Generate().ToString());
        }
    }
}
