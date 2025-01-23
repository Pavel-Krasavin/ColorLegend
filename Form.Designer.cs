namespace ColorHistogram
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.loadFileButton = new System.Windows.Forms.ToolStripButton();
            this.randomDataButton = new System.Windows.Forms.ToolStripButton();
            this._colorHistogram = new ColorHistogram.ColorLegend();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileButton,
            this.randomDataButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // loadFileButton
            // 
            this.loadFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadFileButton.Image = ((System.Drawing.Image)(resources.GetObject("loadFileButton.Image")));
            this.loadFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(56, 22);
            this.loadFileButton.Text = "Load file";
            this.loadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // randomDataButton
            // 
            this.randomDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.randomDataButton.Image = ((System.Drawing.Image)(resources.GetObject("randomDataButton.Image")));
            this.randomDataButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.randomDataButton.Name = "randomDataButton";
            this.randomDataButton.Size = new System.Drawing.Size(82, 22);
            this.randomDataButton.Text = "Random data";
            this.randomDataButton.Click += new System.EventHandler(this.RandomDataButton_Click);
            // 
            // _colorHistogram
            // 
            this._colorHistogram.AxisColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorHistogram.AxisFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorHistogram.AxisNumericFormat = "F1";
            this._colorHistogram.BackColor = System.Drawing.SystemColors.Desktop;
            this._colorHistogram.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Green,
        System.Drawing.Color.Blue,
        System.Drawing.Color.Indigo,
        System.Drawing.Color.Violet};
            this._colorHistogram.Data = null;
            this._colorHistogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorHistogram.InnerMargin = 6;
            this._colorHistogram.Location = new System.Drawing.Point(139, 73);
            this._colorHistogram.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this._colorHistogram.MaximumHistogramLength = 500;
            this._colorHistogram.MaximumHistogramWidth = 100;
            this._colorHistogram.MaximumValue = double.NaN;
            this._colorHistogram.MinimumHistogramLength = 20;
            this._colorHistogram.MinimumHistogramWidth = 4;
            this._colorHistogram.MinimumValue = double.NaN;
            this._colorHistogram.Name = "_colorHistogram";
            this._colorHistogram.Padding = new System.Windows.Forms.Padding(10);
            this._colorHistogram.ShowAxis = true;
            this._colorHistogram.ShowHistogram = true;
            this._colorHistogram.ShowTitle = true;
            this._colorHistogram.ShowUnits = true;
            this._colorHistogram.Size = new System.Drawing.Size(349, 312);
            this._colorHistogram.TabIndex = 0;
            this._colorHistogram.Title = "Title";
            this._colorHistogram.TitleColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorHistogram.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorHistogram.Units = "Units";
            this._colorHistogram.UnitsColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorHistogram.UnitsFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this._colorHistogram);
            this.Name = "Form";
            this.Text = "Color Histogram Example";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorLegend _colorHistogram;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton loadFileButton;
        private System.Windows.Forms.ToolStripButton randomDataButton;
    }
}

