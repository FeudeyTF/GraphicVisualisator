using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator.Graphs
{
    public interface IGraph
    {
        public string Name { get; }

        public IEnumerable<PointF> GetPoints(double startX, double endX, double step = Constants.GRAPHIC_STEP);
    }
}