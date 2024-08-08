namespace WindowsFormsApplication10.MathAnalysis
{
    public class Derivative
    {
        private GraphicManager GraphicManager;
        
        public Derivative(GraphicManager graphicManager)
        {
            GraphicManager = graphicManager;
        }

        public void DrawTangent(double x, int num, double step, GraphicManager.Function f1, Graphics g, double eps)
        {
            try
            {
                for (double i = -num; i < num; i += step)
                {
                    g.DrawLine(new Pen(Color.Green, 3), (float)(i * GraphicManager.hs), -(float)(FindTangent(i, x, f1, eps) * GraphicManager.vs), (float)((i + step) * GraphicManager.hs), -(float)(FindTangent(i + step, x, f1, eps) * GraphicManager.vs));
                }
            }
            catch(Exception e)
            {
                
            }

        }
        
        public static double FindTangent(double x, double x0, GraphicManager.Function f1, double eps)
        {
            double div = (f1(x0 + eps) - f1(x0)) / eps;
            return div * x + f1(x0) - div * x0;
        }

        public static double FindTangentX(double x0, GraphicManager.Function f1, double eps)
        {
            double div = (f1(x0 + eps) - f1(x0)) / eps;
            return div;
        }

        public static string TangentFormula(double x0, GraphicManager.Function f1, double eps)
        {
            double div = (f1(x0 + eps) - f1(x0)) / eps;
            
            if(Math.Round(div, 2) == 0)
            return $"{Math.Round(f1(x0) - div * x0)}";
            else return $"{Math.Round(div, 2)}x + {Math.Round(f1(x0) - div * x0)}";
        }
    }
}
