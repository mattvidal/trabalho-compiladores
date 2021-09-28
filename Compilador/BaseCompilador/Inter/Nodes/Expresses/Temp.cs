using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Symbols;
using BaseCompilador.Lexer;

namespace BaseCompilador.Inter
{
    public class Temp : Express
    {
        static int count = 0;
        int number = 0;

        public Temp(CompilerType p) : base(CompilerWord.temp, p)
        {
            number = ++count;
        }

        public override string ToString()
        {
            return "t" + number;
        }
    }
}
