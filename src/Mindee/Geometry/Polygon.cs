using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;

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
                    throw new InvalidOperationException("A point must have 2 coordinates.");

                Add(new Point(point.First(), point.Last()));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="coordinates"><see cref="IEnumerable{Point}"/></param>
        public Polygon(IEnumerable<Point> coordinates)
        {
            AddRange(coordinates);
        }

        /// <summary>
        /// Get the centroid of the current polygon.
        /// </summary>
        public Point GetCentroid()
        {
            int verticesCount = this.Count();

            double xSum = this.Sum(c => c.X);
            double ySum = this.Sum(c => c.Y);

            return new Point(xSum / verticesCount, ySum / verticesCount);
        }

        /// <summary>
        /// Get the Y min coordinate.
        /// </summary>
        public double GetMinY()
        {
            return this.Min(point => point.Y);
        }

        /// <summary>
        /// Get the Y max coordinate.
        /// </summary>
        public double GetMaxY()
        {
            return this.Max(point => point.Y);
        }

        /// <summary>
        /// Get the X min coordinate.
        /// </summary>
        public double GetMinX()
        {
            return this.Min(point => point.X);
        }

        /// <summary>
        /// Get the X max coordinate.
        /// </summary>
        public double GetMaxX()
        {
            return this.Max(point => point.X);
        }

        /// <summary>
        /// Determine if a Point is within the polygon's Y coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointInY(Point point)
        {
            return Utils.IsPointInY(point: point, minY: GetMinY(), maxY: GetMaxY());
        }

        /// <summary>
        /// The default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count > 0)
                return $"Polygon with {this.Count} points.";
            return "";
        }
    }
}
