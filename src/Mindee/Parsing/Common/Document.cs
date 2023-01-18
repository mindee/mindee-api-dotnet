using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the parsed document.
    /// </summary>
    /// <typeparam name="TInferenceModel">The model which defines the type of document.</typeparam>
    public class Document<TInferenceModel>
        where TInferenceModel : class, new()
    {
        /// <summary>
        /// The model which defines the type of document.
        /// </summary>
        [JsonPropertyName("inference")]
        public TInferenceModel Inference { get; set; }

        /// <summary>
        /// <see cref="Ocr"/>
        /// </summary>
        [JsonPropertyName("ocr")]
        public Ocr Ocr { get; set; }
    }
}
