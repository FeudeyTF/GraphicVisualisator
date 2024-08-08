using System.Text;

namespace GraphicVisualisator
{
    public class Stack<TValue>
    {
        private TValue[] StackArray;

        private int Pointer = 0;

        public Stack(int n)
        {
            StackArray = new TValue[n];
        }

        public bool Push(TValue value)
        {
            if (Pointer < StackArray.Length)
            {
                StackArray[Pointer++] = value;
                return true;
            }
            else
                return false;
        }

        public TValue Peek()
        {
            if (Pointer > 0)
                return StackArray[Pointer - 1];
            else
                return default;
        }

        public TValue Pop()
        {
            if (Pointer > 0)
                return StackArray[--Pointer];
            else 
                return default;
        }

        public int Number
        {
            get { return Pointer; }
        }
    }
   

    public class Parser
    {
        public static string[] PrefixFunctions = { "cos", "sin" };

        public static int GetPriority(string c)
        {
            switch (c)
            {
                case "^":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "+":
                case "-":
                    return 3;
                case "(":
                case ")":
                    return 4;
                default: return 0;
            }
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
                    if (PrefixFunctions.Contains(s) || s == "(" || stringStack.Number == 0)
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
            while (stringStack.Number != 0)
                result += stringStack.Pop() + " ";

            return result.Trim();
        }
        
        public static double Parse(string expr)
        {
            string[] splitExpr = Translate(expr).Split(' ');
            Stack<double> doubleStack = new Stack<double>(expr.Length);
            double x;
            foreach (string s in splitExpr)
            {
                if (double.TryParse(s, out x))
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
                            doubleStack.Push(Math.Cos(doubleStack.Pop()));
                            break;
                        case "sin":
                            doubleStack.Push(Math.Sin(doubleStack.Pop()));
                            break;
                    }
                }
            }
            return doubleStack.Peek();
        }
    }
}



