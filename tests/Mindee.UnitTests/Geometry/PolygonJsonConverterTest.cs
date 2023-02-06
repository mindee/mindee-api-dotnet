using System.Text.Json;
using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.UnitTests.Geometry
{
    [Trait("Category", "Geometry - JSON converter")]
    public class PolygonJsonConverterTest
    {
        [Fact]
        public async Task Deserialize()
        {
            using (var file = new FileInfo("Resources/geometry/polygon.json").OpenRead())
            {
                var fake = await JsonSerializer.DeserializeAsync<Fake>(file);

                Assert.NotNull(fake?.Polygon);
                Assert.Equal(4, fake.Polygon.Count());
                Assert.Equal(0.238, fake.Polygon.First().X);
                Assert.Equal(0.161, fake.Polygon.Last().Y);
            }
        }

        public class Fake
        {
            [JsonPropertyName("polygon")]
            [JsonConverter(typeof(PolygonJsonConverter))]
            public Polygon Polygon { get; }

            public Fake(Polygon polygon)
            {
                Polygon = polygon;
            }
        }
    }
}
