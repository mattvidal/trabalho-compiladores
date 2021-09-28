﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompilador.Inter
{ 
    public class If : Statement
    {
        Express ex;
        Statement st;

        public If(Express e, Statement s)
        {
            ex = e;
            st = s;
            if (ex.type != Symbols.CompilerType.Bool)
                ex.PrintError("Boolean required in if");
        }

        public override void Generate(int b, int a)
        {
            int label = NewLabel();
            ex.Jump(0, a);
            EmitLabel(label);
            st.Generate(label, a);
        }
    }
}
