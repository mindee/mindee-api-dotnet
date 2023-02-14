using Mindee.Parsing.Common;

namespace Mindee.Parsing.ProofOfAddress
{
    /// <summary>
    /// The proof of address v1 definition.
    /// </summary>
    [Endpoint("proof_of_address", "1")]
    public class ProofOfAddressV1Inference : Inference<ProofOfAddressV1DocumentPrediction, ProofOfAddressV1DocumentPrediction>
    {
    }
}
