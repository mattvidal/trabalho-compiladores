using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class SetElement : Statement
    {
        public Id array;
        public Express index;
        public Express expr;

        public SetElement(Access x, Express y)
        {
            array = x.array;
            index = x.index;
            expr = y;
            if (Check(x.type, expr.type) == null)
                PrintError("type error");
        }

        public CompilerType Check(CompilerType p1, CompilerType p2)
        {
            if (p1 is CompilerArray || p2 is CompilerArray )
                return null;
            else if (p1 == p2)
                return p2;
            else if (CompilerType.IsNumeric(p1) && CompilerType.IsNumeric(p2))
                return p2;
            else
                return null;
        }

        public override void Generate(int b, int a)
        {
            string s1 = index.Reduce().ToString();
            string s2 = expr.Reduce().ToString();
            EmitStatement(array.ToString() + " [ " + s1 + " ] = " + s2);
        }
    }
}
