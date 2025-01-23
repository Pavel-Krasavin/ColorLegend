using System.Drawing;
using System.Windows.Forms;

namespace ColorHistogram
{
    /// <summary>
    /// Parent class for rectangular elements in the <see cref="ColorLegend"/>.
    /// </summary>
    internal class Rectangular
    {

        #region Private Data Members

        private Rectangle _bounds;
        private Control _parentControl;

        #endregion Private Data Members

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="control"></param>
        public Rectangular(Control control)
        {
            _parentControl = control;
        }


        #region Public API

        /// <summary>
        /// To be overriden to draw the element.
        /// </summary>
        public virtual void Draw(Graphics g) { }

        /// <summary>
        /// Gets or sets bounds of the element.
        /// </summary>
        public virtual Rectangle Bounds
        {
            get { return _bounds; }
            set
            {
                if (value != _bounds)
                {
                    _bounds = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Invalidates this selement in the parent control.
        /// </summary>
        public void Invalidate()
        {
            _parentControl.Invalidate(Bounds);
        }

        #endregion Public API

    }
}
