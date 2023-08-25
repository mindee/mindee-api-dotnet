using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Cropping result.
    /// </summary>
    public sealed class Cropper
    {
        /// <summary>
        /// List of positions within the image.
        /// </summary>
        [JsonPropertyName("cropping")]
        public List<Standard.PositionField> Cropping { get; set; }
    }
}
