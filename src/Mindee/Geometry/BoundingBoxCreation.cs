using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindee.Geometry
{
    /// <summary>
    /// Bounding box builder.
    /// </summary>
    public static class BoundingBoxCreation
    {
        /// <summary>
        /// Create from polygon.
        /// </summary>
        /// <param name="polygon"><see cref="Polygon"/></param>
        public static Polygon Create(Polygon polygon)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            var minX = polygon.GetMinXCoordinate();
            var minY = polygon.GetMinYCoordinate();
            var maxX = polygon.GetMaxXCoordinate();
            var maxY = polygon.GetMaxYCoordinate();

            return new Polygon(
                new List<Point>() {
                    new Point(minX, minY),
                    new Point(maxX, minY),
                    new Point(maxX, maxY),
                    new Point(minX, maxY)
                    }
                );
        }

        /// <summary>
        /// Create from a list of polygons.
        /// </summary>
        /// <param name="polygons"><see cref="Polygon"/></param>
        /// <exception cref="ArgumentException"></exception>
        public static Polygon Create(IEnumerable<Polygon> polygons)
        {
            if (!polygons.Any())
            {
                throw new ArgumentException(nameof(polygons));
            }

            var minX = polygons.Min(p => p.GetMinXCoordinate());
            var minY = polygons.Min(p => p.GetMinYCoordinate());
            var maxX = polygons.Min(p => p.GetMaxXCoordinate());
            var maxY = polygons.Min(p => p.GetMaxYCoordinate());

            return new Polygon(
                new List<Point>() {
                    new Point(minX, minY),
                    new Point(maxX, minY),
                    new Point(maxX, maxY),
                    new Point(minX, maxY)
                    }
                );
        }
    }
}
