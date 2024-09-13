using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator.Graphs
{
    public class ParametricGraph : IGraph
    {
        public string Name => nameof(Graph);

        public Function OrdinateFunction;
        
        public Function AbscissaFunction;

        public ParametricGraph(Function xFunction, Function yFunction)
        {
            OrdinateFunction = yFunction;
            AbscissaFunction = xFunction;
        }

        public IEnumerable<PointF> GetPoints(GraphParameters parameters)
        {
            for (double i = parameters.StartX; i < parameters.EndX; i += parameters.Step)
            {
                double x = AbscissaFunction(i);
                double y = OrdinateFunction(i);
                yield return new PointF((float)x, (float)y);
            }
        }
    }
}