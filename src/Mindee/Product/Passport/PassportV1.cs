using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Passport
{
    /// <summary>
    /// The definition for Passport, API version 1.
    /// </summary>
    [Endpoint("passport", "1")]
    public sealed class PassportV1 : Inference<PassportV1Document, PassportV1Document>
    {
    }
}
