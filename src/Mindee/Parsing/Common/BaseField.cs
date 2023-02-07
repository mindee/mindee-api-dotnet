using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent basics of a field.
    /// </summary>
    public abstract class BaseField
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
        /// The index of the page where the current field was found.
        /// </summary>
        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="confidence"><see cref="Confidence"/></param>
        /// <param name="polygon"><see cref="Polygon"/></param>
        /// <param name="pageId"><see cref="PageId"/></param>
        protected BaseField(double confidence, Polygon polygon, int? pageId)
        {
            Confidence = confidence;
            Polygon = polygon;
            PageId = pageId;
        }

        /// <summary>
        ///
        /// </summary>
        protected BaseField()
        { }
    }
}
