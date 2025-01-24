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
            this._colorLegend = new ColorHistogram.ColorLegend();
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
            this._colorLegend.AxisColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorLegend.AxisFont = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorLegend.AxisNumericFormat = "F1";
            this._colorLegend.BackColor = System.Drawing.SystemColors.Desktop;
            this._colorLegend.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Green,
        System.Drawing.Color.Blue,
        System.Drawing.Color.Indigo,
        System.Drawing.Color.Violet};
            this._colorLegend.Data = null;
            this._colorLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorLegend.InnerMargin = 6;
            this._colorLegend.LabelNumber = -1;
            this._colorLegend.Location = new System.Drawing.Point(139, 73);
            this._colorLegend.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this._colorLegend.MaximumHistogramLength = 500;
            this._colorLegend.MaximumHistogramWidth = 100;
            this._colorLegend.MaximumValue = double.NaN;
            this._colorLegend.MinimumHistogramLength = 20;
            this._colorLegend.MinimumHistogramWidth = 4;
            this._colorLegend.MinimumValue = double.NaN;
            this._colorLegend.Name = "_colorHistogram";
            this._colorLegend.Padding = new System.Windows.Forms.Padding(10);
            this._colorLegend.ShowAxis = true;
            this._colorLegend.ShowHistogram = true;
            this._colorLegend.ShowTitle = true;
            this._colorLegend.ShowUnits = true;
            this._colorLegend.Size = new System.Drawing.Size(349, 312);
            this._colorLegend.TabIndex = 0;
            this._colorLegend.Title = "Title";
            this._colorLegend.TitleColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorLegend.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorLegend.Units = "Units";
            this._colorLegend.UnitsColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorLegend.UnitsFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this._colorLegend);
            this.Name = "Form";
            this.Text = "Color Histogram Example";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorLegend _colorLegend;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton loadFileButton;
        private System.Windows.Forms.ToolStripButton randomDataButton;
    }
}

