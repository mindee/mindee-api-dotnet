using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Custom
{
    /// <summary>
    /// Define a value available in a field.
    /// </summary>
    public class ListFieldValue
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Define the coordinates of the zone in the page where the values has been found.
        /// </summary>
        [JsonPropertyName("polygon")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Polygon { get; set; }

        /// <summary>
        /// Content of the value.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="confidence"><see cref="Confidence"/></param>
        /// <param name="polygon"><see cref="Polygon"/></param>
        /// <param name="content"><see cref="Content"/></param>
        public ListFieldValue(string content, double confidence, Polygon polygon)
        {
            Confidence = confidence;
            Polygon = polygon;
            Content = content;
        }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            return Content ?? "";
        }
    }
}
