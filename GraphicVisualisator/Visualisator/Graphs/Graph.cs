using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator.Graphs
{
    public class Graph : IGraph
    {
        public string Name => nameof(Graph);

        public Function GraphicFunction;

        public Graph(Function graphicFunction)
        {
            GraphicFunction = graphicFunction;
        }

        public IEnumerable<PointF> GetPoints(double startX, double endX, double step = Constants.GRAPHIC_STEP)
        {
            double minStep = step / 10;
            for (double i = startX; i < endX; i += step)
            {
                var tangent = System.Math.Abs(Derivative.FindTangent(i, i, GraphicFunction, 0.000001f));
                if (tangent > 20)
                    step = minStep;
                if (tangent < 10)
                    step = minStep * 10;
                if (System.Math.Abs(Derivative.FindTangentX(i, GraphicFunction, 0.000001f)) < 100 && GraphicFunction(i) != 0)
                    yield return new((float)i, (float)GraphicFunction(i));

            }
        }
    }
}