using GraphicVisualisator.Math;
using GraphicVisualisator.Visualisator.Graphs;

namespace GraphicVisualisator.Visualisator
{
    public delegate double Function(double x);

    public class GraphManager
    {
        public const double DEFAULT_SCALE_RESIZE = 1.1;

        public readonly Panel GraphPanel;

        private readonly int Height, Width;

        private double OrdinateScale, AbscissaScale; // Y and X axes scales

        public double HeightScale, WidthScale; // Zoom Scale

        private Point Center;

        private bool IsMouseDown = false;

        private Point LastMousePosition;

        private DateTime LastMove = DateTime.Now;

        private double GraphMoveDelay = 0.05;

        private readonly List<EvaluatedGraph> Graphs = new();

        public GraphManager(Panel graphicPanel, double ordinateScale = Constants.DEFAULT_AXES_SCALE, double abscissaScale = Constants.DEFAULT_AXES_SCALE)
        {
            GraphPanel = graphicPanel;
            Height = GraphPanel.Height;
            Width = GraphPanel.Width;
            OrdinateScale = ordinateScale;
            AbscissaScale = abscissaScale;
            Center = new(Width / 2, Height / 2);

            LastMousePosition = new();

            GraphPanel.Paint += HandelPaint;

            GraphPanel.MouseWheel += HandleMouseWheel;
            GraphPanel.MouseUp += HandleMouseUp;
            GraphPanel.MouseDown += HandleMouseDown;
            GraphPanel.MouseMove += HandleMouseMove;

        }

        public void AddGraph(IGraph graph, Color graphColor, GraphParameters parameters) =>
            Graphs.Add(new(graphColor, graph.GetPoints(parameters.StartX, parameters.EndX, parameters.Step).ToList()));

        private void HandelPaint(object? sender, PaintEventArgs args)
        {
            CreateGraphZone(args.Graphics);
            foreach (var graph in Graphs)
                DrawGraphic(graph, args.Graphics);
        }

        private void HandleMouseMove(object? sender, MouseEventArgs args)
        {
            if (IsMouseDown && (DateTime.Now - LastMove).TotalSeconds >= GraphMoveDelay)
            {
                TransformCenter(args.X - LastMousePosition.X, args.Y - LastMousePosition.Y);
                LastMousePosition.X = args.X;
                LastMousePosition.Y = args.Y;
                LastMove = DateTime.Now;
                GraphPanel.Invalidate();
            }
        }

        private void HandleMouseDown(object? sender, MouseEventArgs args)
        {
            IsMouseDown = true;
            LastMousePosition.X = args.X;
            LastMousePosition.Y = args.Y;
            if (Program.MainPage != null)
                Program.MainPage.Cursor = Cursors.SizeAll;
        }

        private void HandleMouseUp(object? sender, MouseEventArgs args)
        {
            IsMouseDown = false;
            if (Program.MainPage != null)
                Program.MainPage.Cursor = Cursors.Default;
            GraphPanel.Invalidate();
        }

        private void HandleMouseWheel(object? sender, MouseEventArgs args)
        {
            if (args.Delta > 0)
            {
                HeightScale *= DEFAULT_SCALE_RESIZE;
                WidthScale *= DEFAULT_SCALE_RESIZE;
            }
            else
            {
                HeightScale /= DEFAULT_SCALE_RESIZE;
                WidthScale /= DEFAULT_SCALE_RESIZE;
            }
            GraphPanel.Invalidate();
        }

        public void CreateGraphZone(Graphics graphics)
        {
            // Рисуем оси координат
            graphics.TranslateTransform(Center.X, Center.Y);
            Pen axisPen = new(Color.Black, 3);
            Font fn = new("Arial", (int)HeightScale / 3);
            StringFormat sf = new()
            {
                Alignment = StringAlignment.Center
            };

            StringFormat sfv = new()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            // Горизонтальная ось
            graphics.DrawLine(axisPen, (float)(-Width / 2 * HeightScale / 10), 0, (float)((Width / 2 - 2) * HeightScale / 10), 0);
            graphics.DrawLine(axisPen, (float)((Width / 2 - 2) * HeightScale / 10), 0, (float)((Width / 2 - 15) * HeightScale / 10), 3);
            graphics.DrawLine(axisPen, (float)((Width / 2 - 2) * HeightScale / 10), 0, (float)((Width / 2 - 15) * HeightScale / 10), -3);

            // Вертикальная ось
            graphics.DrawLine(axisPen, 0, -(float)((Height / 2 - 2) * WidthScale / 10), 0, (float)((Height / 2 - 2) * WidthScale / 10));
            graphics.DrawLine(axisPen, 0, -(float)((Height / 2 + 2) * WidthScale / 10), 3, -(float)((Height / 2 - 15) * WidthScale / 10));
            graphics.DrawLine(axisPen, 0, -(float)((Height / 2 + 2) * WidthScale / 10), -3, -(float)((Height / 2 - 15) * WidthScale / 10));

            // Разметка горизонтальной оси
            for (int i = 1; i < 30; i++)
            {
                double t = OrdinateScale / 10 * i;
                GetScaledCoords(t, 0, out int a, out int b);
                graphics.DrawLine(axisPen, a, b, a, b + 7);
                t = System.Math.Round(t, 2);
                string s = t.ToString();
                graphics.DrawString(s, fn, axisPen.Brush, a, b + 7, sf);
                graphics.DrawLine(axisPen, -a, b, -a, b + 7);
                s = "-" + s;
                graphics.DrawString(s, fn, axisPen.Brush, -a, b + 7, sf);
            }
            // Разметка вертикальной оси
            for (int i = 1; i < 30; i++)
            {
                double t = AbscissaScale / 10 * i;
                GetScaledCoords(0, t, out int a, out int b);
                graphics.DrawLine(axisPen, a, b, a - 7, b);
                t = System.Math.Round(t, 2);
                string s = t.ToString();
                graphics.DrawString(s, fn, axisPen.Brush, a - 10, b, sfv);
                graphics.DrawLine(axisPen, a, -b, a - 7, -b);
                s = "-" + s;
                graphics.DrawString(s, fn, axisPen.Brush, a - 10, -b, sfv);
            }
        }

        public void DrawGraphic(EvaluatedGraph graph, Graphics graphics)
        {
            for (int i = 1; i < graph.Points.Count; i++)
            {
                var lastPoint = graph.Points[i - 1];
                var currentPoint = graph.Points[i];
                graphics.DrawLine(new Pen(graph.GraphColor, 3), (float)(lastPoint.X * HeightScale), -(float)(lastPoint.Y * WidthScale), (float)(currentPoint.X * HeightScale), -(float)(currentPoint.Y * WidthScale));
            }
        }

        private void GetScaledCoords(double x, double y, out int scaledX, out int scaledY)
        {
            scaledX = (int)System.Math.Round(x * HeightScale);
            scaledY = -(int)System.Math.Round(y * WidthScale);
        }

        public void Resize(int heightScale, int widthScale)
        {
            HeightScale = heightScale / (2 * OrdinateScale);
            WidthScale = widthScale / (2 * AbscissaScale);
        }

        public void ScaleUpdate(double heightScale, double widthScale)
        {
            OrdinateScale = heightScale;
            AbscissaScale = widthScale;
        }

        public void TransformCenter(int x, int y)
        {
            Center.X += x;
            Center.Y += y;
        }
    }

    public struct EvaluatedGraph
    {
        public Color GraphColor;

        public List<PointF> Points;

        public EvaluatedGraph(Color color, List<PointF> points)
        {
            GraphColor = color;
            Points = points;
        }
    }
}
