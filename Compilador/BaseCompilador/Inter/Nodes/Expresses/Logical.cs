using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Symbols;
using BaseCompilador.Lexer;

namespace BaseCompilador.Inter
{
    public class Logical : Express
    {
        public Express expr1, expr2;

        public Logical(CompilerToken tok, Express x1, Express x2) : base(tok, null)
        {
            expr1 = x1;
            expr2 = x2;
            type = Check(expr1.type, expr2.type);
            if (type == null)
                PrintError("type error");
        }

        public CompilerType Check(CompilerType p1, CompilerType p2)
        {
            if (p1 == Symbols.CompilerType.Bool && p2 == Symbols.CompilerType.Bool)
                return Symbols.CompilerType.Bool;
            else
                return null;
        }

        public override Express Generate()
        {
            int f = NewLabel();
            int a = NewLabel();
            Temp t = new Temp(type);
            this.Jump(0, f);
            EmitStatement(t.ToString() + " = true");
            EmitStatement("Goto L" + a);
            EmitLabel(f);
            EmitStatement(t.ToString() + " = false");
            EmitLabel(a);
            return t;
        }

        public override string ToString()
        {
            return expr1.ToString() + " " + Op.ToString() + " " + expr2.ToString();
        }
    }
}
