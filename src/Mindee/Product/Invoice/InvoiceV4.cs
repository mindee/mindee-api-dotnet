

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// Invoice API version 4 inference prediction.
    /// </summary>
    [Endpoint("invoices", "4")]
    public sealed class InvoiceV4 : Inference<InvoiceV4Document, InvoiceV4Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<InvoiceV4Document>))]
        public override Pages<InvoiceV4Document> Pages { get; set; }
    }
}
