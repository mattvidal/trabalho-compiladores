using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BaseCompilador.Lexer;
using BaseCompilador.Inter;

namespace BaseCompilador.Symbols
{
    public class CompilerEnvironment
    {
        private Hashtable Table;
        protected CompilerEnvironment Prev;

        public CompilerEnvironment(CompilerEnvironment n)
        {
            Table = new Hashtable();
            Prev = n;
        }

        public void Put(CompilerToken t, Id i)
        {
            Table.Add(t, i);
        }

        public Id GetId(CompilerToken t)
        {
            for (CompilerEnvironment e = this; e != null; e = e.Prev)
            {
                Id found = (Id)(e.Table[t]);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
