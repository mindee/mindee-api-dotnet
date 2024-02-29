using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    /// The list of the candidate's educational background.
    /// </summary>
    public sealed class ResumeV1Education : LineItemField
    {
        /// <summary>
        /// The area of study or specialization.
        /// </summary>
        [JsonPropertyName("degree_domain")]
        public string DegreeDomain { get; set; }

        /// <summary>
        /// The type of degree obtained, such as Bachelor's, Master's, or Doctorate.
        /// </summary>
        [JsonPropertyName("degree_type")]
        public string DegreeType { get; set; }

        /// <summary>
        /// The month when the education program or course was completed.
        /// </summary>
        [JsonPropertyName("end_month")]
        public string EndMonth { get; set; }

        /// <summary>
        /// The year when the education program or course was completed.
        /// </summary>
        [JsonPropertyName("end_year")]
        public string EndYear { get; set; }

        /// <summary>
        /// The name of the school.
        /// </summary>
        [JsonPropertyName("school")]
        public string School { get; set; }

        /// <summary>
        /// The month when the education program or course began.
        /// </summary>
        [JsonPropertyName("start_month")]
        public string StartMonth { get; set; }

        /// <summary>
        /// The year when the education program or course began.
        /// </summary>
        [JsonPropertyName("start_year")]
        public string StartYear { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-15}", printable["DegreeDomain"])
              + " | "
              + String.Format("{0,-25}", printable["DegreeType"])
              + " | "
              + String.Format("{0,-9}", printable["EndMonth"])
              + " | "
              + String.Format("{0,-8}", printable["EndYear"])
              + " | "
              + String.Format("{0,-25}", printable["School"])
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
            return "Domain: "
              + printable["DegreeDomain"]
              + ", Degree: "
              + printable["DegreeType"]
              + ", End Month: "
              + printable["EndMonth"]
              + ", End Year: "
              + printable["EndYear"]
              + ", School: "
              + printable["School"]
              + ", Start Month: "
              + printable["StartMonth"]
              + ", Start Year: "
              + printable["StartYear"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"DegreeDomain", SummaryHelper.FormatString(DegreeDomain, 15)},
                {"DegreeType", SummaryHelper.FormatString(DegreeType, 25)},
                {"EndMonth", SummaryHelper.FormatString(EndMonth)},
                {"EndYear", SummaryHelper.FormatString(EndYear)},
                {"School", SummaryHelper.FormatString(School, 25)},
                {"StartMonth", SummaryHelper.FormatString(StartMonth)},
                {"StartYear", SummaryHelper.FormatString(StartYear)},
            };
        }
    }

    /// <summary>
    /// The list of the candidate's educational background.
    /// </summary>
    public class ResumeV1Educations : List<ResumeV1Education>
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
            int[] columnSizes = { 17, 27, 11, 10, 27, 13, 12 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Domain          ");
            outStr.Append("| Degree                    ");
            outStr.Append("| End Month ");
            outStr.Append("| End Year ");
            outStr.Append("| School                    ");
            outStr.Append("| Start Month ");
            outStr.Append("| Start Year ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
