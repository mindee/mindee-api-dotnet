using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Fr.IdCard
{
    /// <summary>
    /// The definition for idcard_fr v1.
    /// </summary>
    [Endpoint("idcard_fr", "1")]
    public sealed class IdCardV1 : Inference<IdCardV1Page, IdCardV1Document>
    {
    }
}
