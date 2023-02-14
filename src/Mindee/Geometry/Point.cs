using System.Collections.Generic;
using System.Linq;

namespace Mindee.Geometry
{
    /// <summary>
    /// Group of 2 coordinates.
    /// </summary>
    public class Point : List<double>
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X => this.First();

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y => this.Last();

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"><see cref="X"/></param>
        /// <param name="y"><see cref="Y"/></param>
        public Point(double x, double y)
        {
            Add(x);
            Add(y);
        }
    }
}
