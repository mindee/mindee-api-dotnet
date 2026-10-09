using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// A RAG annotation enriched with field-level configuration.
    /// </summary>
    public class RagAnnotation
    {
        /// <summary>
        /// Annotated fields.
        /// </summary>
        [JsonPropertyName("fields")]
        public AnnotatedFields Fields { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public RagAnnotation()
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="fields"><see cref="AnnotatedFields"/></param>
        public RagAnnotation(AnnotatedFields fields)
        {
            this.Fields = fields;
        }
    }
}
