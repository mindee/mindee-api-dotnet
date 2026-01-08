using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.IdCard
{
    /// <summary>
    ///     Carte Nationale d'Identité API version 2.0 page data.
    /// </summary>
    public sealed class IdCardV2Page : IdCardV2Document
    {
        /// <summary>
        ///     The sides of the document which are visible.
        /// </summary>
        [JsonPropertyName("document_side")]
        public ClassificationField DocumentSide { get; set; }

        /// <summary>
        ///     The document type or format.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Document Type: {DocumentType}\n");
            result.Append($":Document Sides: {DocumentSide}\n");
            result.Append(base.ToString());
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
