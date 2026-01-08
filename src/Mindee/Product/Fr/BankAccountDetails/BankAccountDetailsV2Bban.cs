using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    ///     Full extraction of BBAN, including: branch code, bank code, account and key.
    /// </summary>
    public sealed class BankAccountDetailsV2Bban
    {
        /// <summary>
        ///     The BBAN bank code outputted as a string.
        /// </summary>
        [JsonPropertyName("bban_bank_code")]
        public string BbanBankCode { get; set; }

        /// <summary>
        ///     The BBAN branch code outputted as a string.
        /// </summary>
        [JsonPropertyName("bban_branch_code")]
        public string BbanBranchCode { get; set; }

        /// <summary>
        ///     The BBAN key outputted as a string.
        /// </summary>
        [JsonPropertyName("bban_key")]
        public string BbanKey { get; set; }

        /// <summary>
        ///     The BBAN Account number outputted as a string.
        /// </summary>
        [JsonPropertyName("bban_number")]
        public string BbanNumber { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :Bank Code: {printable["BbanBankCode"]}\n"
                   + $"  :Branch Code: {printable["BbanBranchCode"]}\n"
                   + $"  :Key: {printable["BbanKey"]}\n"
                   + $"  :Account Number: {printable["BbanNumber"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Bank Code: "
                   + printable["BbanBankCode"]
                   + ", Branch Code: "
                   + printable["BbanBranchCode"]
                   + ", Key: "
                   + printable["BbanKey"]
                   + ", Account Number: "
                   + printable["BbanNumber"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "BbanBankCode", SummaryHelper.FormatString(BbanBankCode) },
                { "BbanBranchCode", SummaryHelper.FormatString(BbanBranchCode) },
                { "BbanKey", SummaryHelper.FormatString(BbanKey) },
                { "BbanNumber", SummaryHelper.FormatString(BbanNumber) }
            };
        }
    }
}
