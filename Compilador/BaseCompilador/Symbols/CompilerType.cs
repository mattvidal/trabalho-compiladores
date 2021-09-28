using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;

namespace BaseCompilador.Symbols
{
    public class CompilerType : CompilerWord
    {
        public int Width = 0;

        public CompilerType(string s, int tag, int w) : base(s, tag)
        {
            Width = w;
        }

        public static readonly CompilerType Int     = new CompilerType("int",   CompilerTag.BASIC, 4),
                                    Float   = new CompilerType("float", CompilerTag.BASIC, 8),
                                    Char    = new CompilerType("char",  CompilerTag.BASIC, 1),
                                    Bool    = new CompilerType("bool",  CompilerTag.BASIC, 1);

        public static Boolean IsNumeric (CompilerType p)
        {
            if (p == Char || p == Float || p == Int)
                return true;
            else
                return false;
        }

        public static CompilerType Max(CompilerType p1, CompilerType p2)
        {
            if (!(IsNumeric(p1)) || !(IsNumeric(p2)))
                return null;
            else if (p1 == Float || p2 == Float)
                return Float;
            else if (p1 == Int || p2 == Int)
                return Int;
            else
                return Char;
        }
    }
}
