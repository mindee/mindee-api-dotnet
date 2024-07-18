using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Eu.LicensePlate
{
    /// <summary>
    /// License Plate API version 1 inference prediction.
    /// </summary>
    [Endpoint("license_plates", "1")]
    public sealed class LicensePlateV1 : Inference<LicensePlateV1Document, LicensePlateV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<LicensePlateV1Document>))]
        public override Pages<LicensePlateV1Document> Pages { get; set; }
    }
}
