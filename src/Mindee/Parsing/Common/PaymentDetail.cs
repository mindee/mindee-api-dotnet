using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a payment detail.
    /// </summary>
    public class PaymentDetail : BaseField
    {
        /// <summary>
        /// The account number.
        /// </summary>
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// The full IBAN.
        /// </summary>
        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        /// <summary>
        /// The routing number.
        /// </summary>
        [JsonPropertyName("routing_number")]
        public string RoutingNumber { get; set; }

        /// <summary>
        /// The SWIFT value.
        /// </summary>
        [JsonPropertyName("swift")]
        public string Swift { get; set; }

        /// <summary>
        /// A prettier reprensentation of the payment details.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(AccountNumber))
            {
                result.Append($"{AccountNumber}; ");
            }
            if (!string.IsNullOrWhiteSpace(Iban))
            {
                result.Append($"{Iban}; ");
            }
            if (!string.IsNullOrWhiteSpace(RoutingNumber))
            {
                result.Append($"{RoutingNumber}; ");
            }
            if (!string.IsNullOrWhiteSpace(Swift))
            {
                result.Append($"{Swift}; ");
            }

            return result.ToString().Trim();
        }
    }
}
