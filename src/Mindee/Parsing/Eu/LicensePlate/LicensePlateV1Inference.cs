using Mindee.Parsing.Common;

namespace Mindee.Parsing.Eu.LicensePlate
{
    /// <summary>
    /// The definition for license_plates v1.
    /// </summary>
    [Endpoint("license_plates", "1")]
    public class LicensePlateV1Inference : Inference<LicensePlateV1DocumentPrediction, LicensePlateV1DocumentPrediction>
    {
    }
}
