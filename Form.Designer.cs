namespace ColorLegendExample
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
            this._settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._mouseCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._colorLegend = new ColorLegendExample.ColorLegend();
            this.toolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileButton,
            this.randomDataButton,
            this._settingsToolStripButton});
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
            // _settingsToolStripButton
            // 
            this._settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._settingsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_settingsToolStripButton.Image")));
            this._settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._settingsToolStripButton.Name = "_settingsToolStripButton";
            this._settingsToolStripButton.Size = new System.Drawing.Size(53, 22);
            this._settingsToolStripButton.Text = "Settings";
            this._settingsToolStripButton.Click += new System.EventHandler(this.SettingsToolStripButton_Click);
            // 
            // _mouseCheckBox
            // 
            this._mouseCheckBox.AutoSize = true;
            this._mouseCheckBox.Location = new System.Drawing.Point(12, 33);
            this._mouseCheckBox.Name = "_mouseCheckBox";
            this._mouseCheckBox.Size = new System.Drawing.Size(95, 17);
            this._mouseCheckBox.TabIndex = 2;
            this._mouseCheckBox.Text = "Mouse clicked";
            this._mouseCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this._colorLegend);
            this.panel1.Controls.Add(this._mouseCheckBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 425);
            this.panel1.TabIndex = 3;
            // 
            // _colorLegend
            // 
            this._colorLegend.AxisNumericFormat = "F2";
            this._colorLegend.BackColor = System.Drawing.Color.Transparent;
            this._colorLegend.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Green,
        System.Drawing.Color.Blue,
        System.Drawing.Color.Indigo,
        System.Drawing.Color.Violet};
            this._colorLegend.Data = null;
            this._colorLegend.ExtendedTitle = "Extended Title";
            this._colorLegend.ExtendedTitleColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorLegend.ExtendedTitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorLegend.FixedSize = false;
            this._colorLegend.FrameColor = System.Drawing.Color.Black;
            this._colorLegend.FrameWidth = 1;
            this._colorLegend.InnerMargin = 3;
            this._colorLegend.InvertDirection = true;
            this._colorLegend.LabelNumber = -1;
            this._colorLegend.LabelsColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorLegend.LabelsFont = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._colorLegend.Location = new System.Drawing.Point(228, 76);
            this._colorLegend.MaximumVisibleValue = double.NaN;
            this._colorLegend.MinimumHistogramLength = 50;
            this._colorLegend.MinimumHistogramWidth = 10;
            this._colorLegend.MinimumVisibleValue = double.NaN;
            this._colorLegend.MouseTransparent = false;
            this._colorLegend.Name = "_colorLegend";
            this._colorLegend.Padding = new System.Windows.Forms.Padding(5);
            this._colorLegend.ShowExtendedTitle = true;
            this._colorLegend.ShowFrame = false;
            this._colorLegend.ShowHistogram = true;
            this._colorLegend.ShowLabels = true;
            this._colorLegend.ShowOutliers = true;
            this._colorLegend.ShowRibbon = true;
            this._colorLegend.ShowTitle = true;
            this._colorLegend.Size = new System.Drawing.Size(150, 250);
            this._colorLegend.TabIndex = 0;
            this._colorLegend.Title = "Title";
            this._colorLegend.TitleColor = System.Drawing.SystemColors.ControlLightLight;
            this._colorLegend.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Name = "Form";
            this.Text = "Color Legend Example";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColorLegend _colorLegend;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton loadFileButton;
        private System.Windows.Forms.ToolStripButton randomDataButton;
        private System.Windows.Forms.CheckBox _mouseCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton _settingsToolStripButton;
    }
}

