using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.ShippingContainer
{
    /// <summary>
    /// The shipping container model for the v1.
    /// </summary>
    public sealed class ShippingContainerV1DocumentPrediction : PredictionBase
    {
        /// <summary>
        /// ISO 6346 code for container owner prefix + equipment identifier.
        /// </summary>
        [JsonPropertyName("owner")]
        public StringField Owner { get; set; }

        /// <summary>
        /// ISO 6346 code for container serial number (6+1 digits).
        /// </summary>
        [JsonPropertyName("serial_number")]
        public StringField SerialNumber { get; set; }

        /// <summary>
        /// ISO 6346 code for container length, height and type.
        /// </summary>
        [JsonPropertyName("size_type")]
        public StringField SizeType { get; set; }

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Owner: {Owner}\n");
            result.Append($":Serial number: {SerialNumber}\n");
            result.Append($":Size and type: {SizeType}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
