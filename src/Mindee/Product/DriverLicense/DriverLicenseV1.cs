using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.DriverLicense
{
    /// <summary>
    ///     Driver License API version 1 inference prediction.
    /// </summary>
    [Endpoint("driver_license", "1")]
    public sealed class DriverLicenseV1 : Inference<DriverLicenseV1Document, DriverLicenseV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<DriverLicenseV1Document>))]
        public override Pages<DriverLicenseV1Document> Pages { get; set; }
    }
}
