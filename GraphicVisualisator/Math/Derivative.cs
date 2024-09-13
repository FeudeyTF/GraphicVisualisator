using GraphicVisualisator.Visualisator;

namespace GraphicVisualisator.Math
{
    // Derivative - Производная, Tangent - Касательная
    public class Derivative
    {
        public static void DrawTangent(Function function, double x, float startX, float endX, Graphics graphics, GraphManager manager,  Pen drawingPen, double epsilon = Constants.EPSILON)
        {
            var points = GetTangentPoints(function, x, startX, endX, epsilon);
            graphics.DrawLine(
                drawingPen,
                (float)(points[0].X * manager.HeightScale),
                (float)(points[0].Y * manager.WidthScale),
                (float)(points[1].X * manager.HeightScale),
                (float)(points[1].Y * manager.WidthScale)
            );
        }

        public static List<PointF> GetTangentPoints(Function function, double x, float startX, float endX, double epsilon = Constants.EPSILON)
        {
            float startY = (float)-FindTangent(startX, x, function, epsilon);
            float endY = (float)-FindTangent(endX, x, function, epsilon);
            return new() {
                new PointF(startX, -startY),
                new PointF(endX, -endY)
            };
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
