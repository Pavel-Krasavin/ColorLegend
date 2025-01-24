using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ColorHistogram
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            LoadCalc_Pressure();

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
                if (double.TryParse(textLines[i], out v))
                {
                    if (!double.IsNaN(nullValue) && v == nullValue) v = double.NaN;
                    data.Add(v);
                }
            }
            return data.ToArray();
        } 

        #endregion Private API

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

    }
}
