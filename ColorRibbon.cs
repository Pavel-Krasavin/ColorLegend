using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorLegend
{
    internal class ColorRibbon : Rectangular
    {

        #region Private Data Members

        private readonly Color[] DefaultColors =
           new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };

        private Color[] _colors;
        private Brush _brush;

        // for outlier
        protected int _topBottomPadding = 10;
        private bool _invertDirection;

        #endregion Private Data Members

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="control">Parent control.</param>
        public ColorRibbon(Control control) : base(control)
        {
            _colors = DefaultColors;
        }

        #endregion Constructors

        #region Public API

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
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets an array of colors to use for gradient.
        /// </summary>
        public Color[] Colors
        {
            get => _colors;
            set
            {
                if (value == null || value.Length == 0) value = DefaultColors;
                _colors = value;
                CreateBrush();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets flag indicating whether minimum values should be on top.
        /// </summary>
        public virtual bool InvertDirection
        {
            get => _invertDirection;
            set
            {
                if (value != _invertDirection)
                {
                    _invertDirection = value;
                    var linBrush = _brush as LinearGradientBrush;
                    if (linBrush != null)
                    {
                        var brush = new LinearGradientBrush(InnerBounds, Color.Black, Color.Black, _invertDirection ? 270 : 90);
                        brush.InterpolationColors = linBrush.InterpolationColors;
                        _brush = brush;
                    }
                    Invalidate();
                }
            }
        }

        #endregion Public API

        #region Overrides

        /// <inheritdoc/>
        public override Rectangle Bounds
        {
            get => base.Bounds;
            set
            {
                if (value != Bounds)
                {
                    base.Bounds = value;
                    CreateBrush();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Draws the ColorRibbon.
        /// </summary>
        public override void Draw(Graphics g)
        {
            if (_brush != null && !InnerBounds.IsEmpty)
            {
                g.FillRectangle(_brush, InnerBounds);
            }
        }

        #endregion Overrides

        #region Protected API

        /// <summary>
        /// Gets bounds without <see cref="TopBottomPadding"/>.
        /// </summary>
        protected Rectangle InnerBounds => new Rectangle(Bounds.X, Bounds.Y + _topBottomPadding,
                    Bounds.Width, Bounds.Height - 2 * _topBottomPadding);

        #endregion Protected API

        #region Private API

        /// <summary>
        /// Calculates a <see cref="Brush"/> to use for drawing.
        /// </summary>
        private void CreateBrush()
        {
            if (Colors.Length == 1)
            {
                _brush = new SolidBrush(Colors[0]);
                return;
            }
            var colorBlend = new ColorBlend();
            var positions = new float[_colors.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = i / (float)(positions.Length - 1);
            }
            colorBlend.Positions = positions;
            colorBlend.Colors = _colors;


            LinearGradientBrush linBrush = null;
            if (InnerBounds.Height > 0 && InnerBounds.Width > 0)
            {
                linBrush = new LinearGradientBrush(InnerBounds, Color.Black, Color.Black, _invertDirection ? 270 : 90);
                linBrush.InterpolationColors = colorBlend;
            }
            _brush = linBrush;
        }

        #endregion Private API

    }
}
