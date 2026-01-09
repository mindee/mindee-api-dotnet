using Mindee.Parsing.Standard;
using Mindee.UnitTests.Geometry;

namespace Mindee.UnitTests.V1.Parsing.Standard
{
    [Trait("Category", "Standard DateField")]
    public class DateFieldTest
    {
        [Fact]
        public void Constructor_Should_Succeed()
        {
            var polygon = FakesPolygon.GetPolygonAsRectangle();
            var field = new DateField("1980-05-17", 1.0, polygon, 0, true);
            Assert.Equal("1980-05-17", field.Value);
            Assert.Equal("1980-05-17", field.DateObject?.ToString("yyyy-MM-dd"));
            Assert.True(field.IsComputed);
        }
    }
}
