using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Eu.LicensePlate
{
    /// <summary>
    /// Document data for License Plate, API version 1.
    /// </summary>
    public class LicensePlateV1Document : IPrediction
    {
        /// <summary>
        /// List of all license plates found in the image.
        /// </summary>
        [JsonPropertyName("license_plates")]
        public IList<StringField> LicensePlates { get; set; } = new List<StringField>();

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string licensePlates = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 16)),
                LicensePlates.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":License Plates: {licensePlates}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
