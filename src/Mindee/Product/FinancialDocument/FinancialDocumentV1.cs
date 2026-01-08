using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.FinancialDocument
{
    /// <summary>
    ///     Financial Document API version 1 inference prediction.
    /// </summary>
    [Endpoint("financial_document", "1")]
    public sealed class FinancialDocumentV1 : Inference<FinancialDocumentV1Document, FinancialDocumentV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<FinancialDocumentV1Document>))]
        public override Pages<FinancialDocumentV1Document> Pages { get; set; }
    }
}
