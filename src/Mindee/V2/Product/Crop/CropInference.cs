using System;
using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Crop
{
    /// <summary>
    ///     The inference result for a crop utility request.
    /// </summary>
    public class CropInference : BaseInference
    {
        /// <summary>
        /// Result of a crop utility inference.
        /// </summary>
        [JsonPropertyName("result")]
        public CropResult Result { get; set; }

        /// <summary>
        /// Type of the product's response.
        /// </summary>
        public static new Type ResponseType => typeof(CropResponse);

        /// <summary>
        /// String representation of the crop inference.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + Result + "\n";
        }
    }
}
