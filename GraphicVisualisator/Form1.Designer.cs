namespace GraphicVisualisator
{
    partial class MainPage
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownH = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownV = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Tangent = new System.Windows.Forms.NumericUpDown();
            this.DerivativeTimer = new System.Windows.Forms.Timer(this.components);
            this.TimerButton = new System.Windows.Forms.Button();
            this.DerivativeControl = new System.Windows.Forms.Label();
            this.ExpressionBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tangent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(133, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 691);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "По горизонтали";
            // 
            // numericUpDownH
            // 
            this.numericUpDownH.DecimalPlaces = 1;
            this.numericUpDownH.Location = new System.Drawing.Point(13, 31);
            this.numericUpDownH.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.Size = new System.Drawing.Size(113, 23);
            this.numericUpDownH.TabIndex = 2;
            this.numericUpDownH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownH.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownH.ValueChanged += new System.EventHandler(this.numericUpDownH_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "По вертикали";
            // 
            // numericUpDownV
            // 
            this.numericUpDownV.DecimalPlaces = 1;
            this.numericUpDownV.Location = new System.Drawing.Point(13, 76);
            this.numericUpDownV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownV.Name = "numericUpDownV";
            this.numericUpDownV.Size = new System.Drawing.Size(113, 23);
            this.numericUpDownV.TabIndex = 2;
            this.numericUpDownV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownV.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownV.ValueChanged += new System.EventHandler(this.numericUpDownH_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "X касательной";
            // 
            // Tangent
            // 
            this.Tangent.DecimalPlaces = 1;
            this.Tangent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Tangent.Location = new System.Drawing.Point(13, 121);
            this.Tangent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Tangent.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.Tangent.Name = "Tangent";
            this.Tangent.Size = new System.Drawing.Size(113, 23);
            this.Tangent.TabIndex = 2;
            this.Tangent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tangent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Tangent.ValueChanged += new System.EventHandler(this.Tangent_ValueChanged);
            // 
            // DerivativeTimer
            // 
            this.DerivativeTimer.Tick += new System.EventHandler(this.DerivativeTimer_Tick);
            // 
            // TimerButton
            // 
            this.TimerButton.Location = new System.Drawing.Point(14, 151);
            this.TimerButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TimerButton.Name = "TimerButton";
            this.TimerButton.Size = new System.Drawing.Size(112, 27);
            this.TimerButton.TabIndex = 3;
            this.TimerButton.Text = "Start";
            this.TimerButton.UseVisualStyleBackColor = true;
            this.TimerButton.Click += new System.EventHandler(this.TimerButton_Click);
            // 
            // DerivativeControl
            // 
            this.DerivativeControl.AutoSize = true;
            this.DerivativeControl.Location = new System.Drawing.Point(15, 186);
            this.DerivativeControl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DerivativeControl.Name = "DerivativeControl";
            this.DerivativeControl.Size = new System.Drawing.Size(59, 15);
            this.DerivativeControl.TabIndex = 4;
            this.DerivativeControl.Text = "Derivative";
            // 
            // ExpressionBox
            // 
            this.ExpressionBox.Location = new System.Drawing.Point(15, 204);
            this.ExpressionBox.Name = "ExpressionBox";
            this.ExpressionBox.Size = new System.Drawing.Size(100, 23);
            this.ExpressionBox.TabIndex = 5;
            this.ExpressionBox.Tag = "";
            this.ExpressionBox.TextChanged += new System.EventHandler(this.ExpressionBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 719);
            this.Controls.Add(this.ExpressionBox);
            this.Controls.Add(this.DerivativeControl);
            this.Controls.Add(this.TimerButton);
            this.Controls.Add(this.Tangent);
            this.Controls.Add(this.numericUpDownV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownH);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "График";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tangent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Tangent;
        private System.Windows.Forms.Timer DerivativeTimer;
        private System.Windows.Forms.Button TimerButton;
        private System.Windows.Forms.Label DerivativeControl;
        private TextBox ExpressionBox;
    }
}

