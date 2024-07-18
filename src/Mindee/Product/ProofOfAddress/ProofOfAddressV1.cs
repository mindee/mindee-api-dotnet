using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.ProofOfAddress
{
    /// <summary>
    /// Proof of Address API version 1 inference prediction.
    /// </summary>
    [Endpoint("proof_of_address", "1")]
    public sealed class ProofOfAddressV1 : Inference<ProofOfAddressV1Document, ProofOfAddressV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<ProofOfAddressV1Document>))]
        public override Pages<ProofOfAddressV1Document> Pages { get; set; }
    }
}
