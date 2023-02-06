using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// List of all detected cropped elements in the image.
    /// </summary>
    public sealed class PositionField
    {
        /// <summary>
        /// Straight rectangle.
        /// </summary>
        [JsonPropertyName("bounding_box")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon BoundingBox { get; set; }

        /// <summary>
        /// Free polygon with up to 30 vertices.
        /// </summary>
        [JsonPropertyName("polygon")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Polygon { get; set; }

        /// <summary>
        /// Free polygon with 4 vertices.
        /// </summary>
        [JsonPropertyName("quadrangle")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Quadrangle { get; set; }

        /// <summary>
        /// Rectangle that may be oriented (can go beyond the canvas).
        /// </summary>
        [JsonPropertyName("rectangle")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Rectangle { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            return $"Polygon with {Polygon.Count} points.";
        }
    }
}
