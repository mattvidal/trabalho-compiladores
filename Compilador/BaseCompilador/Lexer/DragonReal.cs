using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompilador.Lexer
{
    public class CompilerReal : CompilerToken
    {
        public readonly float Value;

        public CompilerReal(float v) : base(CompilerTag.REAL)
        {
            Value = v;
        }

        public new string ToString()
        {
            return ("" + Value);
        }
    }
}
