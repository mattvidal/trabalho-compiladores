using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Symbols;
using BaseCompilador.Lexer;

namespace BaseCompilador.Inter
{
    public class Express : Node
    {
        public CompilerToken Op;
        public CompilerType type;

        public Express (CompilerToken tok, Symbols.CompilerType p)
        {
            Op = tok;
            type = p;
        }

        public virtual Express Generate()
        {
            return this;
        }

        public Express Reduce()
        {
            return this;
        }

        public void EmitJumps(string test, int t, int f)
        {
            if (t != 0 && f != 0)
            {
                EmitStatement("If " + test + " Goto L" + t);
                EmitStatement("Goto L" + f);
            }
            else if (t != 0)
            {
                EmitStatement("If " + test + " Goto L" + t);
            }
            else if (f != 0)
            {
                EmitStatement("IfFalse " + test + " Goto L" + f);
            }
        }

        public override string ToString()
        {
            return Op.ToString();
        }

        public void Jump(int t, int f)
        {
            EmitJumps(ToString(), t, f);
        }

    }
}
