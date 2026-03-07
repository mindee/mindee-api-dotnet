using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Classification
{
    /// <summary>
    /// Classification of document type from the source file.
    /// </summary>
    public class ClassificationClassifier
    {
        /// <summary>
        /// The document type, as identified on given classification values.
        /// </summary>
        [JsonPropertyName("document_type")]
        public string DocumentType { get; set; }

        /// <summary>
        ///     A prettier representation of the feature values.
        /// </summary>
        public override string ToString()
        {
            return $"Document Type: {DocumentType}";
        }
    }
}
