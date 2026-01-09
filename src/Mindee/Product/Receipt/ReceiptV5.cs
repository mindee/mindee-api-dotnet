using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Receipt
{
    /// <summary>
    /// Receipt API version 5 inference prediction.
    /// </summary>
    [Endpoint("expense_receipts", "5")]
    public sealed class ReceiptV5 : Inference<ReceiptV5Document, ReceiptV5Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<ReceiptV5Document>))]
        public override Pages<ReceiptV5Document> Pages { get; set; }
    }
}
