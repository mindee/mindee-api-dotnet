using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Crop
{
    /// <summary>
    /// Result of a crop utility inference.
    /// </summary>
    public class CropResult
    {
        /// <summary>
        /// List of results of cropped document regions.
        /// </summary>
        [JsonPropertyName("crops")]
        public List<CropItem> Crops { get; set; }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string crops = string.Join("\n", this.Crops.Select(item => item.ToString()));

            return $"Crops\n=====\n{crops}";
        }
    }
}
