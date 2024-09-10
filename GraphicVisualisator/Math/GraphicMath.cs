namespace GraphicVisualisator
{
    public class GraphicMath
    {
        public static int Factorial(int x)
        {
            int y = 1;
            for (int i = x; i > 0; i--)
                y *= i;

            return y;
        }

        public static double Power(double x, int s)
        {
            double y = 1;
            for (int i = 0; i < s; i++)
                y *= x;
            if (s == 0) y = 1;
            return y;
        }

        public static double Sin(double x)
        {
            int step = 10;
            if (x > 0)
                while (x >= System.Math.PI)
                    x -= 2 * System.Math.PI;
            else
                while (x < -System.Math.PI)
                    x += 2 * System.Math.PI;

            double y = 0;
            int k = 1;
            for(int i = 1; i < step; i+=2)
            {
                y += k * Power(x, i) / Factorial(i);
                k = -k;
            }

            return y;
        }

        public static double Cos(double x)
        {

            int step = 10;
            if (x > 0)
                while (x >= System.Math.PI )
                    x -= 2 * System.Math.PI;
            else
                while (x < -System.Math.PI)
                    x += 2 * System.Math.PI;

            double y = 0;
            int k = 1;
            for (int i = 0; i < step; i += 2)
            {
                y += k * Power(x, i) / Factorial(i);
                k = -k;
            }

            return y;
        }

        public static int Sqrt(double x)
        {
            int sqr = 1;
            for (int i = 0; i < (int)x; i++)
                for (int j = 0; j < i; j++)
                    if (j * j == i)
                    {
                        sqr = j;
                        break;
                    }
            return sqr;
        }
    }
}
