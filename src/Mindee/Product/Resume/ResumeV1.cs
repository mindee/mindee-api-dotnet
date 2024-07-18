using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Resume
{
    /// <summary>
    /// Resume API version 1 inference prediction.
    /// </summary>
    [Endpoint("resume", "1")]
    public sealed class ResumeV1 : Inference<ResumeV1Document, ResumeV1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<ResumeV1Document>))]
        public override Pages<ResumeV1Document> Pages { get; set; }
    }
}
