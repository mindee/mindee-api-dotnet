using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Eu.LicensePlates
{
    /// <summary>
    /// The eu licence plates model for the v1.
    /// </summary>
    public sealed class LicensePlatesV1DocumentPrediction : PredictionBase
    {
        /// <summary>
        /// A list of license plates values.
        /// </summary>
        [JsonPropertyName("license_plates")]
        public IList<StringField> LicensePlates { get; set; } = new List<StringField>();

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("----- EU License plate V1 -----\n");
            result.Append($"License plates: {string.Join(", ", LicensePlates.Select(lp => lp.Value))}\n");

            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
