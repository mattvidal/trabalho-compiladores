using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompilador.Lexer
{
    public class CompilerToken
    {
        public readonly int tag;

        public CompilerToken(int t)
        {
            tag = t;
        }

        public new string ToString()
        {
            return ("" + (char)tag);
        }
    }
}
