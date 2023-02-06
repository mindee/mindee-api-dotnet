using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindee.Geometry
{
    /// <summary>
    /// A set of points.
    /// </summary>
    public class Polygon : List<Point>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="coordinates">List of points of coordinates on X and Y.</param>
        public Polygon(List<List<double>> coordinates)
        {
            foreach (var point in coordinates)
            {
                if (point.Count != 2)
                {
                    throw new InvalidOperationException("A point must have 2 coordinates.");
                }
                Add(new Point(point.First(), point.Last()));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="coordinates"><see cref="List{Point}"/></param>
        public Polygon(IEnumerable<Point> coordinates)
        {
            AddRange(coordinates);
        }

        /// <summary>
        /// Get the centroid of the current polygon.
        /// </summary>
        public Point GetCentroid()
        {
            int verticesSum = this.Count();

            double xSum = this.Sum(c => c.X);
            double ySum = this.Sum(c => c.Y);

            return new Point(xSum / verticesSum, ySum / verticesSum);
        }

        /// <summary>
        /// Get the Y min coordinate.
        /// </summary>
        public double GetMinYCoordinate()
        {
            return this.Min(p => p.Y);
        }

        /// <summary>
        /// Get the Y max coordinate.
        /// </summary>
        public double GetMaxYCoordinate()
        {
            return this.Max(p => p.Y);
        }

        /// <summary>
        /// Get the X min coordinate.
        /// </summary>
        public double GetMinXCoordinate()
        {
            return this.Min(p => p.X);
        }

        /// <summary>
        /// Get the X max coordinate.
        /// </summary>
        public double GetMaxXCoordinate()
        {
            return this.Max(p => p.X);
        }
    }
}
