using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Resume
{
    /// <summary>
    /// The definition for Resume, API version 1.
    /// </summary>
    [Endpoint("resume", "1")]
    public sealed class ResumeV1 : Inference<ResumeV1Document, ResumeV1Document>
    {
    }
}
