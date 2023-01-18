using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Us.BankCheck
{
    /// <summary>
    /// The us bank check model for the v1.
    /// </summary>
    public sealed class BankCheckV1DocumentPrediction : PredictionBase
    {
        /// <summary>
        /// Payer's bank account number.
        /// </summary>
        [JsonPropertyName("account_number")]
        public StringField AccountNumber { get; set; }

        /// <summary>
        /// Total including taxes.
        /// </summary>
        [JsonPropertyName("amount")]
        public AmountField Amount { get; set; }

        /// <summary>
        /// Payer's bank account number.
        /// </summary>
        [JsonPropertyName("check_number")]
        public StringField CheckNumber { get; set; }

        /// <summary>
        /// Check's position in the image.
        /// </summary>
        [JsonPropertyName("check_position")]
        public PositionField CheckPosition { get; set; }

        /// <summary>
        /// Date the check was issued.
        /// </summary>
        [JsonPropertyName("date")]
        public StringField IssuanceDate { get; set; }

        /// <summary>
        /// List of payees (full name or company name).
        /// </summary>
        [JsonPropertyName("payees")]
        public IList<StringField> Payees { get; set; } = new List<StringField>();

        /// <summary>
        /// Payer's bank account routing number.
        /// </summary>
        [JsonPropertyName("routing_number")]
        public StringField RoutingNumber { get; set; }

        /// <summary>
        /// Signatures' positions in the image.
        /// </summary>
        [JsonPropertyName("signatures_positions")]
        public IList<PositionField> SignaturesPositions { get; set; } = new List<PositionField>();

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("----- US Bank Check V1 -----\n");
            result.Append($"Routing number: {RoutingNumber?.Value}\n");
            result.Append($"Account number: {AccountNumber?.Value}\n");
            result.Append($"Check number: {CheckNumber?.Value}\n");
            result.Append($"Date: {IssuanceDate?.Value}\n");
            result.Append($"Amount: {Amount?.Value}\n");
            result.Append($"Payees: {string.Join(", ", Payees.Select(gn => gn.Value))}\n");

            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
