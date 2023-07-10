using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Passport
{
    /// <summary>
    /// The definition for passport v1.
    /// </summary>
    [Endpoint("passport", "1")]
    public class PassportV1 : Inference<PassportV1Document, PassportV1Document>
    {
    }
}
