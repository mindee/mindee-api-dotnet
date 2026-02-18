using System.Text;
using System.Text.Json.Serialization;
using Mindee.V1.Parsing;
using Mindee.V1.Parsing.Standard;

namespace Mindee.V1.Product.Fr.IdCard
{
    /// <summary>
    /// Carte Nationale d'Identité API version 1.1 page data.
    /// </summary>
    public sealed class IdCardV1Page : IdCardV1Document
    {
        /// <summary>
        /// The side of the document which is visible.
        /// </summary>
        [JsonPropertyName("document_side")]
        public ClassificationField DocumentSide { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Document Side: {DocumentSide}\n");
            result.Append(base.ToString());
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
