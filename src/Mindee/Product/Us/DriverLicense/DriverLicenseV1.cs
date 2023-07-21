using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.DriverLicense
{
    /// <summary>
    /// The definition for Driver License, API version 1.
    /// </summary>
    [Endpoint("us_driver_license", "1")]
    public sealed class DriverLicenseV1 : Inference<DriverLicenseV1Page, DriverLicenseV1Document>
    {
    }
}
