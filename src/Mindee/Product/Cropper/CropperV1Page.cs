using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Cropper
{
    /// <summary>
    /// Cropper API version 1.1 page data.
    /// </summary>
    public sealed class CropperV1Page : CropperV1Document
    {
        /// <summary>
        /// List of documents found in the image.
        /// </summary>
        [JsonPropertyName("cropping")]
        public IList<PositionField> Cropping { get; set; } = new List<PositionField>();

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string cropping = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 18)),
                Cropping.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Document Cropper: {cropping}\n");
            result.Append(base.ToString());
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
