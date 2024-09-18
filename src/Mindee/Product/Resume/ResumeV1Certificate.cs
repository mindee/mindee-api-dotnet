using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    /// The list of certificates obtained by the candidate.
    /// </summary>
    public sealed class ResumeV1Certificate : LineItemField
    {
        /// <summary>
        /// The grade obtained for the certificate.
        /// </summary>
        [JsonPropertyName("grade")]
        public string Grade { get; set; }

        /// <summary>
        /// The name of certification.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The organization or institution that issued the certificate.
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// The year when a certificate was issued or received.
        /// </summary>
        [JsonPropertyName("year")]
        public string Year { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Grade", SummaryHelper.FormatString(Grade, 10)},
                {"Name", SummaryHelper.FormatString(Name, 30)},
                {"Provider", SummaryHelper.FormatString(Provider, 25)},
                {"Year", SummaryHelper.FormatString(Year)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-10}", printable["Grade"])
              + " | "
              + String.Format("{0,-30}", printable["Name"])
              + " | "
              + String.Format("{0,-25}", printable["Provider"])
              + " | "
              + String.Format("{0,-4}", printable["Year"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Grade: "
              + printable["Grade"]
              + ", Name: "
              + printable["Name"]
              + ", Provider: "
              + printable["Provider"]
              + ", Year: "
              + printable["Year"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Grade", SummaryHelper.FormatString(Grade)},
                {"Name", SummaryHelper.FormatString(Name)},
                {"Provider", SummaryHelper.FormatString(Provider)},
                {"Year", SummaryHelper.FormatString(Year)},
            };
        }
    }

    /// <summary>
    /// The list of certificates obtained by the candidate.
    /// </summary>
    public class ResumeV1Certificates : List<ResumeV1Certificate>
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
            int[] columnSizes = { 12, 32, 27, 6 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Grade      ");
            outStr.Append("| Name                           ");
            outStr.Append("| Provider                  ");
            outStr.Append("| Year ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
