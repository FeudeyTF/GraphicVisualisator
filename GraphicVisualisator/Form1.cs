using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication10.MathAnalysis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using GraphicVisualisator;
using NReco.Linq;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
        GraphicManager graphicsManager;
        Derivative derivative;
        Parser parser = new Parser();
        //Graphic Moves
        bool isMouseDown = false;
        int x1, y1;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            graphicsManager = new GraphicManager(new Pen(Color.Red, 3), Brushes.Black, 10, 10, panel1.Height, panel1.Width);
            graphicsManager.Resize(panel1.Width, panel1.Height);
            derivative = new Derivative(graphicsManager, new Pen(Color.Green, 2));
            this.MouseWheel += new MouseEventHandler(Wheel);
        }

        #region Events
        private void Wheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                graphicsManager.hs *= 1.1;
                graphicsManager.vs *= 1.1;
                panel1.Invalidate();
            }
            else
            {
                graphicsManager.hs /= 1.1;
                graphicsManager.vs /= 1.1;
                panel1.Invalidate();

            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Cursor = Cursors.Default;
            panel1.Invalidate();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            if (isMouseDown)
            {
                x1 = e.X;
                y1 = e.Y;
                Cursor = Cursors.SizeAll;
            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                graphicsManager.TransformCenter(e.X - x1, e.Y - y1);
                panel1.Invalidate();
                x1 = e.X;
                y1 = e.Y;
            }

        }

        private void numericUpDownH_ValueChanged(object sender, EventArgs e)
        {
            graphicsManager.ScaleUpdate((double)numericUpDownH.Value, (double)numericUpDownV.Value);
            graphicsManager.Resize(panel1.Width, panel1.Height);
            panel1.Invalidate();
        }
        private void Tangent_ValueChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
        private void TimerButton_Click(object sender, EventArgs e)
        {
            DerivativeTimer.Enabled = !DerivativeTimer.Enabled;

        }
        private void DerivativeTimer_Tick(object sender, EventArgs e)
        {
            Tangent.Value -= (decimal)0.1;
        }
        #endregion

        #region Functions
        public double CustomFunction(double x)
        {
            return Math.Log2(4 * x - x * x - 2);
        }
        public double CustomFunction2(double x)
        {
            return Math.Pow(5, Math.Abs(x - 2));
        }
        public double Cos(double x)
        {
            return GraphicMath.Cos(x);
        }
        public double Sin(double x)
        {
            return GraphicMath.Sin(x);
        }
        public double Para1(double x)
        {
            return 13 * Math.Cos(x) - 3 * Math.Cos(13 / 3 * x);
        }
        public double Para2(double x)
        {
            return 13 * Math.Sin(x) - 3 * Math.Sin(13 / 3 * x);
        }
        public double PolarFunc(double x)
        {
            return Math.Exp(x);
        }
        #endregion


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            graphicsManager.CreateGraphic(e.Graphics);

            #region Examples
            // graphicsManager.DrawGraphic(10, Cos, e.Graphics, 0.1); // Косинус в Декартовых координатах
            // derivative.DrawTangent((double)Tangent.Value, 30, 1, Cos, e.Graphics, 0.00001); // Касательная к косинусу в Декартовых координатах
            // DerivativeControl.Text = Derivative.TangentFormula((double)Tangent.Value, Cos, 0.00001); // Уравнение касательной к косинусу в Декартовых координатах
            // graphicsManager.DrawPolarGraphic(0, Math.PI*4, PolarFunc, e.Graphics, 0.01); // График функции в полярных координатах
            // graphicsManager.DrawParametricGraphic(0, 10, Para2, Para1, e.Graphics, 0.01);
            #endregion

            // Постройка Функций
            // graphicsManager.DrawGraphic(10, CustomFunction, e.Graphics, 0.1);
            // graphicsManager.DrawGraphic(10, CustomFunction2, e.Graphics, 0.1);
            // derivative.DrawTangent((double)Tangent.Value, 30, 1, CustomFunction, e.Graphics, 0.00001); // Касательная к косинусу в Декартовых координатах
            string expr = "cos(10)";
            //graphicsManager.DrawExpressionGraphic(10, expression, e.Graphics, 0.1);
            StringBuilder str = new StringBuilder();
            for (int j = 0; j < expr.Length; j++)
            {
                if (expr[j] == 'x')
                {
                    str.Append(10);
                    continue;
                }
                str.Append(expr[j]);
            }
            DerivativeControl.Text = parser.Parse(str.ToString()).ToString();

        }
    }
}
