using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Eu.LicensePlate
{
    /// <summary>
    /// The definition for License Plate, API version 1.
    /// </summary>
    [Endpoint("license_plates", "1")]
    public sealed class LicensePlateV1 : Inference<LicensePlateV1Document, LicensePlateV1Document>
    {
    }
}