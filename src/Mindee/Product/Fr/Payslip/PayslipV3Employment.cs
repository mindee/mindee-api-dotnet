using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Information about the employment.
    /// </summary>
    public sealed class PayslipV3Employment
    {
        /// <summary>
        /// The category of the employment.
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// The coefficient of the employment.
        /// </summary>
        [JsonPropertyName("coefficient")]
        public string Coefficient { get; set; }

        /// <summary>
        /// The collective agreement of the employment.
        /// </summary>
        [JsonPropertyName("collective_agreement")]
        public string CollectiveAgreement { get; set; }

        /// <summary>
        /// The job title of the employee.
        /// </summary>
        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// The position level of the employment.
        /// </summary>
        [JsonPropertyName("position_level")]
        public string PositionLevel { get; set; }

        /// <summary>
        /// The seniority date of the employment.
        /// </summary>
        [JsonPropertyName("seniority_date")]
        public string SeniorityDate { get; set; }

        /// <summary>
        /// The start date of the employment.
        /// </summary>
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Category: {printable["Category"]}\n"
                + $"  :Coefficient: {printable["Coefficient"]}\n"
                + $"  :Collective Agreement: {printable["CollectiveAgreement"]}\n"
                + $"  :Job Title: {printable["JobTitle"]}\n"
                + $"  :Position Level: {printable["PositionLevel"]}\n"
                + $"  :Seniority Date: {printable["SeniorityDate"]}\n"
                + $"  :Start Date: {printable["StartDate"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Category: "
              + printable["Category"]
              + ", Coefficient: "
              + printable["Coefficient"]
              + ", Collective Agreement: "
              + printable["CollectiveAgreement"]
              + ", Job Title: "
              + printable["JobTitle"]
              + ", Position Level: "
              + printable["PositionLevel"]
              + ", Seniority Date: "
              + printable["SeniorityDate"]
              + ", Start Date: "
              + printable["StartDate"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Category", SummaryHelper.FormatString(Category)},
                {"Coefficient", SummaryHelper.FormatString(Coefficient)},
                {"CollectiveAgreement", SummaryHelper.FormatString(CollectiveAgreement)},
                {"JobTitle", SummaryHelper.FormatString(JobTitle)},
                {"PositionLevel", SummaryHelper.FormatString(PositionLevel)},
                {"SeniorityDate", SummaryHelper.FormatString(SeniorityDate)},
                {"StartDate", SummaryHelper.FormatString(StartDate)},
            };
        }
    }
}
