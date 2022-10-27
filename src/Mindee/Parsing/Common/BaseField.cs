using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        public List<List<double>> Polygon { get; set; }

        /// <summary>
        /// The index of the page where the current field was found.
        /// </summary>
        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }
}
