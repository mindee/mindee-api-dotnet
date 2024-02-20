using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.InternationalId
{
    /// <summary>
    /// The definition for International ID, API version 2.
    /// </summary>
    [Endpoint("international_id", "2")]
    public sealed class InternationalIdV2 : Inference<InternationalIdV2Document, InternationalIdV2Document>
    {
    }
}
