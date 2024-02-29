

using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Eu.DriverLicense
{
    /// <summary>
    /// The definition for Driver License, API version 1.
    /// </summary>
    [Endpoint("eu_driver_license", "1")]
    public sealed class DriverLicenseV1 : Inference<DriverLicenseV1Page, DriverLicenseV1Document>
    {
    }
}
