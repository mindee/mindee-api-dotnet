using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.UsMail
{
    /// <summary>
    /// US Mail API version 2 inference prediction.
    /// </summary>
    [Endpoint("us_mail", "2")]
    public sealed class UsMailV2 : Inference<UsMailV2Document, UsMailV2Document>
    {
        /// <summary>
        /// The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<UsMailV2Document>))]
        public override Pages<UsMailV2Document> Pages { get; set; }
    }
}
