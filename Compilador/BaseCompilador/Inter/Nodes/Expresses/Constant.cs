using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;


namespace BaseCompilador.Inter
{
    public class Constant : Express
    {
        public Constant(CompilerToken tok, Symbols.CompilerType p) : base(tok, p)
        {
        }

        public Constant(int i) : base(new CompilerNumber(i), Symbols.CompilerType.Int)
        {
        }

        public static readonly Constant True    = new Constant(CompilerWord.True,   Symbols.CompilerType.Bool);
        public static readonly Constant False   = new Constant(CompilerWord.False,  Symbols.CompilerType.Bool);

        public new void Jump(int t, int f)
        {
            if (this == True && t != 0)
            {
                EmitStatement("Goto L" + t);
            }
            else if (this == False && f != 0)
            {
                EmitStatement("Goto L" + f);
            }
        }
    }
}
