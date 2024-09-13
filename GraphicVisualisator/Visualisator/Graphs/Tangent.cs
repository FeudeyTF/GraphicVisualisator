using System.Collections.Immutable;
using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator.Graphs
{
    public class Tangent : IGraph
    {
        public string Name => nameof(Graph);

        public Function GraphicFunction;

        public int X;

        public Tangent(Function graphicFunction, int x)
        {
            GraphicFunction = graphicFunction;
            X = x;

        }

        public IEnumerable<PointF> GetPoints(GraphParameters parameters)
        {
            return Derivative.GetTangentPoints(GraphicFunction, X, (float)parameters.StartX, (float)parameters.EndX);
        }
    }
}