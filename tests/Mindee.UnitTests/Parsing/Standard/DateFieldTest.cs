using Mindee.Geometry;
using Mindee.Parsing.Standard;
using Mindee.UnitTests.Geometry;


namespace Mindee.UnitTests.Parsing.Standard
{
    [Trait("Category", "Standard DateField")]
    public class DateFieldTest
    {
        [Fact]
        public void Constructor_Should_Succeed()
        {
            Polygon polygon = FakesPolygon.GetPolygonAsRectangle();
            DateField field = new DateField(value: "1980-05-17", confidence: 1.0, polygon: polygon, 0, true);
            Assert.Equal(expected: "1980-05-17", actual: field.Value);
            Assert.Equal(expected: "1980-05-17", actual: field.DateObject?.ToString("yyyy-MM-dd"));
            Assert.True(field.IsComputed);
        }
    }
}
