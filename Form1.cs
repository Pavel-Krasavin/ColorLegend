using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ColorLegend
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        public Form1()
        {
            InitializeComponent();
    //        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            LoadCalc_Pressure();
            panel1.MouseClick += OnMouseClick;
        }

        #region Private API

        private void LoadCalc_Pressure()
        {
            string[] textLines = null;
            textLines = File.ReadAllLines("..\\..\\Data\\CALC_PRESSURE");
            _colorLegend.Data = ParseText(textLines, -99);
        }


        private double[] ParseText(string[] textLines, double nullValue = double.NaN)
        {
            var data = new List<double>();
            double v;
            for (int i = 0; i < textLines.Length; i++)
            {
                if (double.TryParse(textLines[i], NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                {
                    if (!double.IsNaN(nullValue) && v == nullValue) v = double.NaN;
                    data.Add(v);
                }
            }
            return data.ToArray();
        }

        #endregion Private API


        protected void OnMouseClick(object sender, MouseEventArgs e)
        {
            base.OnMouseClick(e);
            _mouseCheckBox.Checked = !_mouseCheckBox.Checked;
        }

        #region Event Handlers

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            string[] textLines = null;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textLines = File.ReadAllLines(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception while reading the file: " + ex.Message);
                }
                _colorLegend.Data = ParseText(textLines);
            }
        }

        private void RandomDataButton_Click(object sender, EventArgs e)
        {
            var data = new double[1000];
            var r = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = r.NextDouble();
            }
            _colorLegend.Data = data;
        }

        #endregion Event Handlers

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            var propertiesForm = new ColorLegendProperties();
            propertiesForm.ColorLegend = _colorLegend;
            propertiesForm.ShowDialog();
        }

    }
}