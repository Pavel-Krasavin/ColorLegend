using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ColorLegend
{
    internal class Labels : Rectangular
    {
        public const int MaxLabelCount = 100;

        #region Private Data Members

        private double _minValue = 0, _maxValue = 1;
        private int _labelNumber = -1, _lineLength = 5;
        private string _numFormat = "F1";
        private Color _color = SystemColors.ControlLightLight;
        private Font _font;
        protected int _topBottomPadding = 10;
        private bool _invertDirection;
        private bool _ticksToTheLeft = false;
        private Label[] _labels = new Label[MaxLabelCount];
        private int _labelCount;
        private float _maxLabelLength, _halfTextHeight;
        private static Graphics _graphics;

        #endregion Private Data Members

        #region Constructors

        public Labels(Control control) : base(control)
        {
            Color = SystemColors.ControlLightLight;
            NumericFormat = "F2";
            Font = new Font("Microsoft San Serif", 8);
        }

        #endregion Constructors

        #region Public API

        /// <summary>
        /// Gets or sets axis minimum value.
        /// </summary>
        public double MinimumValue
        {
            get => _minValue;
            set
            {
                if (value != _minValue && !double.IsNaN(value) && !double.IsInfinity(value))
                {
                    _minValue = value;
                    CalculateLabels();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis maximum value.
        /// </summary>
        public double MaximumValue
        {
            get => _maxValue;
            set
            {
                if (value != _maxValue && !double.IsNaN(value) && !double.IsInfinity(value))
                {
                    _maxValue = value;
                    CalculateLabels();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a number of labels in the axis. Negative number means adaptive calculation. Max is 100.
        /// </summary>
        public int LabelNumber
        {
            get => _labelNumber;
            set
            {
                if (value != _labelNumber)
                {
                    _labelNumber = value;
                    CalculateLabels();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether show ticks to the left or to the right of the labels.
        /// </summary>
        public bool TicksToTheLeft
        {
            get => _ticksToTheLeft;
            set
            {
                if (value != _ticksToTheLeft)
                {
                    _ticksToTheLeft = value;
                    CalculateLabels();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis numeric format.
        /// </summary>
        public string NumericFormat
        {
            get => _numFormat;
            set
            {
                if (value != _numFormat)
                {
                    _numFormat = value;
                    CalculateLabels();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis font.
        /// </summary>
        public Font Font
        {
            get => _font;
            set
            {
                if (value != _font)
                {
                    _font = value;
                    CalculateLabels();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis color.
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
        /// Gets or sets value of the top and bottom padding.
        /// </summary>
        public int TopBottomPadding
        {
            get => _topBottomPadding;
            set
            {
                if (_topBottomPadding != value)
                {
                    _topBottomPadding = value;
                    CalculateLabels();
                }
            }
        }

        /// <summary>
        /// Gets labels width necessary for proper drawing.
        /// </summary>
        public int PreferredWidth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets flag indicating whether minimum values should be on top.
        /// </summary>
        public bool InvertDirection
        {
            get => _invertDirection;
            set
            {
                if (value != _invertDirection)
                {
                    _invertDirection = value;
                    CalculateLabels();
                }
            }
        }

        #endregion Public API

        #region Overrides

        /// <summary>
        /// Draws the axis.
        /// </summary>
        public override void Draw(Graphics g)
        {
            if (double.IsNaN(MaximumValue) || double.IsInfinity(MaximumValue) ||
                double.IsNaN(MinimumValue) || double.IsInfinity(MinimumValue)) return;
            if (InnerBounds.Height == 0 || InnerBounds.Width == 0) return;
            if (LabelNumber < 2 && LabelNumber >= 0) return;

            var range = MaximumValue - MinimumValue;
            if (range > double.MaxValue) return;

            //var textHeight = g.MeasureString("A", Font).Height;
            //var firstTick = LabelNumber < 0 ? DecimalRound(MinimumValue) : MinimumValue;
            //double nTicks = LabelNumber;
            
            //var scale = range / InnerBounds.Height;
            //double vStep = 1;
            //if (LabelNumber < 0)
            //{
            //    // let's say
            //    var pixelStep = 3 * (double)textHeight;
            //    vStep = pixelStep * scale;
            //    var exponent = Math.Pow(10.0, Math.Floor(Math.Log10(vStep)));
            //    var mantissa = Math.Round(vStep / exponent);
            //    // get step to be 1, 2 or 5 powers of 10
            //    if (mantissa > 1 && mantissa < 2) mantissa = 2;
            //    else if (mantissa > 2 && mantissa < 5) mantissa = 5;
            //    else if (mantissa > 5) mantissa = 10;
            //    vStep = mantissa * exponent;
            //    nTicks = (MaximumValue - firstTick) / vStep;
            //}
            //else
            //{
            //    nTicks = LabelNumber;
            //    vStep = range / (LabelNumber - 1);
            //}

            var pen = new Pen(Color);
            var brush = new SolidBrush(Color);
            var prevClip = g.ClipBounds;
            g.SetClip(Bounds);
            for (int i = 0; i < _labelCount; i++)
            {
                var p = _labels[i].Y;
                var s = _labels[i].Text;
                if (TicksToTheLeft)
                {
                    g.DrawLine(pen, Bounds.Left, p, Bounds.Left + _lineLength, p);
                    g.DrawString(s, Font,
                        brush, Bounds.Left + _lineLength, p - _halfTextHeight);
                }
                else
                {
                    g.DrawLine(pen, Bounds.Right - _lineLength, p, Bounds.Right, p);
                    g.DrawString(s, Font,
                        brush, Bounds.Right - _lineLength - _labels[i].Width, p - _halfTextHeight);
                }
            }
            //for (int i = 0; i < nTicks + 1; i++)
            //{
            //    var v = firstTick + i * vStep;
            //    if (v < MinimumValue - Helper.TOLERANCE || v > MaximumValue + Helper.TOLERANCE) continue;
            //    var p = (int) ((v - MinimumValue) / scale + InnerBounds.Y);
            //    if (TicksToTheLeft)
            //    {
            //        g.DrawLine(pen, Bounds.Left, p, Bounds.Left + _lineLength, p);
            //        g.DrawString(v.ToString(NumericFormat), Font,
            //            brush, Bounds.Left + _lineLength, p - textHeight / 2);
            //    }
            //    else
            //    {
            //        g.DrawLine(pen, Bounds.Right - _lineLength, p, Bounds.Right, p);
            //        g.DrawString(v.ToString(NumericFormat), Font,
            //            brush, Bounds.Left, p - textHeight / 2);
            //    }
            //}

            g.SetClip(prevClip);
        }

        public override Rectangle Bounds 
        { 
            get => base.Bounds;
            set {
                if (value != Bounds)
                {
                    base.Bounds = value;
                    CalculateLabels();
                }
            }
        }

        #endregion Overrides

        #region Private API

        private Graphics GetGraphics()
        {
            if (_graphics == null)
            {
                var bmp = new Bitmap(1, 1);
                _graphics = Graphics.FromImage(bmp);
            }
            return _graphics;
        }

        private void CalculateLabels()
        {
            if (double.IsNaN(MaximumValue) || double.IsInfinity(MaximumValue) ||
               double.IsNaN(MinimumValue) || double.IsInfinity(MinimumValue) ||
               InnerBounds.Height == 0 || InnerBounds.Width == 0 ||
               LabelNumber < 2 && LabelNumber >= 0)
            {
                this._labelCount = 0;
                return; 
            }
           
            var range = MaximumValue - MinimumValue;
            if (range > double.MaxValue) return;

            var bmp = new Bitmap(1, 1);
            var g = GetGraphics();
            _halfTextHeight = g.MeasureString("A", Font).Height / 2;
            var firstTick = MinimumValue;
        //    var lastTick = DecimalRound(MaximumValue);

            var scale = range / InnerBounds.Height;
            double vStep = 1;
            if (LabelNumber < 0)
            {
                // let's say
                var pixelStep = 5 * (double) _halfTextHeight;
                vStep = pixelStep * scale;
                var exponent = Math.Pow(10.0, Math.Floor(Math.Log10(vStep)));
                var mantissa = Math.Round(vStep / exponent);
                // get step to be 1, 2 or 5 powers of 10
                if (mantissa > 1 && mantissa < 2) mantissa = 2;
                else if (mantissa > 2 && mantissa < 5) mantissa = 5;
                else if (mantissa > 5) mantissa = 10;
                vStep = mantissa * exponent;
                var rem = MinimumValue % vStep;
                if (Helper.AlmostEqual(rem, 0)) firstTick = MinimumValue;
                else firstTick = (MinimumValue - rem) + vStep;
                _labelCount = (int) ((MaximumValue - firstTick) / vStep) + 1;
            }
            else
            {
                _labelCount = LabelNumber;
                vStep = range / (LabelNumber - 1);
            }

            _maxLabelLength = 0;

            for (int i = 0; i < _labelCount; i++)
            {
                var v = firstTick + i * vStep;
                if (v < MinimumValue - Helper.TOLERANCE || v > MaximumValue + Helper.TOLERANCE) continue;
                var p = (float) (InvertDirection ? 
                    (InnerBounds.Bottom - (v - MinimumValue) / scale) :
                    ((v - MinimumValue) / scale + InnerBounds.Y));
                var t = v.ToString(NumericFormat);
                var w = g.MeasureString(t, Font).Width;
                
                _maxLabelLength = Math.Max(_maxLabelLength, w);
                _labels[i] = new Label { Width = w, Text = t, Y = p };
            }

            Invalidate();
        }

        private int GetLabelNumber(Graphics g)
        {
            if (LabelNumber > 1) return LabelNumber;

            var textHeight = g.MeasureString("A", Font).Height;
            var tStep = (2 * (double) textHeight);
            var range = MaximumValue - MinimumValue;
            var scale = range / InnerBounds.Height;
            var mStep = tStep * scale;

            mStep = DecimalRound(mStep);
            return (int) (range / mStep);
        }

        private double GetValueStep(Graphics g)
        {
            if (LabelNumber > 1) return LabelNumber;

            var textHeight = g.MeasureString("A", Font).Height;
            var pStep = (2 * (double)textHeight);

            var range = MaximumValue - MinimumValue;
            var scale = range / InnerBounds.Height;
            var vStep = pStep * scale;

            var exponent = (int) Math.Pow(10.0, Math.Floor(Math.Log10(vStep)));
            var mantissa = (int) Math.Round(vStep / exponent);

            // get step to be 1, 2 or 5 powers of 10
            if (mantissa > 1 && mantissa < 2) mantissa = 2;
            else if (mantissa > 2 && mantissa < 5) mantissa = 5;
            else if (mantissa > 5) mantissa = 10;
            return mantissa * exponent;
        }

        private double DecimalRound(double v)
        {
            var exponent = Math.Pow(10.0, Math.Floor(Math.Log10(v)));
            var mantissa = (int)Math.Round(v / exponent);
            return mantissa * exponent;
        }

        /// <summary>
        /// Gets bounds without <see cref="TopBottomPadding"/>.
        /// </summary>
        private Rectangle InnerBounds
        {
            get
            {
                return new Rectangle(Bounds.X, Bounds.Y + _topBottomPadding,
                    Bounds.Width, Bounds.Height - 2 * _topBottomPadding);
            }
        }

        #endregion Private API

        /// <summary>
        /// Structure to holdd label drawing parameters.
        /// </summary>
        private struct Label
        {
            public float Width;
            public string Text;
            public float Y;
        }

    }
}
