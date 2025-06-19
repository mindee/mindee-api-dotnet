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
        [JsonPropertyName("fields")]
        public GeneratedV2 Fields { get; set; }

        /// <summary>
        /// <see cref="Common.OptionsV2"/>
        /// </summary>
        [JsonPropertyName("options")]
        public OptionsV2 Options { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"Page {Id}\n");
            result.Append("------\n");
            result.Append(Fields.ToString());

            return result.ToString();
        }
    }
}
