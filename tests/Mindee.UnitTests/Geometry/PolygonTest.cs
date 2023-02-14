using Mindee.Geometry;

namespace Mindee.UnitTests.Geometry
{
    [Trait("Category", "Geometry - Polygon")]
    public class PolygonTest
    {
        [Fact]
        public void Get_The_Centroid()
        {
            var actualCentroid = FakesPolygon.GetPolygonAsRectangle().GetCentroid();
            var expectedCentroid = new Point(0.149, 0.538);

            Assert.Equal(expectedCentroid.X, actualCentroid.X);
            Assert.Equal(expectedCentroid.Y, actualCentroid.Y);
        }
    }
}
