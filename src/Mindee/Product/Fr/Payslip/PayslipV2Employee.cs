using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Information about the employee.
    /// </summary>
    public sealed class PayslipV2Employee
    {
        /// <summary>
        /// The address of the employee.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// The date of birth of the employee.
        /// </summary>
        [JsonPropertyName("date_of_birth")]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// The first name of the employee.
        /// </summary>
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the employee.
        /// </summary>
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The phone number of the employee.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The registration number of the employee.
        /// </summary>
        [JsonPropertyName("registration_number")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// The social security number of the employee.
        /// </summary>
        [JsonPropertyName("social_security_number")]
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Address: {printable["Address"]}\n"
                + $"  :Date of Birth: {printable["DateOfBirth"]}\n"
                + $"  :First Name: {printable["FirstName"]}\n"
                + $"  :Last Name: {printable["LastName"]}\n"
                + $"  :Phone Number: {printable["PhoneNumber"]}\n"
                + $"  :Registration Number: {printable["RegistrationNumber"]}\n"
                + $"  :Social Security Number: {printable["SocialSecurityNumber"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Address: "
              + printable["Address"]
              + ", Date of Birth: "
              + printable["DateOfBirth"]
              + ", First Name: "
              + printable["FirstName"]
              + ", Last Name: "
              + printable["LastName"]
              + ", Phone Number: "
              + printable["PhoneNumber"]
              + ", Registration Number: "
              + printable["RegistrationNumber"]
              + ", Social Security Number: "
              + printable["SocialSecurityNumber"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Address", SummaryHelper.FormatString(Address)},
                {"DateOfBirth", SummaryHelper.FormatString(DateOfBirth)},
                {"FirstName", SummaryHelper.FormatString(FirstName)},
                {"LastName", SummaryHelper.FormatString(LastName)},
                {"PhoneNumber", SummaryHelper.FormatString(PhoneNumber)},
                {"RegistrationNumber", SummaryHelper.FormatString(RegistrationNumber)},
                {"SocialSecurityNumber", SummaryHelper.FormatString(SocialSecurityNumber)},
            };
        }
    }
}
