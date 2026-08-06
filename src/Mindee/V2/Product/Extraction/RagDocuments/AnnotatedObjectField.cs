using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// An ObjectField with additional configuration for annotation.
    /// </summary>
    public class AnnotatedObjectField : AnnotatedBaseField
    {
        /// <summary>
        /// Sub-fields of the field.
        /// </summary>
        [JsonPropertyName("fields")]
        public AnnotatedFields Fields { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="guidelines"></param>
        public AnnotatedObjectField(bool selected, string guidelines) : base(selected, guidelines)
        { }
    }
}
