using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompilador.Lexer
{
    public class CompilerNumber : CompilerToken
    {
        public readonly int Value;

        public CompilerNumber(int v) : base(CompilerTag.NUM)
        {
            Value = v;
        }

        public new string ToString()
        {
            return ("" + Value);
        }
    }
}
