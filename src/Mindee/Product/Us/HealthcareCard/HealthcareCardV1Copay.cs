using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.HealthcareCard
{
    /// <summary>
    /// Copayments for covered services.
    /// </summary>
    public sealed class HealthcareCardV1Copay : LineItemField
    {
        /// <summary>
        /// The price of the service.
        /// </summary>
        [JsonPropertyName("service_fees")]
        public double? ServiceFees { get; set; }

        /// <summary>
        /// The name of the service.
        /// </summary>
        [JsonPropertyName("service_name")]
        public string ServiceName { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"ServiceFees", SummaryHelper.FormatAmount(ServiceFees)},
                {"ServiceName", SummaryHelper.FormatString(ServiceName, 20)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-12}", printable["ServiceFees"])
              + " | "
              + String.Format("{0,-20}", printable["ServiceName"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Service Fees: "
              + printable["ServiceFees"]
              + ", Service Name: "
              + printable["ServiceName"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"ServiceFees", SummaryHelper.FormatAmount(ServiceFees)},
                {"ServiceName", SummaryHelper.FormatString(ServiceName)},
            };
        }
    }

    /// <summary>
    /// Copayments for covered services.
    /// </summary>
    public class HealthcareCardV1Copays : List<HealthcareCardV1Copay>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            int[] columnSizes = { 14, 22 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Service Fees ");
            outStr.Append("| Service Name         ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
