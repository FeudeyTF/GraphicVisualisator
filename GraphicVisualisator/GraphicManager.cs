using GraphicVisualisator.Math;

namespace GraphicVisualisator
{
    public delegate double Function(double x);

    public class GraphicManager
    {
        public Pen GraphicPen { get; }

        public Brush GraphicBrush { get; }

        private readonly int Height, Width;

        private double OrdinateScale, AbscissaScale; // Y and X axes scales

        public double HeightScale, WidthScale;

        private Point Center;

        public GraphicManager(Pen pen, Brush brush, double ordinateScale, double abscissaScale, int height, int width)
        {
            Height = height;
            Width = width;
            GraphicPen = pen;
            GraphicBrush = brush;
            OrdinateScale = ordinateScale;
            AbscissaScale = abscissaScale;
            Center = new(Width / 2, Height / 2);
        }

        public void CreateGraphic(Graphics graphics)
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

        public void DrawGraphic(int num, Function f1, Graphics graphics, double step = Constants.GRAPHIC_STEP)
        {
            double minStep = step / 10;
            for (double i = -num; i < num; i += step)
            {
                if (System.Math.Abs(Derivative.FindTangent(i, i, f1, 0.000001f)) > 20)
                    step = minStep;
                if (System.Math.Abs(Derivative.FindTangent(i, i, f1, 0.000001f)) < 10)
                    step = minStep * 10;
                if (System.Math.Abs(Derivative.FindTangentX(i, f1, 0.000001f)) < 100 && f1(i) != 0)
                    graphics.DrawLine(GraphicPen, (float)(i * HeightScale), -(float)(f1(i) * WidthScale), (float)((i + step) * HeightScale), -(float)(f1(i + step) * WidthScale));

            }
        }

        public void DrawExpressionGraphic(int num, string expr, Graphics g, double step = Constants.GRAPHIC_STEP)
        {
            try
            {
                GetScaledCoords(-num, Parser.Parse(expr.Replace("x", (-num).ToString())), out int lastX, out int lastY);
                for (double i = -num + step; i < num; i += step)
                {
                    GetScaledCoords(i, Parser.Parse(expr.Replace("x", i.ToString())), out int scaledX, out int scaledY);
                    g.DrawLine(GraphicPen, lastX, lastY, scaledX, scaledY);
                    lastX = scaledX; lastY = scaledY;
                }
            }
            catch (Exception)
            {
            }
        }

        public void DrawPolarGraphic(double start, double end, Function polarFunction, Graphics g, double step = Constants.GRAPHIC_STEP)
        {
            try
            {
                int scaledX = 0, scaledY = 0;
                int a = 0, b = 0;
                for (double phi = start; phi < end; phi += step)
                {
                    // Функция

                    double r = polarFunction(phi);

                    // Перевод в декартовы
                    double x = r * System.Math.Cos(phi);
                    double y = r * System.Math.Sin(phi);

                    GetScaledCoords(x, y, out scaledX, out scaledY);
                    if (phi != 0)
                        g.DrawLine(GraphicPen, a, b, scaledX, scaledY);
                    a = scaledX; b = scaledY;


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void DrawParametricGraphic(double start, double end, Function xFunction, Function yFunction, Graphics g, double step = Constants.GRAPHIC_STEP)
        {
            try
            {
                int a1 = 0, b1 = 0;
                int a = 0, b = 0;
                for (double i = start; i < end; i += step)
                {
                    // Функция
                    double x = xFunction(i);
                    double y = yFunction(i);

                    GetScaledCoords(x, y, out a1, out b1);
                    if (i != 0)
                        g.DrawLine(GraphicPen, a, b, a1, b1);
                    a = a1; b = b1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
