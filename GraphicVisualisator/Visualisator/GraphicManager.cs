using GraphicVisualisator.Math;
using GraphicVisualisator.Visualisator.Graphs;

namespace GraphicVisualisator.Visualisator
{
    public delegate double Function(double x);

    public class GraphicManager
    {
        public const double DEFAULT_SCALE_RESIZE = 1.1;

        public readonly Panel GraphicPanel;

        public readonly Pen GraphicPen;

        public readonly Brush GraphicBrush;

        private readonly int Height, Width;

        private double OrdinateScale, AbscissaScale; // Y and X axes scales

        public double HeightScale, WidthScale; // Zoom Scale

        private Point Center;

        private bool IsMouseDown = false;

        private Point LastMousePosition;

        private DateTime LastMove = DateTime.Now;

        private double GraphMoveDelay = 0.05;

        List<List<PointF>> Graphs = new();

        public GraphicManager(Panel graphicPanel, Pen pen, Brush brush, int height, int width, double ordinateScale = Constants.DEFAULT_AXES_SCALE, double abscissaScale = Constants.DEFAULT_AXES_SCALE)
        {
            GraphicPanel = graphicPanel;
            Height = height;
            Width = width;
            GraphicPen = pen;
            GraphicBrush = brush;
            OrdinateScale = ordinateScale;
            AbscissaScale = abscissaScale;
            Center = new(Width / 2, Height / 2);

            LastMousePosition = new();

            GraphicPanel.Paint += HandelPaint;

            GraphicPanel.MouseWheel += HandleMouseWheel;
            GraphicPanel.MouseUp += HandleMouseUp;
            GraphicPanel.MouseDown += HandleMouseDown;
            GraphicPanel.MouseMove += HandleMouseMove;

            Graphs.Add(new Graph(GraphicMath.Cos).GetPoints(-30, 30).ToList());
        }

        private void HandelPaint(object? sender, PaintEventArgs args)
        {
            CreateGraphZone(args.Graphics);
            foreach(var graph in Graphs)
                DrawGraphic(graph, new GraphicParameters(-30, 30), args.Graphics);
        }

        private void HandleMouseMove(object? sender, MouseEventArgs args)
        { 
            if (IsMouseDown && (DateTime.Now - LastMove).TotalSeconds >= GraphMoveDelay)
            {
                TransformCenter(args.X - LastMousePosition.X, args.Y - LastMousePosition.Y);
                LastMousePosition.X = args.X;
                LastMousePosition.Y = args.Y;
                LastMove = DateTime.Now;
                GraphicPanel.Invalidate();
            }
        }

        private void HandleMouseDown(object? sender, MouseEventArgs args)
        {
            IsMouseDown = true;
            LastMousePosition.X = args.X;
            LastMousePosition.Y = args.Y;
            if(Program.MainPage != null)
                Program.MainPage.Cursor = Cursors.SizeAll;
        }

        private void HandleMouseUp(object? sender, MouseEventArgs args)
        {
            IsMouseDown = false;
            if(Program.MainPage != null)
                Program.MainPage.Cursor = Cursors.Default;
            GraphicPanel.Invalidate();
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
            GraphicPanel.Invalidate();
        }

        public void CreateGraphZone(Graphics graphics)
        {
            // Рисуем оси координат
            graphics.TranslateTransform(Center.X, Center.Y);
            Pen AxisPen = new(GraphicBrush, 3);
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
            graphics.DrawLine(AxisPen, (float)(-Width / 2 * HeightScale / 10), 0, (float)((Width / 2 - 2) * HeightScale / 10), 0);
            graphics.DrawLine(AxisPen, (float)((Width / 2 - 2) * HeightScale / 10), 0, (float)((Width / 2 - 15) * HeightScale / 10), 3);
            graphics.DrawLine(AxisPen, (float)((Width / 2 - 2) * HeightScale / 10), 0, (float)((Width / 2 - 15) * HeightScale / 10), -3);

            // Вертикальная ось
            graphics.DrawLine(AxisPen, 0, -(float)((Height / 2 - 2) * WidthScale / 10), 0, (float)((Height / 2 - 2) * WidthScale / 10));
            graphics.DrawLine(AxisPen, 0, -(float)((Height / 2 + 2) * WidthScale / 10), 3, -(float)((Height / 2 - 15) * WidthScale / 10));
            graphics.DrawLine(AxisPen, 0, -(float)((Height / 2 + 2) * WidthScale / 10), -3, -(float)((Height / 2 - 15) * WidthScale / 10));

            // Разметка горизонтальной оси
            for (int i = 1; i < 30; i++)
            {
                double t = OrdinateScale / 10 * i;
                GetScaledCoords(t, 0, out int a, out int b);
                graphics.DrawLine(AxisPen, a, b, a, b + 7);
                t = System.Math.Round(t, 2);
                string s = t.ToString();
                graphics.DrawString(s, fn, GraphicBrush, a, b + 7, sf);
                graphics.DrawLine(AxisPen, -a, b, -a, b + 7);
                s = "-" + s;
                graphics.DrawString(s, fn, GraphicBrush, -a, b + 7, sf);
            }
            // Разметка вертикальной оси
            for (int i = 1; i < 30; i++)
            {
                double t = AbscissaScale / 10 * i;
                GetScaledCoords(0, t, out int a, out int b);
                graphics.DrawLine(AxisPen, a, b, a - 7, b);
                t = System.Math.Round(t, 2);
                string s = t.ToString();
                graphics.DrawString(s, fn, GraphicBrush, a - 10, b, sfv);
                graphics.DrawLine(AxisPen, a, -b, a - 7, -b);
                s = "-" + s;
                graphics.DrawString(s, fn, GraphicBrush, a - 10, -b, sfv);
            }
        }

        public void DrawGraphic(List<PointF> points, GraphicParameters parameters, Graphics graphics)
        {
            for(int i = 1; i < points.Count; i++)
            {
                var lastPoint = points[i - 1];
                var currentPoint = points[i];
                graphics.DrawLine(GraphicPen, (float)(lastPoint.X * HeightScale), -(float)(lastPoint.Y * WidthScale), (float)(currentPoint.X * HeightScale), -(float)(currentPoint.Y * WidthScale));
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
}
