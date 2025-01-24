using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorHistogram
{
    internal class Axis : Rectangular
    {

        #region Private Data Members

        private double _minValue = 0, _maxValue = 1;
        private int _labelNumber = -1, _lineLength = 5;
        private string _numFormat = "F1", _units;
        private Color _color = SystemColors.ControlLightLight;
        private Font _font;
        private Brush _brush = new SolidBrush(SystemColors.ControlLightLight);
        private Control _parentControl;
        protected int _topBottomPadding = 10;

        #endregion Private Data Members

        #region Constructors

        public Axis(Control control) : base(control)
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
            get
            {
                return _minValue;
            }
            set
            {
                if (value != _minValue && !double.IsNaN(value) && !double.IsInfinity(value))
                {
                    _minValue = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis maximum value.
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                if (value != _maxValue && !double.IsNaN(value) && !double.IsInfinity(value))
                {
                    _maxValue = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a number of labels in the axis. Negative number means adaptive calculation.
        /// </summary>
        public int LabelNumber
        {
            get
            {
                return _labelNumber;
            }
            set
            {
                if (value != _labelNumber)
                {
                    _labelNumber = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis numeric format.
        /// </summary>
        public string NumericFormat
        {
            get
            {
                return _numFormat;
            }
            set
            {
                if (value != _numFormat)
                {
                    _numFormat = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis font.
        /// </summary>
        public Font Font
        {
            get { return _font; }
            set
            {
                if (value != _font)
                {
                    _font = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets axis color.
        /// </summary>
        public Color Color
        {
            get { return _color; }
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
            get
            {
                return _topBottomPadding;
            }
            set
            {
                if (_topBottomPadding != value)
                {
                    _topBottomPadding = value;
                    Invalidate();
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

            var textHeight = g.MeasureString("A", Font).Height;
            var firstTick = LabelNumber < 0 ? DecimalRound(MinimumValue) : MinimumValue;
            double nTicks = LabelNumber;
            
            var scale = range / InnerBounds.Height;
            double vStep = 1;
            if (LabelNumber < 0)
            {
                // let's say
                var pixelStep = 3 * (double)textHeight;
                vStep = pixelStep * scale;
                var exponent = Math.Pow(10.0, Math.Floor(Math.Log10(vStep)));
                var mantissa = Math.Round(vStep / exponent);
                // get step to be 1, 2 or 5 powers of 10
                if (mantissa > 1 && mantissa < 2) mantissa = 2;
                else if (mantissa > 2 && mantissa < 5) mantissa = 5;
                else if (mantissa > 5) mantissa = 10;
                vStep = mantissa * exponent;
                nTicks = (MaximumValue - firstTick) / vStep;
            }
            else
            {
                nTicks = LabelNumber;
                vStep = range / (LabelNumber - 1);
            }

            var pen = new Pen(Color);
            var brush = new SolidBrush(Color);
            var prevClip = g.ClipBounds;
            g.SetClip(Bounds);
            for (int i = 0; i < nTicks + 1; i++)
            {
                var v = firstTick + i * vStep;
                if (v < MinimumValue - Helper.TOLERANCE || v > MaximumValue + Helper.TOLERANCE) continue;
                var p = (int) ((v - MinimumValue) / scale + InnerBounds.Y);
                g.DrawLine(pen, Bounds.Left, p, Bounds.Left + _lineLength, p);
                g.DrawString(v.ToString(NumericFormat), Font,
                    brush, Bounds.Left + _lineLength, p - textHeight / 2);
            }

            g.SetClip(prevClip);
        }

        #endregion Overrides

        #region Private API

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

    }
}
