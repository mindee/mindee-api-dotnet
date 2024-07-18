using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Cropper
{
    /// <summary>
    /// Cropper API version 1 inference prediction.
    /// </summary>
    [Endpoint("cropper", "1")]
    public sealed class CropperV1 : Inference<CropperV1Page, CropperV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<CropperV1Page>))]
        public override Pages<CropperV1Page> Pages { get; set; }
    }
}
