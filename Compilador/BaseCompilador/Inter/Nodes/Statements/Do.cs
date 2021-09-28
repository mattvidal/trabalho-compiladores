﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Symbols;

namespace BaseCompilador.Inter
{
    public class Do : Statement
    {
        Express ex;
        Statement st;

        public Do()
        {
            ex = null;
            st = null;
        }

        public void Init(Statement s, Express e)
        {
            ex = e;
            st = s;
            if (ex.type != CompilerType.Bool)
                ex.PrintError("boolean required in Do");
        }

        public override void Generate(int b, int a)
        {
            After = a;
            int label = NewLabel();
            st.Generate(b, label);
            EmitLabel(label);
            ex.Jump(b, 0);
        }
    }
}
