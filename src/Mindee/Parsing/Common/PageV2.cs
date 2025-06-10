using System.Text;
using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Page structure in the parsed document on the API V2.
    /// </summary>
    public class PageV2
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
        public GeneratedV2 Prediction { get; set; }

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
