using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator
{
    public struct GraphParameters
    {
        public double StartX;

        public double EndX;

        public double Step;

        public GraphParameters(double startX, double endX, double step = Constants.GRAPHIC_STEP)
        {
            StartX = startX;
            EndX = endX;
            Step = step;
        }
    }
}