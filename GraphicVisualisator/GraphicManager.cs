using GraphicVisualisator;
using System.Text;

namespace WindowsFormsApplication10.MathAnalysis
{
    public class GraphicManager
    {
        public Pen GraphicPen { get; }

        public Brush GraphicBrush { get; }

        private double Height, Width;

        private double HS, VS; 

        public double hs, vs;

        private int cx, cy;

        public delegate double Function(double x);

        public delegate double PolarFunction(double x);

        public delegate double ParametricFunction(double x);


        public GraphicManager(Pen graphicPen, Brush graphicBrush, double HS, double VS, int Height, int Width)
        {
            GraphicPen = graphicPen;
            GraphicBrush = graphicBrush;
            this.HS = HS; this.VS = VS;
            this.cx = Width / 2; this.cy = Height / 2;
            this.Height = Height; this.Width = Width;
           

        }

        public void CreateGraphic(Graphics graphics)
        {
            // Рисуем оси координат
            Graphics g = graphics;
            g.TranslateTransform(cx, cy);
            Pen AxisPen = new Pen(GraphicBrush, 3);
            Font fn = new Font("Arial", (int)hs/3);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            StringFormat sfv = new StringFormat();
            sfv.Alignment = StringAlignment.Far;
            sfv.LineAlignment = StringAlignment.Center;

            // Горизонтальная ось
            g.DrawLine(AxisPen, (float)(-Width / 2 * hs / 10), 0, (float)((Width / 2 - 2) * hs / 10), 0);
            g.DrawLine(AxisPen, (float)((Width / 2 - 2) * hs / 10), 0, (float)((Width / 2 - 15) * hs / 10), 3);
            g.DrawLine(AxisPen, (float)((Width / 2 - 2) * hs / 10), 0, (float)((Width / 2 - 15) * hs / 10), -3);

            // Вертикальная ось
            g.DrawLine(AxisPen, 0, -(float)((Height / 2 - 2)*vs/10), 0, (float)((Height / 2 - 2) * vs / 10));
            g.DrawLine(AxisPen, 0, -(float)((Height / 2 + 2) * vs / 10), 3, -(float)((Height / 2 - 15) * vs / 10));
            g.DrawLine(AxisPen, 0, -(float)((Height / 2 + 2) * vs / 10), -3, -(float)((Height / 2 - 15) * vs / 10));

            // Разметка горизонтальной оси
            for (int i = 1; i < 30; i++)
            {
                double t = HS / 10 * i;
                int a, b;
                Scale(t, 0, out a, out b);
                g.DrawLine(AxisPen, a, b, a, b + 7);
                t = Math.Round(t, 2);
                string s = t.ToString();
                g.DrawString(s, fn, GraphicBrush, a, b + 7, sf);
                g.DrawLine(AxisPen, -a, b, -a, b + 7);
                s = "-" + s;
                g.DrawString(s, fn, GraphicBrush, -a, b + 7, sf);
            }
            // Разметка вертикальной оси
            for (int i = 1; i < 30; i++)
            {
                double t = VS / 10 * i;
                int a, b;
                Scale(0, t, out a, out b);
                g.DrawLine(AxisPen, a, b, a - 7, b);
                t = Math.Round(t, 2);
                string s = t.ToString();
                g.DrawString(s, fn, GraphicBrush, a - 10, b, sfv);
                g.DrawLine(AxisPen, a, -b, a - 7, -b);
                s = "-" + s;
                g.DrawString(s, fn, GraphicBrush, a - 10, -b, sfv);
            }

        }

        public void DrawGraphic(int num, Function f1, Graphics graphics, double step)
        {
            double minStep = step / 10;
            try
            {

                for (double i = -num; i < num; i += step)
                {
                    if (Math.Abs(Derivative.FindTangent(i, i, f1, 0.000001)) > 20)
                        step = minStep;
                    if (Math.Abs(Derivative.FindTangent(i, i, f1, 0.000001)) < 10)
                        step = minStep * 10;
                    if (Math.Abs(Derivative.FindTangentX(i, f1, 0.000001)) < 100 && f1(i) != 0)
                        graphics.DrawLine(GraphicPen, (float)(i * hs), -(float)(f1(i) * vs), (float)((i + step) * hs), -(float)(f1(i + step) * vs));
                    
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void DrawExpressionGraphic(int  num, string expr, Graphics g, double step)
        {
            try
            {
                bool first = true;
                int a1 = 0, b1 = 0;
                int a = 0, b = 0;
                for (double i = -num; i < num; i += step)
                {
                    StringBuilder str = new StringBuilder();
                    for(int j = 0; j < expr.Length; j++)
                    {
                        if (expr[j]=='x')
                        {
                            str.Append(i);
                            continue;
                        }
                        str.Append(expr[j]);
                    }
                    Scale(i, Parser.Parse(str.ToString()), out a1, out b1);
                    if (first)
                    {
                        a = a1; b = b1;
                        first = false;
                        continue;
                    }
                        g.DrawLine(GraphicPen, a, b, a1, b1);
                    a = a1; b = b1;
                }
            }
            catch (Exception e)
            {
            }

        }

        public void DrawPolarGraphic(double start, double end, PolarFunction f1, Graphics g, double step)
        {
            try
            {
                int a1 = 0, b1 = 0;
                int a = 0, b = 0;
                for (double phi = start; phi < end; phi += step)
                {
                    // Функция
             
                    double r = f1(phi);

                    // Перевод в декартовы
                    double x = r * Math.Cos(phi);
                    double y = r * Math.Sin(phi);

                    Scale(x, y, out a1, out b1);
                    if (phi != 0)
                        g.DrawLine(GraphicPen, a, b, a1, b1);
                    a = a1; b = b1;


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void DrawParametricGraphic(double start, double end, ParametricFunction f1, ParametricFunction f2, Graphics g, double step)
        {
            try
            {
                int a1=0, b1=0;
                int a = 0, b = 0;
                for (double i = start; i < end; i += step)
                {
                    // Функция
                    double x = f1(i);
                    double y = f2(i);

                    Scale(x, y, out a1, out b1);
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

        private void Scale(double x, double y, out int w, out int h)
        {
            w = (int)Math.Round(x * hs);
            h = -(int)Math.Round(y * vs);
        }

        public void Resize(int hs, int vs)
        {
            this.hs = hs/ (2 * HS);
            this.vs = vs/ (2 * VS);
        }

        public void ScaleUpdate(double HS, double VS)
        {
            this.HS = HS;
            this.VS = VS;
        }

        public void TransformCenter(int cx, int cy)
        {
            this.cx += cx;
            this.cy += cy;
        }
    }
}
