namespace GraphicVisualisator.Visualisator.Graphs
{
    public interface IGraph
    {
        public string Name { get; }

        public IEnumerable<PointF> GetPoints(GraphParameters parameters);
    }
}