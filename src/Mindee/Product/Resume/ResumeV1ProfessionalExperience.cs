using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    ///     The list of the candidate's professional experiences.
    /// </summary>
    public sealed class ResumeV1ProfessionalExperience : LineItemField
    {
        /// <summary>
        ///     The type of contract for the professional experience.
        /// </summary>
        [JsonPropertyName("contract_type")]
        public string ContractType { get; set; }

        /// <summary>
        ///     The specific department or division within the company.
        /// </summary>
        [JsonPropertyName("department")]
        public string Department { get; set; }

        /// <summary>
        ///     The description of the professional experience as written in the document.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The name of the company or organization.
        /// </summary>
        [JsonPropertyName("employer")]
        public string Employer { get; set; }

        /// <summary>
        ///     The month when the professional experience ended.
        /// </summary>
        [JsonPropertyName("end_month")]
        public string EndMonth { get; set; }

        /// <summary>
        ///     The year when the professional experience ended.
        /// </summary>
        [JsonPropertyName("end_year")]
        public string EndYear { get; set; }

        /// <summary>
        ///     The position or job title held by the candidate.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        ///     The month when the professional experience began.
        /// </summary>
        [JsonPropertyName("start_month")]
        public string StartMonth { get; set; }

        /// <summary>
        ///     The year when the professional experience began.
        /// </summary>
        [JsonPropertyName("start_year")]
        public string StartYear { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "ContractType", SummaryHelper.FormatString(ContractType, 15) },
                { "Department", SummaryHelper.FormatString(Department, 10) },
                { "Description", SummaryHelper.FormatString(Description, 36) },
                { "Employer", SummaryHelper.FormatString(Employer, 25) },
                { "EndMonth", SummaryHelper.FormatString(EndMonth) },
                { "EndYear", SummaryHelper.FormatString(EndYear) },
                { "Role", SummaryHelper.FormatString(Role, 20) },
                { "StartMonth", SummaryHelper.FormatString(StartMonth) },
                { "StartYear", SummaryHelper.FormatString(StartYear) }
            };
        }

        /// <summary>
        ///     Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            var printable = TablePrintableValues();
            return "| "
                   + string.Format("{0,-15}", printable["ContractType"])
                   + " | "
                   + string.Format("{0,-10}", printable["Department"])
                   + " | "
                   + string.Format("{0,-36}", printable["Description"])
                   + " | "
                   + string.Format("{0,-25}", printable["Employer"])
                   + " | "
                   + string.Format("{0,-9}", printable["EndMonth"])
                   + " | "
                   + string.Format("{0,-8}", printable["EndYear"])
                   + " | "
                   + string.Format("{0,-20}", printable["Role"])
                   + " | "
                   + string.Format("{0,-11}", printable["StartMonth"])
                   + " | "
                   + string.Format("{0,-10}", printable["StartYear"])
                   + " |";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Contract Type: "
                   + printable["ContractType"]
                   + ", Department: "
                   + printable["Department"]
                   + ", Description: "
                   + printable["Description"]
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
            return new Dictionary<string, string>
            {
                { "ContractType", SummaryHelper.FormatString(ContractType) },
                { "Department", SummaryHelper.FormatString(Department) },
                { "Description", SummaryHelper.FormatString(Description) },
                { "Employer", SummaryHelper.FormatString(Employer) },
                { "EndMonth", SummaryHelper.FormatString(EndMonth) },
                { "EndYear", SummaryHelper.FormatString(EndYear) },
                { "Role", SummaryHelper.FormatString(Role) },
                { "StartMonth", SummaryHelper.FormatString(StartMonth) },
                { "StartYear", SummaryHelper.FormatString(StartYear) }
            };
        }
    }

    /// <summary>
    ///     The list of the candidate's professional experiences.
    /// </summary>
    public class ResumeV1ProfessionalExperiences : List<ResumeV1ProfessionalExperience>
    {
        /// <summary>
        ///     Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (Count == 0)
            {
                return "\n";
            }

            int[] columnSizes = { 17, 12, 38, 27, 11, 10, 22, 13, 12 };
            var outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Contract Type   ");
            outStr.Append("| Department ");
            outStr.Append("| Description                          ");
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
