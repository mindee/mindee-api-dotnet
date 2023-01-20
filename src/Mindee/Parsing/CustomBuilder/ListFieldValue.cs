using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.CustomBuilder
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
        public List<List<double>> Polygon { get; set; }

        /// <summary>
        /// Content of the value.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// A prettier reprensentation.
        /// </summary>
        public override string ToString()
        {
            return Content ?? "";
        }
    }
}
