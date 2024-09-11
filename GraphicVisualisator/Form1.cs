using GraphicVisualisator.Visualisator;

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

        private string GraphicExpression = "";

        public MainPage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            GraphicManager = new GraphicManager(panel1, new Pen(Color.Red, 3), Brushes.Black, panel1.Height, panel1.Width);
            GraphicManager.Resize(panel1.Width, panel1.Height);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #region Events

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
    }
}
