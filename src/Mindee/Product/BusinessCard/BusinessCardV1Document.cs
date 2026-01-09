using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.BusinessCard
{
    /// <summary>
    /// Business Card API version 1.0 document data.
    /// </summary>
    public class BusinessCardV1Document : IPrediction
    {
        /// <summary>
        /// The address of the person.
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        /// The company the person works for.
        /// </summary>
        [JsonPropertyName("company")]
        public StringField Company { get; set; }

        /// <summary>
        /// The email address of the person.
        /// </summary>
        [JsonPropertyName("email")]
        public StringField Email { get; set; }

        /// <summary>
        /// The Fax number of the person.
        /// </summary>
        [JsonPropertyName("fax_number")]
        public StringField FaxNumber { get; set; }

        /// <summary>
        /// The given name of the person.
        /// </summary>
        [JsonPropertyName("firstname")]
        public StringField Firstname { get; set; }

        /// <summary>
        /// The job title of the person.
        /// </summary>
        [JsonPropertyName("job_title")]
        public StringField JobTitle { get; set; }

        /// <summary>
        /// The lastname of the person.
        /// </summary>
        [JsonPropertyName("lastname")]
        public StringField Lastname { get; set; }

        /// <summary>
        /// The mobile number of the person.
        /// </summary>
        [JsonPropertyName("mobile_number")]
        public StringField MobileNumber { get; set; }

        /// <summary>
        /// The phone number of the person.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public StringField PhoneNumber { get; set; }

        /// <summary>
        /// The social media profiles of the person or company.
        /// </summary>
        [JsonPropertyName("social_media")]
        public IList<StringField> SocialMedia { get; set; } = new List<StringField>();

        /// <summary>
        /// The website of the person or company.
        /// </summary>
        [JsonPropertyName("website")]
        public StringField Website { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string socialMedia = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 14)),
                SocialMedia.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Firstname: {Firstname}\n");
            result.Append($":Lastname: {Lastname}\n");
            result.Append($":Job Title: {JobTitle}\n");
            result.Append($":Company: {Company}\n");
            result.Append($":Email: {Email}\n");
            result.Append($":Phone Number: {PhoneNumber}\n");
            result.Append($":Mobile Number: {MobileNumber}\n");
            result.Append($":Fax Number: {FaxNumber}\n");
            result.Append($":Address: {Address}\n");
            result.Append($":Website: {Website}\n");
            result.Append($":Social Media: {socialMedia}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
