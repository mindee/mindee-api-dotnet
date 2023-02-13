using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// The definition for idcard_fr v1.
    /// </summary>
    [Endpoint("idcard_fr", "1")]
    public class IdCardV1Inference : Inference<IdCardV1PagePrediction, IdCardV1DocumentPrediction>
    {
    }
}
