using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.DriverLicense
{
    /// <summary>
    /// Document data for Driver License, API version 1.
    /// </summary>
    public class DriverLicenseV1Document : IPrediction
    {
        /// <summary>
        /// US driver license holders address
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        /// US driver license holders date of birth
        /// </summary>
        [JsonPropertyName("date_of_birth")]
        public DateField DateOfBirth { get; set; }

        /// <summary>
        /// Document Discriminator Number of the US Driver License
        /// </summary>
        [JsonPropertyName("dd_number")]
        public StringField DdNumber { get; set; }

        /// <summary>
        /// US driver license holders class
        /// </summary>
        [JsonPropertyName("dl_class")]
        public StringField DlClass { get; set; }

        /// <summary>
        /// ID number of the US Driver License.
        /// </summary>
        [JsonPropertyName("driver_license_id")]
        public StringField DriverLicenseId { get; set; }

        /// <summary>
        /// US driver license holders endorsements
        /// </summary>
        [JsonPropertyName("endorsements")]
        public StringField Endorsements { get; set; }

        /// <summary>
        /// Date on which the documents expires.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// US driver license holders eye colour
        /// </summary>
        [JsonPropertyName("eye_color")]
        public StringField EyeColor { get; set; }

        /// <summary>
        /// US driver license holders first name(s)
        /// </summary>
        [JsonPropertyName("first_name")]
        public StringField FirstName { get; set; }

        /// <summary>
        /// US driver license holders hair colour
        /// </summary>
        [JsonPropertyName("hair_color")]
        public StringField HairColor { get; set; }

        /// <summary>
        /// US driver license holders hight
        /// </summary>
        [JsonPropertyName("height")]
        public StringField Height { get; set; }

        /// <summary>
        /// Date on which the documents was issued.
        /// </summary>
        [JsonPropertyName("issued_date")]
        public DateField IssuedDate { get; set; }

        /// <summary>
        /// US driver license holders last name
        /// </summary>
        [JsonPropertyName("last_name")]
        public StringField LastName { get; set; }

        /// <summary>
        /// US driver license holders restrictions
        /// </summary>
        [JsonPropertyName("restrictions")]
        public StringField Restrictions { get; set; }

        /// <summary>
        /// US driver license holders gender
        /// </summary>
        [JsonPropertyName("sex")]
        public StringField Sex { get; set; }

        /// <summary>
        /// US State
        /// </summary>
        [JsonPropertyName("state")]
        public StringField State { get; set; }

        /// <summary>
        /// US driver license holders weight
        /// </summary>
        [JsonPropertyName("weight")]
        public StringField Weight { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":State: {State}\n");
            result.Append($":Driver License ID: {DriverLicenseId}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":Date Of Issue: {IssuedDate}\n");
            result.Append($":Last Name: {LastName}\n");
            result.Append($":First Name: {FirstName}\n");
            result.Append($":Address: {Address}\n");
            result.Append($":Date Of Birth: {DateOfBirth}\n");
            result.Append($":Restrictions: {Restrictions}\n");
            result.Append($":Endorsements: {Endorsements}\n");
            result.Append($":Driver License Class: {DlClass}\n");
            result.Append($":Sex: {Sex}\n");
            result.Append($":Height: {Height}\n");
            result.Append($":Weight: {Weight}\n");
            result.Append($":Hair Color: {HairColor}\n");
            result.Append($":Eye Color: {EyeColor}\n");
            result.Append($":Document Discriminator: {DdNumber}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
