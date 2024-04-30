

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Eu.DriverLicense
{
    /// <summary>
    /// Driver License API version 1 inference prediction.
    /// </summary>
    [Endpoint("eu_driver_license", "1")]
    public sealed class DriverLicenseV1 : Inference<DriverLicenseV1Page, DriverLicenseV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<DriverLicenseV1Page>))]
        public override Pages<DriverLicenseV1Page> Pages { get; set; }
    }
}
