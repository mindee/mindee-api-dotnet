using System.Text.Json;
using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.UnitTests.Geometry
{
    [Trait("Category", "Geometry - JSON converter")]
    public sealed class PolygonJsonConverterTest
    {
        [Fact]
        public async Task Deserialize()
        {
            using var file = new FileInfo("Resources/geometry/polygon.json").OpenRead();
            var fake = await JsonSerializer.DeserializeAsync<Fake>(file);

            Assert.NotNull(fake?.Polygon);
            Assert.Equal(4, fake.Polygon.Count);
            Assert.Equal(0.238, fake.Polygon[0].X);
            Assert.Equal(0.161, fake.Polygon[fake.Polygon.Count - 1].Y);
        }

        public class Fake(Polygon polygon)
        {
            [JsonPropertyName("polygon")]
            [JsonConverter(typeof(PolygonJsonConverter))]
            public Polygon Polygon { get; } = polygon;
        }
    }
}
