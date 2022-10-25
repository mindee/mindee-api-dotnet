using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class PaymentDetail : BaseField
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        [JsonPropertyName("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonPropertyName("swift")]
        public string Swift { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            if(!string.IsNullOrWhiteSpace(AccountNumber))
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
