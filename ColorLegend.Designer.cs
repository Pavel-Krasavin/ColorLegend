namespace ColorLegendExample
{
    partial class ColorLegend
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._titleLabel = new System.Windows.Forms.Label();
            this._exTitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoEllipsis = true;
            this._titleLabel.AutoSize = true;
            this._titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._titleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this._titleLabel.Location = new System.Drawing.Point(22, 14);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(38, 16);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Title";
            this._titleLabel.Visible = false;
            // 
            // _exTitleLabel
            // 
            this._exTitleLabel.AutoSize = true;
            this._exTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this._exTitleLabel.Location = new System.Drawing.Point(27, 44);
            this._exTitleLabel.Name = "_exTitleLabel";
            this._exTitleLabel.Size = new System.Drawing.Size(75, 13);
            this._exTitleLabel.TabIndex = 1;
            this._exTitleLabel.Text = "Extended Title";
            this._exTitleLabel.Visible = false;
            // 
            // ColorLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this._exTitleLabel);
            this.Controls.Add(this._titleLabel);
            this.Name = "ColorLegend";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(173, 254);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _exTitleLabel;
    }
}
