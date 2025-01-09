using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Define a classification field.
    /// </summary>
    public class ClassificationField
    {
        /// <summary>
        /// The content of the value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="value"></param>
        public ClassificationField(string value)
        {
            Value = value;
        }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            return SummaryHelper.FormatString(Value);
        }
    }
}
