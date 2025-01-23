using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorHistogram
{
    public partial class ColorLegend : UserControl
    {

        #region Private Data Members

        // size of a size grip
        private const int SizeGripGauge = 16;
        
        private int _innerMargin = 3;
        private Rectangle _rect1 = new Rectangle(), _rect2 = new Rectangle();
        private int _maxHistLength = 500, _minHistLength = 20, _maxHistWidth = 100, _minHistWidth = 4;
        
        private Axis _axis;
        private ColorRibbon _colorRibbon;
        private Histogram _histogram;
       
        private bool _showHistogram = true, _showAxis = true, _showTitle = true, _showUnits = true;

        #endregion Private Data Members

        #region Public API

        /// <summary>
        /// Gets or sets data to render.
        /// </summary>
        public double[] Data
        {
            get { return _histogram.Data; }
            set
            {
                _histogram.Data = value;
                _axis.MaximumValue = double.IsNaN(_histogram.MaximumVisibleValue) ?
                     _histogram.MaximumValue : _histogram.MaximumVisibleValue;
                _axis.MinimumValue = double.IsNaN(_histogram.MinimumVisibleValue) ?
                    _histogram.MinimumValue : _histogram.MinimumVisibleValue;
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Histogram"/>.
        /// </summary>
        public bool ShowHistogram
        {
            get { return _showHistogram; }
            set {
                if (value != _showHistogram)
                {
                    _showHistogram = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Title"/>.
        /// </summary>
        public bool ShowTitle
        {
            get { return _showTitle; }
            set
            {
                if (value != _showTitle)
                {
                    _showTitle = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Units"/>.
        /// </summary>
        public bool ShowUnits
        {
            get { return _showUnits; }
            set
            {
                if (value != _showUnits)
                {
                    _showUnits = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Axis"/>.
        /// </summary>
        public bool ShowAxis
        {
            get { return _showAxis; }
            set
            {
                if (value != _showAxis)
                {
                    _showAxis = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the control's title string.
        /// </summary>
        public string Title
        {
            get { return TitleLabel.Text; }
            set
            {
                if (value != Title)
                {
                    TitleLabel.Text = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Font"/> for the title label.
        /// </summary>
        public Font TitleFont
        {
            get { return TitleLabel.Font; }
            set
            {
                if (value != TitleFont)
                {
                    TitleLabel.Font = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the title label.
        /// </summary>
        public Color TitleColor
        {
            get { return TitleLabel.ForeColor; }
            set
            {
                TitleLabel.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the control's units string.
        /// </summary>
        public string Units
        {
            get { return UnitsLabel.Text; }
            set
            {
                if (value != Units)
                {
                    UnitsLabel.Text = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Font"/> for the units label.
        /// </summary>
        public Font UnitsFont
        {
            get { return UnitsLabel.Font; }
            set
            {
                if (value != UnitsFont)
                {
                    UnitsLabel.Font = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the units label.
        /// </summary>
        public Color UnitsColor
        {
            get { return UnitsLabel.ForeColor; }
            set
            {
                UnitsLabel.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Font"/> for the axis.
        /// </summary>
        public Font AxisFont
        {
            get { return _axis.Font; }
            set
            {
                if (value != _axis.Font)
                {
                    _axis.Font = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the axis.
        /// </summary>
        public Color AxisColor
        {
            get { return _axis.Color; }
            set
            {
                _axis.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets a numeric format for the axis.
        /// </summary>
        public string AxisNumericFormat
        {
            get { return _axis.NumericFormat; }
            set
            {
                _axis.NumericFormat = value;
            }
        }

        /// <summary>
        /// Gets or sets gap width between child elements.
        /// </summary>
        public int InnerMargin
        {
            get
            {
                return _innerMargin;
            }
            set
            {
                if (value != _innerMargin)
                {
                    _innerMargin = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets an array of colors to use for gradient.
        /// </summary>
        public Color[] Colors
        {
            get
            {
                return _colorRibbon.Colors;
            }
            set
            {
                _colorRibbon.Colors = value;
                _histogram.Colors = value;
            }
        }

        /// <summary>
        /// Gets or sets a maximum length of the child <see cref="Histogram"/>.
        /// </summary>
        public int MaximumHistogramLength
        {
            get { return _maxHistLength; }
            set
            {
                if (value != _maxHistLength)
                {
                    _maxHistLength = Math.Max(_minHistLength, value);
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a minimum length of the child <see cref="Histogram"/>.
        /// </summary>
        public int MinimumHistogramLength
        {
            get { return _minHistLength; }
            set
            {
                if (value != _minHistLength)
                {
                    _minHistLength = Math.Max(0, Math.Min(value, _maxHistLength));
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a maximum width of the child <see cref="Histogram"/>.
        /// </summary>
        public int MaximumHistogramWidth
        {
            get { return _maxHistWidth; }
            set
            {
                if (value != _maxHistWidth)
                {
                    _maxHistWidth = Math.Max(_minHistWidth, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a minimum width of the child <see cref="Histogram"/>.
        /// </summary>
        public int MinimumHistogramWidth
        {
            get { return _minHistWidth; }
            set
            {
                if (value != _minHistWidth)
                {
                    _minHistWidth = Math.Max(0, Math.Min(value, _maxHistWidth));
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a minimum value to draw.
        /// </summary>
        public double MinimumValue
        {
            get
            {
                return _histogram.MinimumVisibleValue;
            }
            set
            {
                _histogram.MinimumVisibleValue = value;
                _axis.MinimumValue = _histogram.MinimumVisibleValue;
            }
        }

        /// <summary>
        /// Gets or sets a maximum value to draw.
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return _histogram.MaximumVisibleValue;
            }
            set
            {
                _histogram.MaximumVisibleValue = value;
                _axis.MaximumValue = _histogram.MaximumVisibleValue;
            }
        }

        #endregion Public API

        #region Constructors

        /// <summary>
        /// Creates an instance.
        /// </summary>
        public ColorLegend()
        {
            DoubleBuffered = true;

            _axis = new Axis(this);
            _colorRibbon = new ColorRibbon(this);
            _histogram = new Histogram(this);
            _axis.TopBottomPadding = _colorRibbon.TopBottomPadding = _histogram.TopBottomPadding = 20;

            InitializeComponent();
            InitMovement();
            this.ResizeRedraw = true;
            
            RepositionElements();
        }

        #endregion Constructors

        #region Overrides

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetClip(new Rectangle(0, 0, Width, Height));
            _colorRibbon.Draw(e.Graphics);
            if (ShowHistogram)
                _histogram.Draw(e.Graphics);
            if (ShowAxis)
                _axis.Draw(e.Graphics);
         
            // draw a size grip to visualize resizing possibility
            var rc = new Rectangle(this.ClientSize.Width - SizeGripGauge, this.ClientSize.Height - SizeGripGauge, SizeGripGauge, SizeGripGauge);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }

        /// <inheritdoc/>
        protected override void OnPaddingChanged(EventArgs e)
        {
            RepositionElements();
            base.OnPaddingChanged(e);
        }

        /// <inheritdoc/>
        protected override void OnResize(EventArgs e)
        {
            RepositionElements();
            base.OnResize(e);
        }

        /// <inheritdoc/>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _histogram.Color = BackColor;
        }

        // overriden to make control resizable
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_NCHITTEST (message sent to determine
            // what part of the window corresponds to a particular screen coordinate)
            if (m.Msg == 0x84)
            {
                var pos = this.PointToClient(new Point(m.LParam.ToInt32()));
                if (pos.X >= this.ClientSize.Width - SizeGripGauge && pos.Y >= this.ClientSize.Height - SizeGripGauge)
                    m.Result = new IntPtr(17);  // HT_BOTTOMRIGHT (bottom right corner)
            }
        }

        #endregion Overrides

        #region Private Methods

        /// <summary>
        /// Makes this control movable by mouse.
        /// </summary>
        private void InitMovement()
        {
            var dragging = false;
            var dragStart = Point.Empty;

            this.MouseDown += (sender, e) =>
            {
                dragging = true;
                dragStart = new Point(e.X, e.Y);
            };

            this.MouseUp += (sender, e) =>
            {
                dragging = false;
            };

            this.MouseMove += (sender, e) =>
            {
                if (dragging)
                {
                    this.Left = Math.Max(0, e.X + this.Left - dragStart.X);
                    this.Top = Math.Max(0, e.Y + this.Top - dragStart.Y);
                }
            };
        }

        /// <summary>
        /// Calculates locations and sizes of the child elements.
        /// </summary>
        private void RepositionElements()
        {
            int y = 0;
            if (!ShowTitle || string.IsNullOrEmpty(Title)) TitleLabel.Visible = false;
            else
            {
                TitleLabel.Visible = true;
                TitleLabel.Location = new Point(Padding.Left, Padding.Right);
                TitleLabel.MaximumSize = new Size(Width - Padding.Left - Padding.Right, Height / 3);
                y += TitleLabel.Bottom;
            }

            if (!ShowUnits || string.IsNullOrEmpty(Units)) UnitsLabel.Visible = false;
            else
            {
                UnitsLabel.Visible = true;
                UnitsLabel.Location = new Point(Padding.Left, y + InnerMargin);
                UnitsLabel.MaximumSize = new Size(Width - Padding.Left - Padding.Right, Height / 3);
                y += UnitsLabel.Height + InnerMargin;
            }

            // from top: padding, title, margin, units, two margins, histogram, padding
            _rect1.Y = y;
            _rect1.Height = Height - y - Padding.Bottom;
            if (MaximumHistogramLength > 0)
                _rect1.Height = Math.Min(_rect1.Height, MaximumHistogramLength);
            if (MinimumHistogramLength > 0)
                _rect1.Height = Math.Max(_rect1.Height, MinimumHistogramLength);

            // from left: padding, color ribbon, axis, margin, histogram, padding
            _rect1.X = Padding.Left;
            _rect1.Width = (int) ((Width - Padding.Left - Padding.Right) * (ShowHistogram ? 0.4 : 0.8));
            if (MaximumHistogramWidth > 0)
                _rect1.Width = Math.Min(_rect1.Width, MaximumHistogramWidth);
            if (MinimumHistogramWidth > 0)
                _rect1.Width = Math.Max(_rect1.Width, MinimumHistogramWidth);

            _colorRibbon.Bounds = _rect1;

            _rect2.Y = _rect1.Y;
            _rect2.X = (int) (_rect1.X + 1.5 * _rect1.Width + InnerMargin);
            _rect2.Height = _rect1.Height;
            _rect2.Width = _rect1.Width;

            _histogram.Bounds = _rect2;

            var axisRect = new Rectangle();
            axisRect.Width = _rect1.Width / (ShowHistogram ? 2 : 1);
            axisRect.Height = _rect1.Height;
            axisRect.X = _rect1.Right;
            axisRect.Y = _rect1.Y;
            _axis.Bounds = axisRect;

            Invalidate();
        }

        #endregion Private Methods

    }
}
