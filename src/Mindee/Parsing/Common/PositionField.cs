using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public sealed class PositionField
    {
        /// <summary>
        /// Straight rectangle.
        /// </summary>
        [JsonPropertyName("bounding_box")]
        public List<List<double>> BoundingBox { get; set; }

        /// <summary>
        /// Free polygon with up to 30 vertices.
        /// </summary>
        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        /// <summary>
        /// Free polygon with 4 vertices.
        /// </summary>
        [JsonPropertyName("quadrangle")]
        public List<List<double>> Quadrangle { get; set; }

        /// <summary>
        /// Rectangle that may be oriented (can go beyond the canvas).
        /// </summary>
        [JsonPropertyName("rectangle")]
        public List<List<double>> Rectangle { get; set; }
    }
}
