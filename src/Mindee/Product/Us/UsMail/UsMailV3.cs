using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Us.UsMail
{
    /// <summary>
    ///     US Mail API version 3 inference prediction.
    /// </summary>
    [Endpoint("us_mail", "3")]
    public sealed class UsMailV3 : Inference<UsMailV3Document, UsMailV3Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<UsMailV3Document>))]
        public override Pages<UsMailV3Document> Pages { get; set; }
    }
}
