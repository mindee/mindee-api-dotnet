using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    /// The list of the candidate's professional experiences.
    /// </summary>
    public sealed class ResumeV1ProfessionalExperience : LineItemField
    {
        /// <summary>
        /// The type of contract for the professional experience.
        /// </summary>
        [JsonPropertyName("contract_type")]
        public string ContractType { get; set; }

        /// <summary>
        /// The specific department or division within the company.
        /// </summary>
        [JsonPropertyName("department")]
        public string Department { get; set; }

        /// <summary>
        /// The name of the company or organization.
        /// </summary>
        [JsonPropertyName("employer")]
        public string Employer { get; set; }

        /// <summary>
        /// The month when the professional experience ended.
        /// </summary>
        [JsonPropertyName("end_month")]
        public string EndMonth { get; set; }

        /// <summary>
        /// The year when the professional experience ended.
        /// </summary>
        [JsonPropertyName("end_year")]
        public string EndYear { get; set; }

        /// <summary>
        /// The position or job title held by the candidate.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// The month when the professional experience began.
        /// </summary>
        [JsonPropertyName("start_month")]
        public string StartMonth { get; set; }

        /// <summary>
        /// The year when the professional experience began.
        /// </summary>
        [JsonPropertyName("start_year")]
        public string StartYear { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"ContractType", SummaryHelper.FormatString(ContractType, 15)},
                {"Department", SummaryHelper.FormatString(Department, 10)},
                {"Employer", SummaryHelper.FormatString(Employer, 25)},
                {"EndMonth", SummaryHelper.FormatString(EndMonth)},
                {"EndYear", SummaryHelper.FormatString(EndYear)},
                {"Role", SummaryHelper.FormatString(Role, 20)},
                {"StartMonth", SummaryHelper.FormatString(StartMonth)},
                {"StartYear", SummaryHelper.FormatString(StartYear)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-15}", printable["ContractType"])
              + " | "
              + String.Format("{0,-10}", printable["Department"])
              + " | "
              + String.Format("{0,-25}", printable["Employer"])
              + " | "
              + String.Format("{0,-9}", printable["EndMonth"])
              + " | "
              + String.Format("{0,-8}", printable["EndYear"])
              + " | "
              + String.Format("{0,-20}", printable["Role"])
              + " | "
              + String.Format("{0,-11}", printable["StartMonth"])
              + " | "
              + String.Format("{0,-10}", printable["StartYear"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Contract Type: "
              + printable["ContractType"]
              + ", Department: "
              + printable["Department"]
              + ", Employer: "
              + printable["Employer"]
              + ", End Month: "
              + printable["EndMonth"]
              + ", End Year: "
              + printable["EndYear"]
              + ", Role: "
              + printable["Role"]
              + ", Start Month: "
              + printable["StartMonth"]
              + ", Start Year: "
              + printable["StartYear"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"ContractType", SummaryHelper.FormatString(ContractType)},
                {"Department", SummaryHelper.FormatString(Department)},
                {"Employer", SummaryHelper.FormatString(Employer)},
                {"EndMonth", SummaryHelper.FormatString(EndMonth)},
                {"EndYear", SummaryHelper.FormatString(EndYear)},
                {"Role", SummaryHelper.FormatString(Role)},
                {"StartMonth", SummaryHelper.FormatString(StartMonth)},
                {"StartYear", SummaryHelper.FormatString(StartYear)},
            };
        }
    }

    /// <summary>
    /// The list of the candidate's professional experiences.
    /// </summary>
    public class ResumeV1ProfessionalExperiences : List<ResumeV1ProfessionalExperience>
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
            int[] columnSizes = { 17, 12, 27, 11, 10, 22, 13, 12 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Contract Type   ");
            outStr.Append("| Department ");
            outStr.Append("| Employer                  ");
            outStr.Append("| End Month ");
            outStr.Append("| End Year ");
            outStr.Append("| Role                 ");
            outStr.Append("| Start Month ");
            outStr.Append("| Start Year ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
