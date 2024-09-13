using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator.Graphs
{
    public class PolarGraph : IGraph
    {
        public string Name => nameof(Graph);

        public Function GraphicFunction;

        public PolarGraph(Function graphicFunction)
        {
            GraphicFunction = graphicFunction;
        }

        public IEnumerable<PointF> GetPoints(GraphParameters parameters)
        {
            for (double phi = parameters.StartX; phi < parameters.EndX; phi += parameters.Step)
            {
                // Функция
                double r = GraphicFunction(phi);
                // Перевод в декартовы
                double x = r * System.Math.Cos(phi);
                double y = r * System.Math.Sin(phi);
                yield return new PointF((float)x, (float)y);
            }
        }
    }
}