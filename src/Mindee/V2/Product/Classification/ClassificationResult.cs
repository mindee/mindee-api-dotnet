using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Classification
{
    /// <summary>
    /// Result of the document classifier inference.
    /// </summary>
    public class ClassificationResult
    {
        /// <summary>
        /// Classification of document type from the source file.
        /// </summary>
        [JsonPropertyName("classification")]
        public ClassificationClassifier Classification { get; set; }

        /// <summary>
        ///     A prettier representation.
        /// </summary>
        public override string ToString()
        {
            return $"Classification\n==============\n{Classification}";
        }
    }
}
