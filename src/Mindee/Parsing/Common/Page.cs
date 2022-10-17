using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define a page in the parsed document.
    /// </summary>
    /// <typeparam name="TPredictionModel">The prediction model type.</typeparam>
    public class Page<TPredictionModel>
        where TPredictionModel : class, new()
    {
        /// <summary>
        /// The id of the page. It starts at 0.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// <see cref="Orientation"/>
        /// </summary>
        [JsonPropertyName("orientation")]
        public Orientation Orientation { get; set; }

        /// <summary>
        /// The prediction model type.
        /// </summary>
        [JsonPropertyName("prediction")]
        public TPredictionModel Prediction { get; set; }
    }
}
