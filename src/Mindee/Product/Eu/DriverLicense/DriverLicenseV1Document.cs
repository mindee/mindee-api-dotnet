using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Eu.DriverLicense
{
    /// <summary>
    /// Driver License API version 1.0 document data.
    /// </summary>
    public class DriverLicenseV1Document : IPrediction
    {
        /// <summary>
        /// EU driver license holders address
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        /// EU driver license holders categories
        /// </summary>
        [JsonPropertyName("category")]
        public StringField Category { get; set; }

        /// <summary>
        /// Country code extracted as a string.
        /// </summary>
        [JsonPropertyName("country_code")]
        public StringField CountryCode { get; set; }

        /// <summary>
        /// The date of birth of the document holder
        /// </summary>
        [JsonPropertyName("date_of_birth")]
        public DateField DateOfBirth { get; set; }

        /// <summary>
        /// ID number of the Document.
        /// </summary>
        [JsonPropertyName("document_id")]
        public StringField DocumentId { get; set; }

        /// <summary>
        /// Date the document expires
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// First name(s) of the driver license holder
        /// </summary>
        [JsonPropertyName("first_name")]
        public StringField FirstName { get; set; }

        /// <summary>
        /// Authority that issued the document
        /// </summary>
        [JsonPropertyName("issue_authority")]
        public StringField IssueAuthority { get; set; }

        /// <summary>
        /// Date the document was issued
        /// </summary>
        [JsonPropertyName("issue_date")]
        public DateField IssueDate { get; set; }

        /// <summary>
        /// Last name of the driver license holder.
        /// </summary>
        [JsonPropertyName("last_name")]
        public StringField LastName { get; set; }

        /// <summary>
        /// Machine-readable license number
        /// </summary>
        [JsonPropertyName("mrz")]
        public StringField Mrz { get; set; }

        /// <summary>
        /// Place where the driver license holder was born
        /// </summary>
        [JsonPropertyName("place_of_birth")]
        public StringField PlaceOfBirth { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Country Code: {CountryCode}\n");
            result.Append($":Document ID: {DocumentId}\n");
            result.Append($":Driver License Category: {Category}\n");
            result.Append($":Last Name: {LastName}\n");
            result.Append($":First Name: {FirstName}\n");
            result.Append($":Date Of Birth: {DateOfBirth}\n");
            result.Append($":Place Of Birth: {PlaceOfBirth}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":Issue Date: {IssueDate}\n");
            result.Append($":Issue Authority: {IssueAuthority}\n");
            result.Append($":MRZ: {Mrz}\n");
            result.Append($":Address: {Address}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
