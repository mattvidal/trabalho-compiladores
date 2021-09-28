using BaseCompilador.IO;
using BaseCompilador.Lexer;
using BaseCompilador.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompiladorController : ControllerBase
    {
        [Route("EnviarParaCompilar")]
        [HttpPost]
        public IActionResult EnviarParaCompilar([FromBody] string codigo, bool ignorarErros)
        {
            //InputAndOutput.InputText = "{int i; int j; float v; float x; float[100] a; while (true) { do i = i+1; while (a[i] < v); do j = j+1; while (a[j] > v); if (i >= j) break; x = a[i]; a[i] = a[j]; a[j] = x; }}";

            //Validando string vazia
            if(!string.IsNullOrEmpty(codigo))
            {
                try
                {
                    InputAndOutput.InputText = codigo;

                    InputAndOutput.OutputText.Clear();
                    InputAndOutput.ErrorText.Clear();
                    CompilerLexer lex = new CompilerLexer();
                    CompilerParser par = new CompilerParser(lex);
                    par.Program();


                    if (ignorarErros)
                    {
                        return Ok(InputAndOutput.OutputText.ToString());
                    }
                    else
                    {
                        return StatusCode(400, InputAndOutput.ErrorText.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new HttpRequestException(ex.ToString());
                }
               
            }
            else
            {
                return StatusCode(400, "Entrada vazia.");
            }

            //return InputAndOutput.OutputText.ToString();


            // return InputAndOutput.ErrorText.ToString();
        }
    }
}
