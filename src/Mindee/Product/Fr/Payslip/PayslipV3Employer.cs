using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    ///     Information about the employer.
    /// </summary>
    public sealed class PayslipV3Employer
    {
        /// <summary>
        ///     The address of the employer.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        ///     The company ID of the employer.
        /// </summary>
        [JsonPropertyName("company_id")]
        public string CompanyId { get; set; }

        /// <summary>
        ///     The site of the company.
        /// </summary>
        [JsonPropertyName("company_site")]
        public string CompanySite { get; set; }

        /// <summary>
        ///     The NAF code of the employer.
        /// </summary>
        [JsonPropertyName("naf_code")]
        public string NafCode { get; set; }

        /// <summary>
        ///     The name of the employer.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The phone number of the employer.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     The URSSAF number of the employer.
        /// </summary>
        [JsonPropertyName("urssaf_number")]
        public string UrssafNumber { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :Address: {printable["Address"]}\n"
                   + $"  :Company ID: {printable["CompanyId"]}\n"
                   + $"  :Company Site: {printable["CompanySite"]}\n"
                   + $"  :NAF Code: {printable["NafCode"]}\n"
                   + $"  :Name: {printable["Name"]}\n"
                   + $"  :Phone Number: {printable["PhoneNumber"]}\n"
                   + $"  :URSSAF Number: {printable["UrssafNumber"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Address: "
                   + printable["Address"]
                   + ", Company ID: "
                   + printable["CompanyId"]
                   + ", Company Site: "
                   + printable["CompanySite"]
                   + ", NAF Code: "
                   + printable["NafCode"]
                   + ", Name: "
                   + printable["Name"]
                   + ", Phone Number: "
                   + printable["PhoneNumber"]
                   + ", URSSAF Number: "
                   + printable["UrssafNumber"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Address", SummaryHelper.FormatString(Address) },
                { "CompanyId", SummaryHelper.FormatString(CompanyId) },
                { "CompanySite", SummaryHelper.FormatString(CompanySite) },
                { "NafCode", SummaryHelper.FormatString(NafCode) },
                { "Name", SummaryHelper.FormatString(Name) },
                { "PhoneNumber", SummaryHelper.FormatString(PhoneNumber) },
                { "UrssafNumber", SummaryHelper.FormatString(UrssafNumber) }
            };
        }
    }
}
