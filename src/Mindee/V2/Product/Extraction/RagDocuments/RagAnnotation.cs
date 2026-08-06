using System.Text.Json;
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
    }
}
