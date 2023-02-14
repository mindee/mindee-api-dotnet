using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.CustomBuilder
{
    /// <summary>
    /// Define the values list of a field.
    /// </summary>
    public class ListField
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

        /// <summary>
        ///
        /// </summary>
        public ListField()
        {
            Values = new List<ListFieldValue>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="confidence"><see cref="Confidence"/></param>
        /// <param name="values"><see cref="Values"/></param>
        public ListField(double confidence, List<ListFieldValue> values)
        {
            Confidence = confidence;
            Values = values;
        }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            return $"{string.Join(" ", Values)}";
        }
    }
}
