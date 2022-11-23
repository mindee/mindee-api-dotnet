using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Cropper
{
    /// <summary>
    /// The cropper model for the v1.
    /// </summary>
    [Endpoint("cropper", "1")]
    public sealed class CropperV1Prediction
    {
        /// <summary>
        /// List of all detected cropped elements in the image.
        /// </summary>
        [JsonPropertyName("cropping")]
        public List<PositionField> Cropping { get; set; }
    }
}
