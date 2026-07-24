using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// Base class for annotated fields.
    /// </summary>
    public class BaseRagAnnotatedField
    {
        /// <summary>
        /// When true, use the RAG information for the final result. When false, use the Data Schema information.
        /// </summary>
        [JsonPropertyName("selected")]
        public bool Selected { get; set; }

        /// <summary>
        /// Guidelines or instructions for processing this field.
        /// </summary>
        [JsonPropertyName("guidelines")]
        public string Guidelines { get; set; }
    }
}
