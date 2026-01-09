using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.DeliveryNote
{
    /// <summary>
    /// Delivery note API version 1 inference prediction.
    /// </summary>
    [Endpoint("delivery_notes", "1")]
    public sealed class DeliveryNoteV1 : Inference<DeliveryNoteV1Document, DeliveryNoteV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<DeliveryNoteV1Document>))]
        public override Pages<DeliveryNoteV1Document> Pages { get; set; }
    }
}
