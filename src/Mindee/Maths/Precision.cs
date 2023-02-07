using System;

namespace Mindee.Maths
{
    /// <summary>
    ///
    /// </summary>
    public static class Precision
    {
        /// <summary>
        /// Compare two values with a tolerance.
        /// </summary>
        /// <example>
        /// - a = 1; b = 1.05, tolerance = 0.1. The method will return True.
        /// - a = 1; b = 1.05, tolerance = 0.04. The method will return False.
        /// </example>
        /// <param name="a">First value to compare.</param>
        /// <param name="b">Second value to compare.</param>
        /// <param name="tolerance">Tolerance value.</param>
        public static bool Equals(double a, double b, double tolerance)
        {
            if (Math.Abs(b - a) <= tolerance)
            {
                return true;
            }
            if (Math.Abs(a - b) <= tolerance)
            {
                return true;
            }

            return false;
        }
    }
}
