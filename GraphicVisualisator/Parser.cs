using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphicVisualisator
{
    class DoubleStack
    {
        double[] dat;
        int sp = 0;

        public DoubleStack(int n)
        {
            dat = new double[n];
        }

        public bool Push(double x)
        {
            if (sp < dat.Length)
            {
                dat[sp++] = x;
                return true;
            }
            else
                return false;
        }

        public double Peek()
        {
            if (sp > 0) return dat[sp - 1];
            else return 0;
        }

        public double Pop()
        {
            if (sp > 0) return dat[--sp];
            else return 0;
        }

        public int Number
        {
            get { return sp; }
        }

    }
    class StringStack
    {
        string[] dat;
        int sp = 0;

        public StringStack(int n)
        {
            dat = new string[n];
        }

        public bool Push(string x)
        {
            if (sp < dat.Length)
            {
                dat[sp++] = x;
                return true;
            }
            else
                return false;
        }

        public string Peek()
        {
            if (sp > 0) return dat[sp - 1];
            else return "";
        }

        public string Pop()
        {
            if (sp > 0) return dat[--sp];
            else return "";
        }
        public int Number
        {
            get { return sp; }
        }

    }
    public class Parser
    {
        public Dictionary<string, int> Operands = new Dictionary<string, int>();
        public Parser()
        {
            Operands.Add("(", 1);
            Operands.Add(")", 2);
            Operands.Add("-", 2);
            Operands.Add("+", 2);
            Operands.Add("*", 3);
            Operands.Add("/", 3);
            Operands.Add("cos", 2);
            Operands.Add("sin", 2);
        }
        public static string ExprBuilder(string expr)
        {
            string[] symb = { "+", "-", "*", "/", "(", ")" };
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < expr.Length; i++)
            {
                if (symb.Contains(expr[i].ToString()))
                {
                    if (i > 0)
                    {
                        if (expr[i - 1] != ' ' && expr[i] != '(')
                        {
                            result.Append(' ');
                        }
                        result.Append(expr[i]);
                        if (i + 1 < expr.Length)
                            if (expr[i + 1] != ' ')
                            {
                                result.Append(' ');
                            }
                    }
                }
                else
                    result.Append(expr[i]);
            }

            return result.ToString();
        }
        public string Translate(string expr)
        {
            string[] splitedString = ExprBuilder(expr).Split();
            StringStack ss = new StringStack(100);
            string result = "";
            foreach (string s in splitedString)
            {
                if (!Operands.TryGetValue(s, out _))
                    result += s + " ";
                else
                {
                    if (ss.Number == 0 || Operands[ss.Peek()] < Operands[s] || s == "(")
                    {
                        ss.Push(s);
                    }
                    if (s == ")")
                    {
                        while (ss.Peek() != "(")
                        {
                            result += ss.Pop() + " ";
                        }
                        ss.Pop();
                    }

                }
            }
            while (ss.Number != 0)
                result += ss.Pop() + " ";

            return result.Trim();
        }
        public double Parse(string expr)
        {
            string[] splitExpr = Translate(expr).Split(' ');
            DoubleStack St = new DoubleStack(expr.Length);
            double x;
            foreach (string s in splitExpr)
            {
                if (double.TryParse(s, out x))
                {
                    St.Push(x);
                }
                else
                {
                    switch (s)
                    {
                        case "+":
                            St.Push(St.Pop() + St.Pop());
                            break;
                        case "-":
                            St.Push(-St.Pop() + St.Pop());
                            break;
                        case "/":
                            St.Push(1 / St.Pop() * St.Pop());
                            break;
                        case "*":
                            St.Push(St.Pop() * St.Pop());
                            break;
                        case "cos":
                            St.Push(Math.Cos(St.Pop()));
                            break;
                        case "sin":
                            St.Push(Math.Sin(St.Pop()));
                            break;
                    }
                }
            }
            return St.Peek();
        }
    }
}


