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

        public IEnumerable<PointF> GetPoints(double startX, double endX, double step = Constants.GRAPHIC_STEP)
        {
            for (double phi = startX; phi < endX; phi += step)
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