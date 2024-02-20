using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.InternationalId
{
    /// <summary>
    /// The definition for International ID, API version 1.
    /// </summary>
    [Endpoint("international_id", "1")]
    public sealed class InternationalIdV1 : Inference<InternationalIdV1Document, InternationalIdV1Document>
    {
    }
}
