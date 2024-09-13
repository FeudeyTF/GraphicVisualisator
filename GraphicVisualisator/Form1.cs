using GraphicVisualisator.Visualisator;
using GraphicVisualisator.Visualisator.Graphs;

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

        private readonly GraphManager GraphicManager;

        public MainPage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            GraphicManager = new GraphManager(panel1);
            GraphicManager.AddGraph(new Graph(System.Math.Cos), Color.Green, new(-30, 30, 0.3));
            GraphicManager.AddGraph(new Tangent(System.Math.Cos, 10), Color.Blue, new(-30, 30, 0.3));
            GraphicManager.Resize(panel1.Width, panel1.Height);
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
            panel1.Invalidate();
        }
    }
}
