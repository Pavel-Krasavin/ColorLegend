using System.Drawing;
using System.Windows.Forms;

namespace ColorLegendExample
{
    /// <summary>
    /// Parent class for rectangular elements in the <see cref="ColorLegend"/>.
    /// </summary>
    internal abstract class Rectangular
    {

        #region Private Data Members

        private Rectangle _bounds;
        private Control _parentControl;
        private bool _layoutSuspended;

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
        /// Suspends the layout logic of the object.
        /// </summary>
        public void SuspendLayout() 
        {
            _layoutSuspended = true;
        }

        /// <summary>
        /// Resumesthe layout logic of the object and performs layout.
        /// </summary>
        public void ResumeLayout()
        {
            _layoutSuspended = false;
            PerfromLayout();
        }

        /// <summary>
        /// If layout is not suspended, performs layout and invalidates the object.
        /// </summary>
        protected void PerfromLayout()
        {
            if (!_layoutSuspended)
            {
                PositionElements(); 
            }
        }

        /// <summary>
        /// to be overriden to perform layout.
        /// </summary>
        protected abstract void PositionElements();

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
        /// Invalidates this element in the parent control.
        /// </summary>
        public void Invalidate()
        {
            _parentControl.Invalidate(Bounds);
        }

        #endregion Public API

    }
}
