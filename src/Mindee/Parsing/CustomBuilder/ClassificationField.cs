using System.Text.Json.Serialization;

namespace Mindee.Parsing.CustomBuilder
{
    /// <summary>
    /// Define a classification field.
    /// </summary>
    public class ClassificationField
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// The content of the value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// A prettier reprensentation.
        /// </summary>
        public override string ToString()
        {
            return Value ?? "";
        }
    }
}
