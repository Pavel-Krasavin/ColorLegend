using System;
using System.Drawing;

namespace ColorLegendExample
{
    /// <summary>
    /// Helper class.
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Very small number.
        /// </summary>
        public const double TOLERANCE = 1e-10;

        /// <summary>
        /// Checks if two numbers are very close.
        /// </summary>
        public static bool AlmostEqual(double a, double b)
        {
            return Math.Abs(a - b) <= TOLERANCE;
        }

        /// <summary>
        /// Checks if the <see cref="Rectangle"/> area is greater than zero.
        /// </summary>
        public static bool IsZeroSize(this Rectangle r)
        {
            return r.Width <= 0 || r.Height <= 0;
        }

    }
}
