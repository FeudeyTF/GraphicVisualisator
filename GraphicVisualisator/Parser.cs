using System.Text;

namespace GraphicVisualisator
{
    // Парсер польской записи
    public class Parser
    {
        public static readonly string[] PrefixFunctions = { "cos", "sin" };

        public static int GetPriority(string c)
        {
            return c switch
            {
                "^" => 1,
                "*" or "/" => 2,
                "+" or "-" => 3,
                "(" or ")" => 4,
                _ => 0,
            };
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
                        if (expr[i - 1] != ' ' && result.ToString()[i - 1] != ' ')
                        {
                            result.Append(' ');
                        }
                        result.Append(expr[i]);
                        if (i + 1 < expr.Length)
                            if (expr[i + 1] != ' ' && !symb.Contains(expr[i - 1].ToString()))
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

        public static string Translate(string expr)
        {
            string[] splitedString = ExprBuilder(expr).Split();
            Stack<string> stringStack = new Stack<string>(100);
            string result = "";
            foreach (string s in splitedString)
            {
                if (double.TryParse(s, out _))
                    result += s + " ";
                else
                {
                    if (PrefixFunctions.Contains(s) || s == "(" || stringStack.Count == 0)
                    {
                        stringStack.Push(s);
                        continue;
                    }
                    if (s == ")")
                    {
                        while (stringStack.Peek() != "(")
                        {
                            result += stringStack.Pop() + " ";
                        }
                        stringStack.Pop();
                        continue;
                    }
                    if (GetPriority(s) != 0)
                    {
                        while (PrefixFunctions.Contains(stringStack.Peek()) || GetPriority(stringStack.Peek()) < GetPriority(s))
                        {
                            result += s + " ";
                            stringStack.Pop();
                        }
                        stringStack.Push(s);
                    }

                }
            }
            while (stringStack.Count != 0)
                result += stringStack.Pop() + " ";

            return result.Trim();
        }
        
        public static double Parse(string expression)
        {
            string[] splitExpr = Translate(expression).Split(' ');
            Stack<double> doubleStack = new(expression.Length);
            foreach (string s in splitExpr)
            {
                if (double.TryParse(s, out double x))
                {
                    doubleStack.Push(x);
                }
                else
                {
                    switch (s)
                    {
                        case "+":
                            doubleStack.Push(doubleStack.Pop() + doubleStack.Pop());
                            break;
                        case "-":
                            doubleStack.Push(-doubleStack.Pop() + doubleStack.Pop());
                            break;
                        case "/":
                            doubleStack.Push(1 / doubleStack.Pop() * doubleStack.Pop());
                            break;
                        case "*":
                            doubleStack.Push(doubleStack.Pop() * doubleStack.Pop());
                            break;
                        case "cos":
                            doubleStack.Push(System.Math.Cos(doubleStack.Pop()));
                            break;
                        case "sin":
                            doubleStack.Push(System.Math.Sin(doubleStack.Pop()));
                            break;
                    }
                }
            }
            return doubleStack.Peek();
        }
    }
}