using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.MultiReceiptsDetector
{
    /// <summary>
    ///     Multi Receipts Detector API version 1 inference prediction.
    /// </summary>
    [Endpoint("multi_receipts_detector", "1")]
    public sealed class
        MultiReceiptsDetectorV1 : Inference<MultiReceiptsDetectorV1Document, MultiReceiptsDetectorV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<MultiReceiptsDetectorV1Document>))]
        public override Pages<MultiReceiptsDetectorV1Document> Pages { get; set; }
    }
}
