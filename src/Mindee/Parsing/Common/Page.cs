using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define a page in the parsed document.
    /// </summary>
    /// <typeparam name="TPagePrediction">The prediction model type.</typeparam>
    public class Page<TPagePrediction>
        where TPagePrediction : IPrediction, new()
    {
        /// <summary>
        /// The id of the page. It starts at 0.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// <see cref="Common.Orientation"/>
        /// </summary>
        [JsonPropertyName("orientation")]
        public Orientation Orientation { get; set; }

        /// <summary>
        /// The prediction model type.
        /// </summary>
        [JsonPropertyName("prediction")]
        public TPagePrediction Prediction { get; set; }

        /// <summary>
        /// <see cref="Common.Extras"/>
        /// </summary>
        [JsonPropertyName("extras")]
        public Extras Extras { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"Page {Id}\n");
            result.Append("------\n");
            result.Append(Prediction.ToString());

            return result.ToString();
        }
    }
}
