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

        public IEnumerable<PointF> GetPoints(GraphParameters parameters)
        {
            double minStep = parameters.Step / 10;
            for (double i = parameters.StartX; i < parameters.EndX; i += parameters.Step)
            {
                var tangent = System.Math.Abs(Derivative.FindTangent(i, i, GraphicFunction, Constants.EPSILON));
                if (tangent > 20)
                    parameters.Step = minStep;
                if (tangent < 10)
                    parameters.Step = minStep * 10;
                if (System.Math.Abs(Derivative.FindTangentX(i, GraphicFunction, Constants.EPSILON)) < 100 && GraphicFunction(i) != 0)
                    yield return new((float)i, (float)GraphicFunction(i));
            }
        }

        public List<PointF> GetTangentPoints(double x, GraphParameters parameters) => 
            Derivative.GetTangentPoints(GraphicFunction, x, (float)parameters.StartX, (float)parameters.EndX);
    }
}