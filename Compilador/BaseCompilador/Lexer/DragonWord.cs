using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCompilador.Lexer
{
    public class CompilerWord : CompilerToken
    {
        public string LexElement = "";

        public CompilerWord(string s, int tag) : base(tag)
        {
            LexElement = s;
        }

        public new string ToString()
        {
            return LexElement;
        }

        public readonly static CompilerWord   and     = new CompilerWord("&&",      CompilerTag.AND),
                                            or      = new CompilerWord("||",      CompilerTag.OR),
                                            eq      = new CompilerWord("==",      CompilerTag.EQ),
                                            ne      = new CompilerWord("!=",      CompilerTag.NE),
                                            le      = new CompilerWord("<=",      CompilerTag.LE),
                                            ge      = new CompilerWord(">=",      CompilerTag.GE),
                                            minus   = new CompilerWord("minus",   CompilerTag.MINUS),
                                            True    = new CompilerWord("true",    CompilerTag.TRUE),
                                            False   = new CompilerWord("false",   CompilerTag.FALSE),
                                            temp    = new CompilerWord("t",       CompilerTag.TEMP);
    }
}
