using Mindee.Parsing.Common;

namespace Mindee.Parsing.Eu.LicensePlates
{
    /// <summary>
    /// The license plates v1 definition.
    /// </summary>
    [Endpoint("license_plates", "1")]
    public class LicensePlatesV1Inference : Inference<LicensePlatesV1DocumentPrediction, LicensePlatesV1DocumentPrediction>
    {
    }
}
