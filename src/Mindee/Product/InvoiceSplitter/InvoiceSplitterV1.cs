using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    ///     Invoice Splitter API version 1 inference prediction.
    /// </summary>
    [Endpoint("invoice_splitter", "1")]
    public sealed class InvoiceSplitterV1 : Inference<InvoiceSplitterV1Document, InvoiceSplitterV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<InvoiceSplitterV1Document>))]
        public override Pages<InvoiceSplitterV1Document> Pages { get; set; }
    }
}
