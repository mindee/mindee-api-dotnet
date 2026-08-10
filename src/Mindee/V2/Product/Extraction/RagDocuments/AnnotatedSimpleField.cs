using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// A SimpleField with additional configuration for annotation.
    /// </summary>
    [JsonConverter(typeof(AnnotatedSimpleFieldJsonConverter))]
    public class AnnotatedSimpleField : AnnotatedBaseField
    {
        /// <summary>
        ///     Field value, one of: string, bool, int, double, null.
        /// </summary>
        [JsonPropertyName("value")]
        public dynamic Value { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="selected"></param>
        /// <param name="guidelines"></param>
        public AnnotatedSimpleField(object value, bool selected, string guidelines) : base(selected, guidelines)
        {
            Value = value;
        }
    }
}
