using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class While : Statement
    {
        Express expr;
        Statement stmt;

        public While()
        {
            expr = null;
            stmt = null;
        }

        public void Init(Express x, Statement s)
        {
            expr = x;
            stmt = s;
            if (expr.type != CompilerType.Bool)
                expr.PrintError("boolean required in while");
        }

        public override void Generate(int b, int a)
        {
            After = a;                // save label a
            expr.Jump(0, a);
            int label = NewLabel();   // label for stmt
            EmitLabel(label);
            stmt.Generate(label, b);
            EmitStatement("goto L" + b);
        }
    }
}
