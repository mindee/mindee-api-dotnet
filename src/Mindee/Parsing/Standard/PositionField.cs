using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
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
            if (Polygon != null && Polygon.Count != 0)
            {
                return $"Polygon with {Polygon.Count} points.";
            }
            if (BoundingBox != null && BoundingBox.Count != 0)
            {
                return $"Polygon with {BoundingBox.Count} points.";
            }
            if (Quadrangle != null && Quadrangle.Count != 0)
            {
                return $"Polygon with {Quadrangle.Count} points.";
            }
            if (Rectangle != null && Rectangle.Count != 0)
            {
                return $"Polygon with {Rectangle.Count} points.";
            }
            return "";
        }
    }
}
