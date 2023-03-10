using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.ShippingContainer
{
    /// <summary>
    /// Document data for Shipping Container, API version 1.
    /// </summary>
    public class ShippingContainerV1DocumentPrediction
    {
        /// <summary>
        /// The ISO-6346 code for container owner and equipment identifier.
        /// </summary>
        [JsonPropertyName("owner")]
        public StringField Owner { get; set; }

        /// <summary>
        /// ISO-6346 code for container serial number.
        /// </summary>
        [JsonPropertyName("serial_number")]
        public StringField SerialNumber { get; set; }

        /// <summary>
        /// ISO 6346 code for container length, height and type.
        /// </summary>
        [JsonPropertyName("size_type")]
        public StringField SizeType { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {

            StringBuilder result = new StringBuilder();
            result.Append($":Owner: {Owner}\n");
            result.Append($":Serial Number: {SerialNumber}\n");
            result.Append($":Size and Type: {SizeType}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
