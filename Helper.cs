using System;

namespace ColorLegend
{
    /// <summary>
    /// Helper class.
    /// </summary>
    internal class Helper
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

    }
}
