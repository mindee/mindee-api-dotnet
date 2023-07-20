using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.BankCheck
{
    /// <summary>
    /// Document data for Bank Checks (beta), API version 1.
    /// </summary>
    public class BankCheckV1Document : IPrediction
    {
        /// <summary>
        /// The check payer's account number.
        /// </summary>
        [JsonPropertyName("account_number")]
        public StringField AccountNumber { get; set; }

        /// <summary>
        /// The amount of the check.
        /// </summary>
        [JsonPropertyName("amount")]
        public AmountField Amount { get; set; }

        /// <summary>
        /// The issuer's check number.
        /// </summary>
        [JsonPropertyName("check_number")]
        public StringField CheckNumber { get; set; }

        /// <summary>
        /// The date the check was issued.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        /// List of the check's payees (recipients).
        /// </summary>
        [JsonPropertyName("payees")]
        public IList<StringField> Payees { get; set; } = new List<StringField>();

        /// <summary>
        /// The check issuer's routing number.
        /// </summary>
        [JsonPropertyName("routing_number")]
        public StringField RoutingNumber { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string payees = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 8)),
                Payees.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Check Issue Date: {Date}\n");
            result.Append($":Amount: {Amount}\n");
            result.Append($":Payees: {payees}\n");
            result.Append($":Routing Number: {RoutingNumber}\n");
            result.Append($":Account Number: {AccountNumber}\n");
            result.Append($":Check Number: {CheckNumber}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
