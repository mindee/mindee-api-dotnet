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
