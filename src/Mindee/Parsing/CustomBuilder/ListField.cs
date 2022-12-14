using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.CustomBuilder
{
    /// <summary>
    /// Define the values list of a field.
    /// </summary>
    public sealed class ListField
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// <see cref="ListFieldValue"/>
        /// </summary>
        [JsonPropertyName("values")]
        public List<ListFieldValue> Values { get; set; }
    }
}
