namespace GraphicVisualisator.Math
{
    // Derivative - Производная, Tangent - Касательная
    public class Derivative
    {
        private readonly Function ParentFunction;
        
        public Derivative(Function function)
        {
            ParentFunction = function;
        }

        public void DrawTangent(double x, double startX, double endX, Graphics graphics, GraphicManager manager,  Pen drawingPen, double epsilon = Constants.EPSILON)
        {
            double startY = -FindTangent(startX, x, ParentFunction, epsilon);
            double endY = -FindTangent(endX, x, ParentFunction, epsilon);
            graphics.DrawLine(
                drawingPen,
                (float)(startX * manager.HeightScale),
                (float)(startY * manager.WidthScale),
                (float)(endX * manager.HeightScale),
                (float)(endY * manager.WidthScale)
            );
        }
        
        public static double FindTangent(double x, double x0, Function function, double eps)
        {
            double div = (function(x0 + eps) - function(x0)) / eps;
            return div * x + function(x0) - div * x0;
        }

        public static double FindTangentX(double x0, Function function, double eps) => (function(x0 + eps) - function(x0)) / eps;

        public static string GetTangentFormula(double x, Function function, double eps = Constants.EPSILON)
        {
            double div = FindTangentX(x, function, eps);
            
            if(System.Math.Round(div, 2) == 0)
                return $"{System.Math.Round(function(x) - div * x)}";
            else 
                return $"{System.Math.Round(div, 2)}x + {System.Math.Round(function(x) - div * x)}";
        }
    }
}
