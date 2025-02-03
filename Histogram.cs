using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorLegend
{
    internal class Histogram : ColorRibbon
    {

        #region Private Data Members

        private double[] _data;
        // changable values for leftmost and rightmost pixel bins
        private double _minValue = double.NaN, _maxValue = double.NaN;
        // minimum and maximum data values
        private double _min = 0, _max = 1;
        private int[] _histData;
        // value of the highest bar
        private int _maxBar;
        private Color _color;
        private bool _showOutliers;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="control">Parent control.</param>
        public Histogram(Control control) : base(control)
        {
        }

        #endregion Constructors

        #region Public API

        /// <summary>
        /// Gets or sets histogram data.
        /// </summary>
        public double[] Data
        {
            get => _data;
            set
            {
                _data = value;
                DefineDataMinMax();
                PrepareHistogramData();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a maximum value to draw.
        /// </summary>
        public double MaximumVisibleValue
        {
            get => _maxValue;
            set
            {
                if (value != _maxValue && !double.IsInfinity(value))
                {
                    _maxValue = value <= MinimumVisibleValue ? MinimumVisibleValue + Helper.TOLERANCE : value;
                    PrepareHistogramData();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a minimum value to draw.
        /// </summary>
        public double MinimumVisibleValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                if (value != _minValue && !double.IsInfinity(value))
                {
                    _minValue = value > MaximumVisibleValue ? MaximumVisibleValue - Helper.TOLERANCE : value;
                    PrepareHistogramData();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets data maximum value.
        /// </summary>
        public double MaximumValue => _max;

        /// <summary>
        /// Gets data minimum value.
        /// </summary>
        public double MinimumValue => _min;

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                if (value != _color)
                {
                    _color = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show the histogram outliers.
        /// </summary>
        public bool ShowOutliers
        {
            get => _showOutliers;
            set
            {
                if (value != _showOutliers)
                {
                    _showOutliers = value;
                    Invalidate();
                }
            }
        }

        #endregion Public API

        #region Overrides

        /// <summary>
        /// Draws the Histogram.
        /// </summary>
        public override void Draw(Graphics g)
        {
            if (NoData) return;
            base.Draw(g);
            DrawOutliers(g);
            if (Helper.AlmostEqual(_max, _min)) return;
            DrawHistogram(g);
        }

        /// <summary>
        /// Gets or sets bounds of the shape.
        /// </summary>
        public override Rectangle Bounds
        {
            get => base.Bounds;
            set
            {
                base.Bounds = value;
                PrepareHistogramData();
                Invalidate();
            }
        }

        #endregion Overrides

        #region Private API

        /// <summary>
        /// Draws a mask of the background color over the <see cref="ColorRibbon"/> to get a histogram.
        /// </summary>
        private void DrawHistogram(Graphics g)
        {
            if (Helper.AlmostEqual(0, _maxBar)) return;
            var pen = new Pen(Color);
            var w = InnerBounds.Width;
            for (int i = 0; i < InnerBounds.Height; i++)
            {
                var l = (int)Math.Round(w - w * (_histData[i] / (float)_maxBar));
                g.DrawLine(pen, InnerBounds.Right, InnerBounds.Y + i, InnerBounds.Right - l, InnerBounds.Y + i);
            }
        }

        /// <summary>
        /// Draws top and bottom outliers if necessary.
        /// </summary>
        private void DrawOutliers(Graphics g) 
        {
            if (!ShowOutliers) return;

            if (MinimumVisibleValue > _min)
            {
                var rect = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, TopBottomPadding);
                var linBrush = new LinearGradientBrush(rect, Color, Colors[0], 90);
                g.FillRectangle(linBrush, rect);
            }
            if (MaximumVisibleValue < _max)
            {
                var rect = new Rectangle(Bounds.X, InnerBounds.Bottom, Bounds.Width, TopBottomPadding);
                var c = Colors.Length > 1 ? Colors[Colors.Length - 1] : Colors[0];
                var linBrush = new LinearGradientBrush(rect, c, Color, 90);
                g.FillRectangle(linBrush, rect);
            }
        }

        /// <summary>
        /// Calculates minimum and maximum data values.
        /// </summary>
        private void DefineDataMinMax()
        {
            if (Data == null || Data.Length == 0) return;
            _min = double.MaxValue; _max = double.MinValue;
            foreach (var d in _data)
            {
                if (double.IsNaN(d) || double.IsInfinity(d)) continue;
                if (d < _min) _min = d;
                if (d > _max) _max = d;
            }
        }

        /// <summary>
        /// Calculates sizes of pixel bins.
        /// </summary>
        private void PrepareHistogramData()
        {
            if (InnerBounds.Height <= 0) return;
            var binN = InnerBounds.Height;
            _histData = new int[binN];
            if (NoData) return;
            var max = double.IsNaN(MaximumVisibleValue) ? _max : MaximumVisibleValue;
            var min = double.IsNaN(MinimumVisibleValue) ? _min : MinimumVisibleValue;

            if (Helper.AlmostEqual(max, min)) return;

            var step = (max - min) / binN;
            _maxBar = 0;
            foreach (var d in _data)
            {
                if (double.IsNaN(d) || double.IsInfinity(d) || 
                    d < min || d > max) continue;
                var n = (int)((d - min) / step);
                if (n == binN) n--;
                _histData[n]++;
                _maxBar = Math.Max(_maxBar, _histData[n]);
            }
        }

        /// <summary>
        /// Gets a flag indicating whether there is data to draw.
        /// </summary>
        private bool NoData
        {
            get
            {
                return Data == null || Data.Length == 0;
            }
        }

        #endregion Private API

    }
}
