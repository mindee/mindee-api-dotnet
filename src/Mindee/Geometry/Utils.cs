using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindee.Geometry
{
    /// <summary>
    ///     Various geometry functions.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        ///     Determine if a Point is within two Y coordinates.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public static bool IsPointInY(Point point, double minY, double maxY)
        {
            return minY <= point.Y && point.Y <= maxY;
        }

        /// <summary>
        ///     Compare two polygons based on their Y coordinates.
        ///     Useful for sorting lists.
        /// </summary>
        public static int CompareOnY(Polygon polygon1, Polygon polygon2)
        {
            var sort = polygon1.GetMinY() - polygon2.GetMinY();
            if (sort == 0)
            {
                return 0;
            }

            return sort < 0 ? -1 : 1;
        }

        /// <summary>
        ///     Compare two polygons based on their X coordinates.
        ///     Useful for sorting lists.
        /// </summary>
        public static int CompareOnX(Polygon polygon1, Polygon polygon2)
        {
            var sort = polygon1.GetMinX() - polygon2.GetMinX();
            if (sort == 0)
            {
                return 0;
            }

            return sort < 0 ? -1 : 1;
        }

        /// <summary>
        ///     Create from polygon.
        /// </summary>
        /// <param name="polygon"></param>
        public static Bbox BboxFromPolygon(Polygon polygon)
        {
            if (polygon == null)
            {
                return null;
            }

            return new Bbox(
                polygon.GetMinX(),
                polygon.GetMaxX(),
                polygon.GetMinY(),
                polygon.GetMaxY()
            );
        }

        /// <summary>
        ///     Create from a list of polygons.
        /// </summary>
        /// <param name="polygons">
        ///     <see cref="Polygon" />
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public static Polygon BoundingBoxFromPolygons(IEnumerable<Polygon> polygons)
        {
            if (!polygons.Any())
            {
                throw new ArgumentException(nameof(polygons));
            }

            var minX = polygons.Min(p => p.GetMinX());
            var minY = polygons.Min(p => p.GetMinY());
            var maxX = polygons.Max(p => p.GetMaxX());
            var maxY = polygons.Max(p => p.GetMaxY());

            return new Polygon(
                new List<Point> { new(minX, minY), new(maxX, minY), new(maxX, maxY), new(minX, maxY) });
        }

        /// <summary>
        ///     Create from polygon.
        /// </summary>
        /// <param name="polygon">
        ///     <see cref="Polygon" />
        /// </param>
        public static Polygon BoundingBoxFromPolygon(Polygon polygon)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            var minX = polygon.GetMinX();
            var minY = polygon.GetMinY();
            var maxX = polygon.GetMaxX();
            var maxY = polygon.GetMaxY();

            return new Polygon(
                new List<Point> { new(minX, minY), new(maxX, minY), new(maxX, maxY), new(minX, maxY) });
        }
    }
}
