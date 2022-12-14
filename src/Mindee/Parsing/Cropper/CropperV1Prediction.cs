using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
        public List<PositionField> Cropping { get; set; } = new List<PositionField>();

        /// <summary>
        /// A pretty summary of the value.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("----- Cropper Data -----\n");
            result.Append($"Cropping: {string.Join("\n          ", Cropping?.Select(c => c))}\n");
            result.Append("------------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
