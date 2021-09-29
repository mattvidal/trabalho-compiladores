using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BaseCompilador.Symbols;
using System.IO;
using BaseCompilador.IO;

namespace BaseCompilador.Lexer
{
    public class CompilerLexer
    {
        public static int Line = 1;
        char Peek = ' ';
        Hashtable Words = new Hashtable();
        StringReader inputText = new StringReader(InputAndOutput.InputText);

        void Reserve(CompilerWord w)
        {
            Words.Add(w.LexElement, w);
        }

        public CompilerLexer()
        {
            Reserve(new CompilerWord("if",      CompilerTag.IF      ));
            Reserve(new CompilerWord("else",    CompilerTag.ELSE    ));
            Reserve(new CompilerWord("while",   CompilerTag.WHILE   ));
            Reserve(new CompilerWord("do",      CompilerTag.DO      ));
            Reserve(new CompilerWord("break",   CompilerTag.BREAK   ));
            Reserve(CompilerWord.True);
            Reserve(CompilerWord.False);
            Reserve(Symbols.CompilerType.Int);
            Reserve(Symbols.CompilerType.Char);
            Reserve(Symbols.CompilerType.Float);
            Reserve(Symbols.CompilerType.Bool);
        }

        void ReadChar()
        {
            try
            {
                //Peek = (char)Console.Read();
                Peek = (char)inputText.Read();
            }
            catch
            {
                throw new IOException();
            }
        }

        Boolean ReadChar(char c)
        {
            try
            {
                ReadChar();
                if (Peek != c)
                    return false;
                Peek = ' ';
                return true;
            }
            catch
            {
                throw new IOException();
            }
        }

        public CompilerToken Scan()
        {
            try
            {
                for (; ; ReadChar())
                {
                    if (Peek == ' ' || Peek == '\t')
                        continue;
                    else if (Peek == '\n')
                        Line = Line + 1;
                    else
                        break;
                }
                switch (Peek)
                {
                    case '&':
                        if (ReadChar('&'))
                            return CompilerWord.and;
                        else
                            return new CompilerToken('&');
                    case '|':
                        if (ReadChar('|'))
                            return CompilerWord.or;
                        else
                            return new CompilerToken('|');
                    case '=':
                        if (ReadChar('='))
                            return CompilerWord.eq;
                        else
                            return new CompilerToken('=');
                    case '!':
                        if (ReadChar('='))
                            return CompilerWord.ne;
                        else
                            return new CompilerToken('!');
                    case '<':
                        if (ReadChar('='))
                            return CompilerWord.le;
                        else
                            return new CompilerToken('<');
                    case '>':
                        if (ReadChar('='))
                            return CompilerWord.ge;
                        else
                            return new CompilerToken('>');
                }
                if (Char.IsDigit(Peek))
                {
                    int v = 0;
                    do
                    {
                        v *= 10;
                        v += Convert.ToInt16(Peek);
                        ReadChar();
                    }
                    while (Char.IsDigit(Peek));
                    if (Peek != '.')
                        return new CompilerNumber(v);
                    float x = v;
                    float d = 10;
                    for (; ; )
                    {
                        ReadChar();
                        if (!Char.IsDigit(Peek)) break;
                        x += Convert.ToInt16(Peek);
                        x /= d;
                        d *= 10;
                    }
                    return new CompilerReal(x);
                }
                if (Char.IsLetter(Peek))
                {
                    StringBuilder Sb = new StringBuilder();
                    do
                    {
                        Sb.Append(Peek);
                        ReadChar();
                    }
                    while (Char.IsLetterOrDigit(Peek));
                    string s = Sb.ToString();
                    CompilerWord w = (CompilerWord)Words[s];
                    if (w != null)
                        return w;
                    w = new CompilerWord(s, CompilerTag.ID);
                    Words.Add(s, w);
                    return w;
                }
                CompilerToken tok = new CompilerToken(Peek);
                Peek = ' ';
                return tok;
            }
            catch
            {
                throw new IOException();
            }
        }
    }
}
