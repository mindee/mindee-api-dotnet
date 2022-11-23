using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Optionnals informationabout the document.
    /// </summary>
    public sealed class Extras
    {
        /// <summary>
        /// Cropping result.
        /// </summary>
        [JsonPropertyName("cropper")]
        public Cropper Cropper { get; set; }
    }
}
