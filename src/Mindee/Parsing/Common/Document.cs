using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the parsed document with the legacy format.
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

        /// <summary>
        /// The original file name of the parsed document.
        /// </summary>
        [JsonPropertyName("name")]
        public string Filename { get; set; }

        /// <summary>
        /// The Mindee Id of the current document.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Number of pages in the document.
        /// </summary>
        [JsonPropertyName("n_pages")]
        public int NPages { get; set; }

        /// <summary>
        /// A prettier representation.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("########\n");
            result.Append("Document\n");
            result.Append("########\n");
            result.Append($":Mindee ID: {Id}\n");
            result.Append($":Filename: {Filename}\n");
            result.Append(Inference);

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
