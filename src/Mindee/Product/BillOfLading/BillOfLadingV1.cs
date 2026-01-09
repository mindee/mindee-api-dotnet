using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.BillOfLading
{
    /// <summary>
    /// Bill of Lading API version 1 inference prediction.
    /// </summary>
    [Endpoint("bill_of_lading", "1")]
    public sealed class BillOfLadingV1 : Inference<BillOfLadingV1Document, BillOfLadingV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<BillOfLadingV1Document>))]
        public override Pages<BillOfLadingV1Document> Pages { get; set; }
    }
}
