using BaseCompilador.IO;
using BaseCompilador.Lexer;
using BaseCompilador.Parser;
using System;

namespace Compilador
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /*
           DragonLexer lex = new DragonLexer();
           DragonParser par = new DragonParser(lex);
           par.Program();
           */

            //InputAndOutput.InputText = "{int i; i = 0; while (i < 100) { i = i + 1; }}";
            //InputAndOutput.InputText = "{int i; int j; float v; float x; float[100] a; while (true) { do i = i+1; while (a[i] < v); do j = j+1; while (a[j] > v); if (i >= j) break; x = a[i]; a[i] = a[j]; a[j] = x; }}";
            InputAndOutput.InputText = "{int i; int j; float v; float x; float[100] a; while (true) { do i = i+1; while (a[i] < v); do j = j+1; while (a[j] > v); if (i >= j) break; x = a[i]; a[i] = a[j]; a[j] = x; }}";
            InputAndOutput.OutputText.Clear();
            InputAndOutput.ErrorText.Clear();
            CompilerLexer lex = new CompilerLexer();
            CompilerParser par = new CompilerParser(lex);
            par.Program();
            //richTextBox2.Text = InputAndOutput.OutputText.ToString();
            //richTextBox3.Text = InputAndOutput.ErrorText.ToString();

            Console.WriteLine(InputAndOutput.OutputText.ToString());

            Console.WriteLine(InputAndOutput.ErrorText.ToString());
        }
    }
}
