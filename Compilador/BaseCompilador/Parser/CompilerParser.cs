using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseCompilador.Inter;
using BaseCompilador.Lexer;
using BaseCompilador.Symbols;
using BaseCompilador.IO;

namespace BaseCompilador.Parser
{
    public class CompilerParser
    {
        private CompilerLexer lex;
        private CompilerToken look;
        CompilerEnvironment top = null;
        int used = 0;

        public CompilerParser(CompilerLexer l)
        {
            lex = l;
            Move();
        }

        private Statement Assign()
        {
            Statement statement;
            CompilerToken t = look;
            Match(CompilerTag.ID);
            Id id = top.GetId(t);
            if(id == null)
            {
                PrintError(t.ToString() + " undeclared.");
            }
            if(look.tag == '=')
            {
                Move();
                statement = new Set(id, GetBoolExpress());
            }
            else
            {
                Access x = GetOffset(id);
                Match('=');
                statement = new SetElement(x, GetBoolExpress());
            }
            Match(';');
            return statement;
        }

        private void Declare()
        {
            while(look.tag == CompilerTag.BASIC)
            {
                CompilerType p = GetCompilerType();
                CompilerToken tok = look;
                Match(CompilerTag.ID);
                Match(';');
                Id id = new Id((CompilerWord)tok, p , used);
                top.Put(tok, id);
                used += p.Width;
            }
        }

        private Statement GetBlock()
        {
            Match('{');
            CompilerEnvironment savedEnv = top;
            top = new CompilerEnvironment(top);
            Declare();
            Statement s = GetStatements();
            Match('}');
            top = savedEnv;
            return s;
        }

        private Express GetBoolExpress()
        {
            Express x = Join();
            while(look.tag == CompilerTag.OR)
            {
                CompilerToken tok = look;
                Move();
                x = new Or(tok, x, Join());
            }
            return x;
        }

        private CompilerType GetDimensions(CompilerType p)
        {
            Match('[');
            CompilerToken tok = look;
            Match(CompilerTag.NUM);
            Match(']');
            if(look.tag == '[')
            {
                p = GetDimensions(p);
            }
            return new CompilerArray(((CompilerNumber)tok).Value, p);
        }

        private Express GetEquality()
        {
            Express x = GetRelativity();
            while(look.tag == CompilerTag.EQ || look.tag == CompilerTag.NE)
            {
                CompilerToken tok = look;
                Move();
                x = new Relation(tok, x, GetRelativity());
            }
            return x;
        }

        private void PrintError(string s)
        {
            //throw new Exception("Near line: " + CompilerLexer.Line + ": " + s);
            InputAndOutput.ErrorText.Append("Near line: " + CompilerLexer.Line + ": " + s);
            InputAndOutput.ErrorText.AppendLine();
        }

        private Express GetExpress()
        {
            Express x = GetTerm();
            while (look.tag == '+' || look.tag == '-')
            {
                CompilerToken tok = look;
                Move();
                x = new Arith(tok, x, GetExpress());
            }
            return x;
        }

        private Express GetFactor()
        {
            Express x = null;
            switch (look.tag)
            {
                case '(':
                    Move();
                    x = GetBoolExpress();
                    Match(')');
                    return x;

                case CompilerTag.NUM:
                    x = new Constant(look, CompilerType.Int);
                    Move();
                    return x;

                case CompilerTag.REAL:
                    x = new Constant(look, CompilerType.Float);
                    Move();
                    return x;

                case CompilerTag.TRUE:
                    x = Constant.True;
                    Move();
                    return x;

                case CompilerTag.FALSE:
                    x = Constant.False;
                    Move();
                    return x;

                case CompilerTag.ID:
                    string s = look.ToString();
                    Id id = top.GetId(look);
                    if(id == null)
                    {
                        PrintError(look.ToString() + " undeclared.");
                    }
                    Move();
                    if(look.tag != '[')
                    {
                        return id;
                    }
                    else
                    {
                        return GetOffset(id);
                    }

                default:
                    PrintError("Syntax error.");
                    return x;
            }
        }

        private Express GetRelativity()
        {
            Express x = GetExpress();
            switch (look.tag)
            {
                case '<':
                case '>':
                case CompilerTag.LE:
                case CompilerTag.GE:
                    CompilerToken tok = look;
                    Move();
                    return new Relation(tok, x, GetExpress());
                default:
                    return x;
            }
        }

        private Statement GetSingleStatement()
        {
            Express x;
            //Statement s;
            Statement s1, s2;
            Statement savedStatement;

            switch (look.tag)
            {
                case ';':
                    Move();
                    return Statement.Nul;

                case CompilerTag.IF:
                    Match(CompilerTag.IF);
                    Match('(');
                    x = GetBoolExpress();
                    Match(')');
                    s1 = GetSingleStatement();
                    if (look.tag != CompilerTag.ELSE)
                    {
                        return new If(x, s1);
                    }
                    Match(CompilerTag.ELSE);
                    s2 = GetSingleStatement();
                    return new Else(x, s1, s2);

                case CompilerTag.WHILE:
                    While whilenode = new While();
                    savedStatement = Statement.Enclosing;
                    Statement.Enclosing = whilenode;
                    Match(CompilerTag.WHILE);
                    Match('(');
                    x = GetBoolExpress();
                    Match(')');
                    s1 = GetSingleStatement();
                    whilenode.Init(x, s1);
                    Statement.Enclosing = savedStatement;
                    return whilenode;

                case CompilerTag.DO:
                    Do donode = new Do();
                    savedStatement = Statement.Enclosing;
                    Statement.Enclosing = donode;
                    Match(CompilerTag.DO);
                    s1 = GetSingleStatement();
                    Match(CompilerTag.WHILE);
                    Match('(');
                    x = GetBoolExpress();
                    Match(')');
                    Match(';');
                    donode.Init(s1, x);
                    Statement.Enclosing = savedStatement;
                    return donode;

                case CompilerTag.BREAK:
                    Match(CompilerTag.BREAK);
                    Match(';');
                    return new Break();

                case '{':
                    return GetBlock();

                default:
                    return Assign();
            }
        }

        private Statement GetStatements()
        {
            if(look.tag == '}')
            {
                return Statement.Nul;
            }
            else
            {
                return new Sequence(GetSingleStatement(), GetStatements());
            }
        }

        private Express GetTerm()
        {
            Express x = GetUnary();
            while(look.tag == '*' || look.tag == '/')
            {
                CompilerToken tok = look;
                Move();
                x = new Arith(tok, x, GetUnary());
            }
            return x;
        }

        private Express GetUnary()
        {
            if(look.tag == '-')
            {
                Move();
                return new Unary(CompilerWord.minus, GetUnary());
            }
            else if(look.tag == '!')
            {
                CompilerToken tok = look;
                Move();
                return new Not(tok, GetUnary());
            }
            else
            {
                return GetFactor();
            }
        }

        private Express Join()
        {
            Express x = GetEquality();
            while(look.tag == CompilerTag.AND)
            {
                CompilerToken tok = look;
                Move();
                x = new And(tok, x, GetEquality());
            }
            return x;
        }

        private void Match(int t)
        {
            if (look.tag == t)
            {
                Move();
            }
            else
            {
                PrintError("Syntax error.");
            }
        }

        private void Move()
        {
            look = lex.Scan();
        }

        public void Program()
        {
            Statement s = GetBlock();
            int begin = s.NewLabel();
            int after = s.NewLabel();
            s.EmitLabel(begin);
            s.Generate(begin, after);
            s.EmitLabel(after);
        }

        private CompilerType GetCompilerType()
        {
            CompilerType p = (CompilerType)look;
            Match(CompilerTag.BASIC);
            if(look.tag != '[')
            {
                return p;
            }
            else
            {
                return GetDimensions(p);
            }
        }


        //fix null reference exception
        private Access GetOffset(Id a)
        {
            Express i;
            Express w;
            Express t1, t2;
            Express loc;
            CompilerType type = a.type;
            Match('[');
            i = GetBoolExpress();
            Match(']');
            type = ((CompilerArray)type).of;
            w = new Constant(type.Width);
            t1 = new Arith(new CompilerToken('*'), i, w);
            loc = t1;
            while(look.tag == '[')
            {
                Match('[');
                i = GetBoolExpress();
                Match(']');
                type = ((CompilerArray)type).of;
                w = new Constant(type.Width);
                t1 = new Arith(new CompilerToken('*'), i, w);
                t2 = new Arith(new CompilerToken('+'), loc, t1);
                loc = t2;
            }
            return new Access(a, loc, type);
        }
    }
}
