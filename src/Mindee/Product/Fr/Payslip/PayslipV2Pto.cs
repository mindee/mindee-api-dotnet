using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Information about paid time off.
    /// </summary>
    public sealed class PayslipV2Pto
    {
        /// <summary>
        /// The amount of paid time off accrued in this period.
        /// </summary>
        [JsonPropertyName("accrued_this_period")]
        public double? AccruedThisPeriod { get; set; }

        /// <summary>
        /// The balance of paid time off at the end of the period.
        /// </summary>
        [JsonPropertyName("balance_end_of_period")]
        public double? BalanceEndOfPeriod { get; set; }

        /// <summary>
        /// The amount of paid time off used in this period.
        /// </summary>
        [JsonPropertyName("used_this_period")]
        public double? UsedThisPeriod { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Accrued This Period: {printable["AccruedThisPeriod"]}\n"
                + $"  :Balance End of Period: {printable["BalanceEndOfPeriod"]}\n"
                + $"  :Used This Period: {printable["UsedThisPeriod"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Accrued This Period: "
              + printable["AccruedThisPeriod"]
              + ", Balance End of Period: "
              + printable["BalanceEndOfPeriod"]
              + ", Used This Period: "
              + printable["UsedThisPeriod"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"AccruedThisPeriod", SummaryHelper.FormatAmount(AccruedThisPeriod)},
                {"BalanceEndOfPeriod", SummaryHelper.FormatAmount(BalanceEndOfPeriod)},
                {"UsedThisPeriod", SummaryHelper.FormatAmount(UsedThisPeriod)},
            };
        }
    }
}
