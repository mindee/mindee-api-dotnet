using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Raw text extracted from the page.
    /// </summary>
    public class RawTextPage
    {
        /// <summary>
        /// Page content as a single string.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Page contents as a string.
        /// </summary>
        public override string ToString()
        {
            return Content ?? string.Empty;
        }
    }
}
