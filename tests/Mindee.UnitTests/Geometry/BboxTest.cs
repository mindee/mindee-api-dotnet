using Mindee.Geometry;

namespace Mindee.UnitTests.Geometry
{
    [Trait("Category", "Geometry - Bbox")]
    public class BboxTest
    {
        [Fact]
        public void CreateFromOnePolygon()
        {
            var polygon = new Polygon(new List<Point>()
            {
                new Point(0.081, 0.442),
                new Point(0.15, 0.442),
                new Point(0.15, 0.451),
                new Point(0.081, 0.451)
            });

            Bbox bbox = BboxCreation.Create(polygon);

            Assert.Equal(0.442, bbox.MinY);
            Assert.Equal(0.081, bbox.MinX);
            Assert.Equal(0.451, bbox.MaxY);
            Assert.Equal(0.15, bbox.MaxX);
        }

        [Fact]
        public void Merge2Bbox()
        {
            var firstBbox = new Bbox(0.081, 0.15, 0.442, 0.451);
            var secondBbox = new Bbox(0.157, 0.26, 0.442, 0.451);

            firstBbox.Merge(secondBbox);

            Assert.Equal(0.442, firstBbox.MinY);
            Assert.Equal(0.081, firstBbox.MinX);
            Assert.Equal(0.451, firstBbox.MaxY);
            Assert.Equal(0.26, firstBbox.MaxX);
        }
    }
}
