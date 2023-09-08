using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.W9
{
    /// <summary>
    /// The definition for US W9, API version 1.
    /// </summary>
    [Endpoint("us_w9", "1")]
    public sealed class W9V1 : Inference<W9V1Page, W9V1Document>
    {
    }
}
