using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// The ID card fr v1 definition.
    /// </summary>
    [Endpoint("idcard_fr", "1")]
    public class IdCardV1Inference : Inference<IdCardV1DocumentPrediction, IdCardV1DocumentPrediction>
    {
    }
}
