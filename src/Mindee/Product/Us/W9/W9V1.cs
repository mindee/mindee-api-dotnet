using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.W9
{
    /// <summary>
    /// W9 API version 1 inference prediction.
    /// </summary>
    [Endpoint("us_w9", "1")]
    public sealed class W9V1 : Inference<W9V1Page, W9V1Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<W9V1Page>))]
        public override Pages<W9V1Page> Pages { get; set; }
    }
}
