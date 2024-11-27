using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.DriverLicense
{
    /// <summary>
    /// Driver License API version 1.0 document data.
    /// </summary>
    public class DriverLicenseV1Document : IPrediction
    {
        /// <summary>
        /// The category or class of the driver license.
        /// </summary>
        [JsonPropertyName("category")]
        public StringField Category { get; set; }

        /// <summary>
        /// The alpha-3 ISO 3166 code of the country where the driver license was issued.
        /// </summary>
        [JsonPropertyName("country_code")]
        public StringField CountryCode { get; set; }

        /// <summary>
        /// The date of birth of the driver license holder.
        /// </summary>
        [JsonPropertyName("date_of_birth")]
        public DateField DateOfBirth { get; set; }

        /// <summary>
        /// The DD number of the driver license.
        /// </summary>
        [JsonPropertyName("dd_number")]
        public StringField DdNumber { get; set; }

        /// <summary>
        /// The expiry date of the driver license.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// The first name of the driver license holder.
        /// </summary>
        [JsonPropertyName("first_name")]
        public StringField FirstName { get; set; }

        /// <summary>
        /// The unique identifier of the driver license.
        /// </summary>
        [JsonPropertyName("id")]
        public StringField Id { get; set; }

        /// <summary>
        /// The date when the driver license was issued.
        /// </summary>
        [JsonPropertyName("issued_date")]
        public DateField IssuedDate { get; set; }

        /// <summary>
        /// The authority that issued the driver license.
        /// </summary>
        [JsonPropertyName("issuing_authority")]
        public StringField IssuingAuthority { get; set; }

        /// <summary>
        /// The last name of the driver license holder.
        /// </summary>
        [JsonPropertyName("last_name")]
        public StringField LastName { get; set; }

        /// <summary>
        /// The Machine Readable Zone (MRZ) of the driver license.
        /// </summary>
        [JsonPropertyName("mrz")]
        public StringField Mrz { get; set; }

        /// <summary>
        /// The place of birth of the driver license holder.
        /// </summary>
        [JsonPropertyName("place_of_birth")]
        public StringField PlaceOfBirth { get; set; }

        /// <summary>
        /// Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
        /// </summary>
        [JsonPropertyName("state")]
        public StringField State { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Country Code: {CountryCode}\n");
            result.Append($":State: {State}\n");
            result.Append($":ID: {Id}\n");
            result.Append($":Category: {Category}\n");
            result.Append($":Last Name: {LastName}\n");
            result.Append($":First Name: {FirstName}\n");
            result.Append($":Date of Birth: {DateOfBirth}\n");
            result.Append($":Place of Birth: {PlaceOfBirth}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":Issued Date: {IssuedDate}\n");
            result.Append($":Issuing Authority: {IssuingAuthority}\n");
            result.Append($":MRZ: {Mrz}\n");
            result.Append($":DD Number: {DdNumber}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
