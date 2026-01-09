using System.Text.Json.Serialization;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing.Custom
{
    /// <summary>
    ///     Define a classification field.
    /// </summary>
    public class ClassificationField : BaseField
    {
        /// <summary>
        ///     The content of the value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        ///     A prettier reprensentation.
        /// </summary>
        public override string ToString()
        {
            return Value ?? "";
        }
    }
}
