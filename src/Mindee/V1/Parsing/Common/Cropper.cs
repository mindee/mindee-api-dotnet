using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.V1.Parsing.Standard;

namespace Mindee.V1.Parsing.Common
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
