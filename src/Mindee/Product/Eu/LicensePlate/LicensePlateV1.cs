using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Eu.LicensePlate
{
    /// <summary>
    /// The definition for license_plates v1.
    /// </summary>
    [Endpoint("license_plates", "1")]
    public sealed class LicensePlateV1 : Inference<LicensePlateV1Document, LicensePlateV1Document>
    {
    }
}
