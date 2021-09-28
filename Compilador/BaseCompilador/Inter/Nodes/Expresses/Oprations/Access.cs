using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Access : Operation
    {
        public Id array;
        public Express index;

        public Access(Id a, Express i, CompilerType p) : base(new CompilerWord("[]", CompilerTag.INDEX), p)
        {
            array = a;
            index = i;
        }

        public override Express Generate()
        {
            return new Access(array, index.Reduce(), type);
        }

        public new void Jump(int t, int f)
        {
            EmitJumps(Reduce().ToString(), t, f);
        }

        public override String ToString()
        {
            return array.ToString() + " [ " + index.ToString() + " ]";
        }
    }
}
