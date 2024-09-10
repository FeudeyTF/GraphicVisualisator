using GraphicVisualisator.Math;

namespace GraphicVisualisator
{
    public partial class MainPage : Form
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

        private readonly GraphicManager GraphicManager;

        private bool IsMouseDown = false;

        private Point LastMousePosition;

        private string GraphicExpression = "";

        public MainPage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            GraphicManager = new GraphicManager(new Pen(Color.Red, 3), Brushes.Black, 10, 10, panel1.Height, panel1.Width);
            GraphicManager.Resize(panel1.Width, panel1.Height);
            LastMousePosition = new();
            MouseWheel += HandleMouseWheel;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #region Events
        private void HandleMouseWheel(object? sender, MouseEventArgs args)
        {
            if (args.Delta > 0)
            {
                GraphicManager.HeightScale *= 1.1;
                GraphicManager.WidthScale *= 1.1;
                panel1.Invalidate();
            }
            else
            {
                GraphicManager.HeightScale /= 1.1;
                GraphicManager.WidthScale /= 1.1;
                panel1.Invalidate();

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs args)
        {
            IsMouseDown = false;
            Cursor = Cursors.Default;
            panel1.Invalidate();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs args)
        {
            IsMouseDown = true;

            if (IsMouseDown)
            {
                LastMousePosition.X = args.X;
                LastMousePosition.Y = args.Y;
                Cursor = Cursors.SizeAll;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs args)
        {
            if (IsMouseDown)
            {
                GraphicManager.TransformCenter(args.X - LastMousePosition.X, args.Y - LastMousePosition.Y);
                panel1.Invalidate();
                LastMousePosition.X = args.X;
                LastMousePosition.Y = args.Y;
            }
        }

        private void numericUpDownH_ValueChanged(object sender, EventArgs args)
        {
            GraphicManager.ScaleUpdate((double)numericUpDownH.Value, (double)numericUpDownV.Value);
            GraphicManager.Resize(panel1.Width, panel1.Height);
            panel1.Invalidate();
        }

        private void Tangent_ValueChanged(object sender, EventArgs args)
        {
            panel1.Invalidate();
        }

        private void TimerButton_Click(object sender, EventArgs args)
        {
            DerivativeTimer.Enabled = !DerivativeTimer.Enabled;
        }

        private void DerivativeTimer_Tick(object sender, EventArgs args)
        {
            Tangent.Value -= (decimal)0.1;
        }

        #endregion

        private void ExpressionBox_TextChanged(object sender, EventArgs args)
        {
            GraphicExpression = ExpressionBox.Text;
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs args)
        {
            GraphicManager.CreateGraphic(args.Graphics);
            if(!string.IsNullOrEmpty(GraphicExpression))
                GraphicManager.DrawExpressionGraphic(30, GraphicExpression, args.Graphics);
            GraphicManager.DrawGraphic(30, GraphicMath.Cos, args.Graphics);
            new Derivative(GraphicMath.Cos).DrawTangent((double)Tangent.Value, -30, 30, args.Graphics, GraphicManager, new Pen(Color.Green, 3));
        }
    }
}
