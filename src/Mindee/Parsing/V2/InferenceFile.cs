using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// ResultFile info for V2 API.
    /// </summary>
    public class InferenceFile
    {
        /// <summary>
        /// ResultFile name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional file alias.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Page count.
        /// </summary>
        [JsonPropertyName("page_count")]
        public int PageCount { get; set; }

        /// <summary>
        /// MIME type.
        /// </summary>
        [JsonPropertyName("mime_type")]
        public string MimeType { get; set; }

        /// <summary>
        /// Pretty-prints the file section exactly as expected by Inference.ToString().
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder("File\n====");
            sb.Append($"\n:Name: {Name}");
            sb.Append($"\n:Alias: {Alias}");
            sb.Append($"\n:Page Count: {PageCount}");
            sb.Append($"\n:MIME Type: {MimeType}");
            return sb.ToString();
        }
    }
}
