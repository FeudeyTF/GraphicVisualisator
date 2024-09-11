using GraphicVisualisator.Math;

namespace GraphicVisualisator.Visualisator
{
    public struct GraphicParameters
    {
        public double StartX;

        public double EndX;

        public double Step;

        public GraphicParameters(double startX, double endX, double step = Constants.GRAPHIC_STEP)
        {
            StartX = startX;
            EndX = endX;
            Step = step;
        }
    }
}