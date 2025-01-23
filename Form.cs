using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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

        private void LoadCalc_Pressure()
        {
            string[] textLines = null;
            textLines = File.ReadAllLines("..\\..\\Data\\CALC_PRESSURE");
            _colorHistogram.Data = ParseText(textLines, -99);
        }

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
                _colorHistogram.Data = ParseText(textLines);
            }
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
                    //     if (!double.IsNaN(v) && !double.IsInfinity(v))
                    data.Add(v); 
                }
            }
            return data.ToArray();
        }

        private void RandomDataButton_Click(object sender, EventArgs e)
        {
            var data = new double[1000];
            var r = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = r.NextDouble();
            }
            _colorHistogram.Data = data;
        }
    }
}
