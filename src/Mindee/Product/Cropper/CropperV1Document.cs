using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Cropper
{
    /// <summary>
    /// Document data for Cropper, API version 1.
    /// </summary>
    public sealed class CropperV1Document : IPrediction
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
            string cropping = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 10)),
                Cropping.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Cropping: {cropping}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
