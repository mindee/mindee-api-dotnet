using Mindee.Geometry;

namespace Mindee.UnitTests.Geometry
{
    public static class FakesPolygon
    {
        public static Polygon GetPolygonAsRectangle()
        {
            return new Polygon(GetPointsAsRectangle());
        }

        public static Polygon GetPolygonWichIsNotRectangle()
        {
            return new Polygon(GetPointsWichIsNotRectangle());
        }

        private static List<Point> GetPointsAsRectangle()
        {
            return new List<Point>()
            {
                new Point(0.123, 0.53),
                new Point(0.175, 0.53),
                new Point(0.175, 0.546),
                new Point(0.123, 0.546)
            };
        }

        private static List<Point> GetPointsWichIsNotRectangle()
        {
            return new List<Point>()
            {
                new Point(0.205, 0.407),
                new Point(0.379, 0.407),
                new Point(0.381, 0.43),
                new Point(0.207, 0.43)
            };
        }
    }
}
