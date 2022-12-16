using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        public List<List<double>> BoundingBox { get; set; } = new List<List<double>>();

        /// <summary>
        /// Free polygon with up to 30 vertices.
        /// </summary>
        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; } = new List<List<double>>();

        /// <summary>
        /// Free polygon with 4 vertices.
        /// </summary>
        [JsonPropertyName("quadrangle")]
        public List<List<double>> Quadrangle { get; set; } = new List<List<double>>();

        /// <summary>
        /// Rectangle that may be oriented (can go beyond the canvas).
        /// </summary>
        [JsonPropertyName("rectangle")]
        public List<List<double>> Rectangle { get; set; } = new List<List<double>>();

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            return $"Polygon with {Polygon.Count} points.";
        }
    }
}
