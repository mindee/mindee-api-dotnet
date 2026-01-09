using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Information about the employee's bank account.
    /// </summary>
    public sealed class PayslipV3BankAccountDetail
    {
        /// <summary>
        /// The name of the bank.
        /// </summary>
        [JsonPropertyName("bank_name")]
        public string BankName { get; set; }

        /// <summary>
        /// The IBAN of the bank account.
        /// </summary>
        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        /// <summary>
        /// The SWIFT code of the bank.
        /// </summary>
        [JsonPropertyName("swift")]
        public string Swift { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Bank Name: {printable["BankName"]}\n"
                + $"  :IBAN: {printable["Iban"]}\n"
                + $"  :SWIFT: {printable["Swift"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Bank Name: "
              + printable["BankName"]
              + ", IBAN: "
              + printable["Iban"]
              + ", SWIFT: "
              + printable["Swift"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"BankName", SummaryHelper.FormatString(BankName)},
                {"Iban", SummaryHelper.FormatString(Iban)},
                {"Swift", SummaryHelper.FormatString(Swift)},
            };
        }
    }
}
