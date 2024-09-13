using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator.Graphs
{
    public class ExpressionGraph : IGraph
    {
        public string Name => nameof(Graph);

        public Function GraphicFunction;

        public ExpressionGraph(string expression)
        {
            GraphicFunction = (x) => Parser.Parse(expression.Replace("x", (x).ToString()));
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
    }
}