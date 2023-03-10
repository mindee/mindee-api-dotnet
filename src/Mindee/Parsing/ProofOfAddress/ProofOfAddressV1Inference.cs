using Mindee.Parsing.Common;

namespace Mindee.Parsing.ProofOfAddress
{
    /// <summary>
    /// The definition for proof_of_address v1.
    /// </summary>
    [Endpoint("proof_of_address", "1")]
    public class ProofOfAddressV1Inference : Inference<ProofOfAddressV1DocumentPrediction, ProofOfAddressV1DocumentPrediction>
    {
    }
}
