using System.Collections.Generic;
using System.Linq;
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
            return
                SummaryHelper.Clean($":License plates: {string.Join(", ", LicensePlates.Select(lp => lp))}\n");
        }
    }
}
