using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.ShippingContainer
{
    /// <summary>
    /// The shipping container model for the v1.
    /// </summary>
    [Endpoint("shipping_containers", "1")]
    public sealed class ShippingContainerV1Prediction : PredictionBase
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
            StringBuilder result = new StringBuilder("----- Shipping Container V1 -----\n");
            result.Append($"Owner: {Owner.Value}\n");
            result.Append($"Serial number: {SerialNumber.Value}\n");
            result.Append($"Size and type: {SizeType.Value}\n");

            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
