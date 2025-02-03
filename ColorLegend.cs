using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PGMUI.Data.Linq.Helpers;
using PGMUI.Emf;

namespace ColorLegend
{
    public partial class ColorLegend : UserControl
    {

        #region Private Data Members

   //     const int WS_EX_TRANSPARENT = 0x20;

        // size of a size grip
        private const int SizeGripGauge = 16;
        
        private int _innerMargin = 3;
        private Rectangle _rect1 = new Rectangle(), _rect2 = new Rectangle();
        private int _maxHistLength = 500, _minHistLength = 20, _maxHistWidth = 100, _minHistWidth = 4;
        
        private Labels _labels;
        private ColorRibbon _colorRibbon;
        private Histogram _histogram;
       
        private bool _showHistogram = true, _showLabels = true, _showTitle = true, _showExTitle = true, _showFrame = false;
        private bool _mouseTransparent = false;
        private Color _frameColor = Color.Black;
        private int _frameWidth = 1;
        //private readonly int[] _mouseEvents = new int[] { 
        //    0x0084, 0x0200, 0x0203, 0x0201, 0x0202, 0x02A1, 0x020E, 0x020A, 0x0206, 0x0204, 0x0205, 0x020D, 0x020B, 0x020C
        //  };

        // indicates that layout is needed
        private bool _changed;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Creates an instance.
        /// </summary>
        public ColorLegend()
        {            
            DoubleBuffered = true;

            _labels = new Labels(this);
            _colorRibbon = new ColorRibbon(this);
            _histogram = new Histogram(this);
            _labels.TopBottomPadding = _colorRibbon.TopBottomPadding = _histogram.TopBottomPadding = 20;

            InitializeComponent();
            InitMovement();
         //   this.ResizeRedraw = true;

            RepositionElements();
        }

        #endregion Constructors

        #region Public API

        /// <summary>
        /// Gets or sets data to render.
        /// </summary>
        public double[] Data
        {
            get => _histogram.Data;
            set
            {
                _histogram.Data = value;
                _labels.MaximumValue = double.IsNaN(_histogram.MaximumVisibleValue) ?
                     _histogram.MaximumValue : _histogram.MaximumVisibleValue;
                _labels.MinimumValue = double.IsNaN(_histogram.MinimumVisibleValue) ?
                    _histogram.MinimumValue : _histogram.MinimumVisibleValue;
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Histogram"/>.
        /// </summary>
        public bool ShowHistogram
        {
            get => _showHistogram;
            set {
                if (value != _showHistogram)
                {
                    _showHistogram = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show the histogram outliers.
        /// </summary>
        public bool ShowOutliers
        {
            get => _histogram.ShowOutliers;
            set
            {
                _histogram.ShowOutliers = value;
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Title"/>.
        /// </summary>
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                if (value != _showTitle)
                {
                    _showTitle = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="ExTitle"/>.
        /// </summary>
        public bool ShowExtendedTitle
        {
            get => _showExTitle;
            set
            {
                if (value != _showExTitle)
                {
                    _showExTitle = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show <see cref="Labels"/>.
        /// </summary>
        public bool ShowLabels
        {
            get => _showLabels;
            set
            {
                if (value != _showLabels)
                {
                    _showLabels = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether to show tghe outer frame.
        /// </summary>
        public bool ShowFrame
        {
            get => _showFrame;
            set
            {
                if (value != _showFrame)
                {
                    _showFrame = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the frame.
        /// </summary>
        public Color FrameColor
        {
            get => _frameColor;
            set
            {
                if (value != _frameColor)
                {
                    _frameColor =  value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a width of the frame.
        /// </summary>
        public int FrameWidth
        {
            get => _frameWidth;
            set
            {
                if (value != _frameWidth)
                {
                    _frameWidth = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the control's title string.
        /// </summary>
        public string Title
        {
            get => _titleLabel.Text;
            set
            {
                if (value != Title)
                {
                    _titleLabel.Text = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Font"/> for the title label.
        /// </summary>
        public Font TitleFont
        {
            get => _titleLabel.Font;
            set
            {
                if (value != TitleFont)
                {
                    _titleLabel.Font = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the title label.
        /// </summary>
        public Color TitleColor
        {
            get => _titleLabel.ForeColor;
            set
            {
                _titleLabel.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the control's extended title string.
        /// </summary>
        public string ExtendedTitle
        {
            get => _exTitleLabel.Text;
            set
            {
                if (value != ExtendedTitle)
                {
                    _exTitleLabel.Text = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Font"/> for the extended title label.
        /// </summary>
        public Font ExtendedTitleFont
        {
            get => _exTitleLabel.Font;
            set
            {
                if (value != ExtendedTitleFont)
                {
                    _exTitleLabel.Font = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the extended title label.
        /// </summary>
        public Color ExtendedTitleColor
        {
            get => _exTitleLabel.ForeColor;
            set
            {
                _exTitleLabel.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Font"/> for the axis.
        /// </summary>
        public Font LabelsFont
        {
            get => _labels.Font;
            set
            {
                if (value != _labels.Font)
                {
                    _labels.Font = value;
                    RepositionElements();
                }
            }
        }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> for the axis.
        /// </summary>
        public Color LabelsColor
        {
            get => _labels.Color;
            set
            {
                _labels.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets a numeric format for the axis.
        /// </summary>
        public string AxisNumericFormat
        {
            get => _labels.NumericFormat;
            set
            {
                _labels.NumericFormat = value;
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
        /// Gets or sets a minimum width of the child <see cref="Histogram"/>.
        /// </summary>
        public int MinimumHistogramWidth
        {
            get => _minHistWidth;
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
        /// Gets or sets a maximum width of the child <see cref="Histogram"/>.
        /// </summary>
        public int MaximumHistogramWidth
        {
            get => _maxHistWidth;
            set
            {
                if (value != _maxHistWidth)
                {
                    _maxHistWidth = Math.Max(_minHistWidth, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a minimum value to draw.
        /// </summary>
        public double MinimumVisibleValue
        {
            get => _histogram.MinimumVisibleValue;
            set
            {
                _histogram.MinimumVisibleValue = value;
                _labels.MinimumValue = _histogram.MinimumVisibleValue;
            }
        }

        /// <summary>
        /// Gets or sets a maximum value to draw.
        /// </summary>
        public double MaximumVisibleValue
        {
            get => _histogram.MaximumVisibleValue;
            set
            {
                _histogram.MaximumVisibleValue = value;
                _labels.MaximumValue = _histogram.MaximumVisibleValue;
            }
        }
        /// <summary>
        /// Gets data maximum value.
        /// </summary>
        public double MaximumValue => _histogram.MaximumValue;

        /// <summary>
        /// Gets data minimum value.
        /// </summary>
        public double MinimumValue => _histogram.MinimumValue;


        /// <summary>
        /// Gets or sets a number of labels in the axis. Negative number means adaptive calculation.
        /// </summary>
        public int LabelNumber
        {
            get => _labels.LabelNumber;
            set => _labels.LabelNumber = value;
        }

        /// <summary>
        /// Gets or sets flag indicating whether this <see cref="ColorLegend"/> passes mouse events to the parent control.
        /// </summary>
        public bool MouseTransparent
        {
            get => _mouseTransparent;
            set {
                if (value != _mouseTransparent)
                {
                    _mouseTransparent = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether minimum values should be on top.
        /// </summary>
        public virtual bool InvertDirection
        {
            get => _histogram.InvertDirection;
            set
            {
                _histogram.InvertDirection = value;
                _colorRibbon.InvertDirection = value;
                _labels.InvertDirection = value;
            }
        }

        #endregion Public API

        #region Overrides

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;

        //        return cp;
        //    }
        //}

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetClip(new Rectangle(0, 0, Width, Height));
            _colorRibbon.Draw(e.Graphics);
            if (ShowHistogram)
            {
                _histogram.Draw(e.Graphics);
            }
            if (ShowLabels)
            {
                _labels.Draw(e.Graphics);
            }
            if (ShowFrame)
            {
                DrawFrame(e.Graphics);
            }
         
            if (!MouseTransparent)
            {
                // draw a size grip to visualize resizing possibility
                var rc = new Rectangle(this.ClientSize.Width - SizeGripGauge, this.ClientSize.Height - SizeGripGauge, SizeGripGauge, SizeGripGauge);
                ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            }
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
   //         base.WndProc(ref m);

            const int WM_NCHITTEST = 0x0084;
            //const int WM_MOUSEMOVE       = 0x0200;
            //const int WM_LBUTTONDBLCLK   = 0x0203;
            //const int WM_LBUTTONDOWN     = 0x0201;
            //const int WM_LBUTTONUP       = 0x0202;
            //const int WM_MOUSEHOVER      = 0x02A1;
            //const int WM_MOUSEHWHEEL     = 0x020E;
            //const int WM_MOUSEWHEEL      = 0x020A;
            //const int WM_RBUTTONDBLCLK   = 0x0206;
            //const int WM_RBUTTONDOWN     = 0x0204;
            //const int WM_RBUTTONUP       = 0x0205;
            //const int WM_XBUTTONDBLCLK   = 0x020D;
            //const int WM_XBUTTONDOWN     = 0x020B;
            //const int WM_XBUTTONUP       = 0x020C;

            const int HTTRANSPARENT = (-1);

            // WM_NCHITTEST (message sent to determine
            // what part of the window corresponds to a particular screen coordinate)
            if (m.Msg == WM_NCHITTEST)
            {
                if (MouseTransparent)
                {
                    m.Result = new IntPtr(HTTRANSPARENT);
                    return;
                }
                else
                {
                    var pos = this.PointToClient(new Point(m.LParam.ToInt32()));
                    if (IsPointInRightBottomCorner(pos))
                    {
                        m.Result = new IntPtr(17);  // HT_BOTTOMRIGHT (bottom right corner)
                        return;
                    }
                }
            }
            base.WndProc(ref m);
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
                if (dragging && !MouseTransparent)
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
            if (!ShowTitle || string.IsNullOrEmpty(Title)) _titleLabel.Visible = false;
            else
            {
                _titleLabel.Visible = true;
                _titleLabel.Location = new Point(Padding.Left, Padding.Right);
                _titleLabel.MaximumSize = new Size(Width - Padding.Left - Padding.Right, Height / 3);
                y += _titleLabel.Bottom;
            }

            if (!ShowExtendedTitle || string.IsNullOrEmpty(ExtendedTitle)) _exTitleLabel.Visible = false;
            else
            {
                _exTitleLabel.Visible = true;
                _exTitleLabel.Location = new Point(Padding.Left, y + InnerMargin);
                _exTitleLabel.MaximumSize = new Size(Width - Padding.Left - Padding.Right, Height / 3);
                y += _exTitleLabel.Height + InnerMargin;
            }

            // from top: padding, title, margin, extended title, two margins, histogram, padding
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
            _labels.Bounds = axisRect;

            Invalidate();
        }


        private bool IsPointInRightBottomCorner(Point p)
        {
            return p.X >= this.ClientSize.Width - SizeGripGauge && p.Y >= this.ClientSize.Height - SizeGripGauge;
        }

        private void DrawFrame(Graphics g)
        {
            var pen = new Pen(FrameColor, FrameWidth);
            g.DrawRectangle(pen, new Rectangle(FrameWidth / 2, FrameWidth / 2, Width - FrameWidth, Height - FrameWidth));  
        }

        #endregion Private Methods

    }
}
