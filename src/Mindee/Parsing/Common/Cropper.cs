using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing.Common
{
    /// <summary>
    ///     Cropping result.
    /// </summary>
    public sealed class Cropper
    {
        /// <summary>
        ///     List of positions within the image.
        /// </summary>
        [JsonPropertyName("cropping")]
        public List<PositionField> Cropping { get; set; }
    }
}
