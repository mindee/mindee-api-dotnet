using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Information about the pay period.
    /// </summary>
    public sealed class PayslipV2PayPeriod
    {
        /// <summary>
        /// The end date of the pay period.
        /// </summary>
        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        /// <summary>
        /// The month of the pay period.
        /// </summary>
        [JsonPropertyName("month")]
        public string Month { get; set; }

        /// <summary>
        /// The date of payment for the pay period.
        /// </summary>
        [JsonPropertyName("payment_date")]
        public string PaymentDate { get; set; }

        /// <summary>
        /// The start date of the pay period.
        /// </summary>
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        /// <summary>
        /// The year of the pay period.
        /// </summary>
        [JsonPropertyName("year")]
        public string Year { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :End Date: {printable["EndDate"]}\n"
                + $"  :Month: {printable["Month"]}\n"
                + $"  :Payment Date: {printable["PaymentDate"]}\n"
                + $"  :Start Date: {printable["StartDate"]}\n"
                + $"  :Year: {printable["Year"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "End Date: "
              + printable["EndDate"]
              + ", Month: "
              + printable["Month"]
              + ", Payment Date: "
              + printable["PaymentDate"]
              + ", Start Date: "
              + printable["StartDate"]
              + ", Year: "
              + printable["Year"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"EndDate", SummaryHelper.FormatString(EndDate)},
                {"Month", SummaryHelper.FormatString(Month)},
                {"PaymentDate", SummaryHelper.FormatString(PaymentDate)},
                {"StartDate", SummaryHelper.FormatString(StartDate)},
                {"Year", SummaryHelper.FormatString(Year)},
            };
        }
    }
}
