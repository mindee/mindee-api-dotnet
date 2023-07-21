using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.ProofOfAddress
{
    /// <summary>
    /// The definition for Proof of Address, API version 1.
    /// </summary>
    [Endpoint("proof_of_address", "1")]
    public sealed class ProofOfAddressV1 : Inference<ProofOfAddressV1Document, ProofOfAddressV1Document>
    {
    }
}
