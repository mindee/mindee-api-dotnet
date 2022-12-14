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
        /// Signatures' positions in the image.
        /// </summary>
        [JsonPropertyName("cropping")]
        public List<PositionField> Cropping { get; set; }
    }
}
