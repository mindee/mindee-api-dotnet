using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.DriverLicense
{
    /// <summary>
    /// Page data for Driver License, API version 1.
    /// </summary>
    public sealed class DriverLicenseV1Page : DriverLicenseV1Document
    {
        /// <summary>
        /// Has a photo of the US driver license holder
        /// </summary>
        [JsonPropertyName("photo")]
        public PositionField Photo { get; set; }

        /// <summary>
        /// Has a signature of the US driver license holder
        /// </summary>
        [JsonPropertyName("signature")]
        public PositionField Signature { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Photo: {Photo}\n");
            result.Append($":Signature: {Signature}\n");
            result.Append(base.ToString());
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
