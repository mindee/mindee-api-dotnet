using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.BarcodeReader
{
    /// <summary>
    ///     Barcode Reader API version 1 inference prediction.
    /// </summary>
    [Endpoint("barcode_reader", "1")]
    public sealed class BarcodeReaderV1 : Inference<BarcodeReaderV1Document, BarcodeReaderV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<BarcodeReaderV1Document>))]
        public override Pages<BarcodeReaderV1Document> Pages { get; set; }
    }
}
