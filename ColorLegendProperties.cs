using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorLegend
{
    public partial class ColorLegendProperties : Form
    {
        private ColorLegend _colorLegend;
        private Font _titleFont, _exTitleFont, _labelsFont;

        public ColorLegendProperties()
        {
            InitializeComponent();
        }

        public ColorLegend ColorLegend { 
            get => _colorLegend;
            set
            { 
                _colorLegend = value;
                UpdateControls();
            } 
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                AutoValidate = AutoValidate.Disable;
                _cancelSimpleButton.PerformClick();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void UpdateControls()
        {
            if (_colorLegend == null)
            {
                _xtraTabPage1.Enabled = false;
                return;
            }

            _xtraTabPage1.Enabled = true;

            _showFrameCheckEdit.Checked = _colorLegend.ShowFrame;
            _showTitleCheckEdit.Checked = _colorLegend.ShowTitle;
            _showExTitleCheckEdit.Checked = _colorLegend.ShowExtendedTitle;

            _titleFont = new Font( _colorLegend.TitleFont, _colorLegend.TitleFont.Style);
            _exTitleFont = _colorLegend.ExtendedTitleFont;
            _labelsFont = _colorLegend.LabelsFont;


            _frameColorEdit.Color = _colorLegend.FrameColor;
            _titleColorEdit.Color = _colorLegend.TitleColor;
            _exTitleColorEdit.Color = _colorLegend.ExtendedTitleColor;
            _labelsColorEdit.Color = _colorLegend.LabelsColor;

            _titleTextEdit.Text = _colorLegend.Title;
            _exTitleTextEdit.Text = _colorLegend.ExtendedTitle;

            _showHistCheckEdit.Checked = _colorLegend.ShowHistogram;
            _showOutliersCheckEdit.Checked = _colorLegend.ShowOutliers;

            _minFromDataCheckEdit.Checked = double.IsNaN(_colorLegend.MinimumVisibleValue);
            _maxFromDataCheckEdit.Checked = double.IsNaN( _colorLegend.MaximumVisibleValue);
            SetMinValue();
            if (!_minFromDataCheckEdit.Checked)
            {
                _minValueSpinEdit.EditValue = _colorLegend.MinimumVisibleValue;
            }
            SetMaxValue();
            if (!_maxFromDataCheckEdit.Checked)
            {
                _maxValueSpinEdit.EditValue = _colorLegend.MaximumVisibleValue;
            }

            //  _minValueSpinEdit.EditValue = _colorLegend.MinimumVisibleValue;
            //   _maxValueSpinEdit.EditValue = _colorLegend.MaximumVisibleValue;

            _invertDirectionCheckEdit.Checked = _colorLegend.InvertDirection;
        }

        private void UpdateColorLegend()
        {
            if (_colorLegend == null) return;

            _colorLegend.ShowFrame = _showFrameCheckEdit.Checked;
            _colorLegend.ShowTitle = _showTitleCheckEdit.Checked;
            _colorLegend.ShowExtendedTitle = _showExTitleCheckEdit.Checked;

            _colorLegend.TitleFont = _titleFont;
            _colorLegend.ExtendedTitleFont = _exTitleFont;
            _colorLegend.LabelsFont = _labelsFont;

            _colorLegend.FrameColor = _frameColorEdit.Color;
            _colorLegend.TitleColor = _titleColorEdit.Color;
            _colorLegend.ExtendedTitleColor = _exTitleColorEdit.Color;
            _colorLegend.LabelsColor = _labelsColorEdit.Color;

            _colorLegend.Title = _titleTextEdit.Text;
            _colorLegend.ExtendedTitle = _exTitleTextEdit.Text;

            _colorLegend.ShowHistogram = _showHistCheckEdit.Checked;
            _colorLegend.ShowOutliers = _showOutliersCheckEdit.Checked;

            _colorLegend.MinimumVisibleValue = _minFromDataCheckEdit.Checked ? double.NaN : Convert.ToDouble(_minValueSpinEdit.EditValue);
            _colorLegend.MaximumVisibleValue = _maxFromDataCheckEdit.Checked ? double.NaN : Convert.ToDouble(_maxValueSpinEdit.EditValue);

            _colorLegend.InvertDirection = _invertDirectionCheckEdit.Checked;
        }

        private void ChangeFont(ref Font font)
        {
            var fontDialog = new FontDialog();
            fontDialog.Font = font;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog.Font;
            }
        }

        private void SetMaxValue()
        {
            _maxValueSpinEdit.Enabled = !_maxFromDataCheckEdit.Checked;
            if (_maxFromDataCheckEdit.Checked)
            {
                _maxValueSpinEdit.EditValue = _colorLegend.MaximumValue;
            }
        }

        private void SetMinValue()
        {
            _minValueSpinEdit.Enabled = !_minFromDataCheckEdit.Checked;
            if (_minFromDataCheckEdit.Checked)
            {
                _minValueSpinEdit.EditValue = _colorLegend.MinimumValue;
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                UpdateColorLegend();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ApplySimpleButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                UpdateColorLegend();
            }
        }

        private void LabelsSimpleButton_Click(object sender, EventArgs e)
        {
            ChangeFont(ref _labelsFont);
        }

        private void MaxFromDataCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            SetMaxValue();
        }

        private void MinFromDataCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            SetMinValue();
        }

        private void MinValueSpinEdit_Validating(object sender, CancelEventArgs e)
        {
            var min = Convert.ToDouble(_minValueSpinEdit.EditValue);
            var max = Convert.ToDouble(_maxValueSpinEdit.EditValue);
            if (double.IsNaN(min) || double.IsInfinity(min))
            {
                _errorProvider.SetError(_minValueSpinEdit, "Incorrect value");
            }
            else if (min >= max)
            {
                _errorProvider.SetError(_minValueSpinEdit, "Minimim should be smaller than max");
            }
            else
            {
                _errorProvider.SetError(_minValueSpinEdit, "");
            }
            if (!string.IsNullOrEmpty(_errorProvider.GetError(_minValueSpinEdit)))
            {
                e.Cancel = true;
            }
        }

        private void _maxValueSpinEdit_Validating(object sender, CancelEventArgs e)
        {
            var min = Convert.ToDouble(_minValueSpinEdit.EditValue);
            var max = Convert.ToDouble(_maxValueSpinEdit.EditValue);
            if (double.IsNaN(max) || double.IsInfinity(max))
            {
                _errorProvider.SetError(_maxValueSpinEdit, "Incorrect value");
            }
            else if (min >= max)
            {
                _errorProvider.SetError(_maxValueSpinEdit, "Max should be bigger than min");
            }
            else
            {
                _errorProvider.SetError(_maxValueSpinEdit, "");
            }
            if (!string.IsNullOrEmpty(_errorProvider.GetError(_maxValueSpinEdit)))
            {
                e.Cancel = true;
            }
        }

        private void ExTitleFontSimpleButton_Click(object sender, EventArgs e)
        {
            ChangeFont(ref _exTitleFont);
        }

        private void TitleFontSimpleButton_Click(object sender, EventArgs e)
        {
            ChangeFont(ref _titleFont);
        }
    }
}
