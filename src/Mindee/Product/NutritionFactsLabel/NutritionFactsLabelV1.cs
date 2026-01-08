using System.Text.Json.Serialization;
using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.NutritionFactsLabel
{
    /// <summary>
    ///     Nutrition Facts Label API version 1 inference prediction.
    /// </summary>
    [Endpoint("nutrition_facts", "1")]
    public sealed class NutritionFactsLabelV1 : Inference<NutritionFactsLabelV1Document, NutritionFactsLabelV1Document>
    {
        /// <summary>
        ///     The pages and the associated values which were detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        [JsonConverter(typeof(PagesJsonConverter<NutritionFactsLabelV1Document>))]
        public override Pages<NutritionFactsLabelV1Document> Pages { get; set; }
    }
}
