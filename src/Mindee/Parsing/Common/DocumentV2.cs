using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the parsed document with the API V2 format.
    /// </summary>
    public class DocumentV2
    {
        /// <summary>
        /// The original file name of the parsed document.
        /// </summary>
        [JsonPropertyName("model")]
        public Model Model { get; set; }

        /// <summary>
        /// The model which defines the type of document.
        /// </summary>
        [JsonPropertyName("inference")]
        public InferenceV2 Inference { get; set; }

        /// <summary>
        /// The original file name of the parsed document.
        /// </summary>
        [JsonPropertyName("name")]
        public string Filename { get; set; }

        /// <summary>
        /// The Mindee ID of the current document.
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
