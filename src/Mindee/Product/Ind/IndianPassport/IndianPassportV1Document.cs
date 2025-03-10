using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Ind.IndianPassport
{
    /// <summary>
    /// Passport - India API version 1.2 document data.
    /// </summary>
    public class IndianPassportV1Document : IPrediction
    {
        /// <summary>
        /// The first line of the address of the passport holder.
        /// </summary>
        [JsonPropertyName("address1")]
        public StringField Address1 { get; set; }

        /// <summary>
        /// The second line of the address of the passport holder.
        /// </summary>
        [JsonPropertyName("address2")]
        public StringField Address2 { get; set; }

        /// <summary>
        /// The third line of the address of the passport holder.
        /// </summary>
        [JsonPropertyName("address3")]
        public StringField Address3 { get; set; }

        /// <summary>
        /// The birth date of the passport holder, ISO format: YYYY-MM-DD.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public DateField BirthDate { get; set; }

        /// <summary>
        /// The birth place of the passport holder.
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// ISO 3166-1 alpha-3 country code (3 letters format).
        /// </summary>
        [JsonPropertyName("country")]
        public StringField Country { get; set; }

        /// <summary>
        /// The date when the passport will expire, ISO format: YYYY-MM-DD.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// The file number of the passport document.
        /// </summary>
        [JsonPropertyName("file_number")]
        public StringField FileNumber { get; set; }

        /// <summary>
        /// The gender of the passport holder.
        /// </summary>
        [JsonPropertyName("gender")]
        public ClassificationField Gender { get; set; }

        /// <summary>
        /// The given names of the passport holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public StringField GivenNames { get; set; }

        /// <summary>
        /// The identification number of the passport document.
        /// </summary>
        [JsonPropertyName("id_number")]
        public StringField IdNumber { get; set; }

        /// <summary>
        /// The date when the passport was issued, ISO format: YYYY-MM-DD.
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public DateField IssuanceDate { get; set; }

        /// <summary>
        /// The place where the passport was issued.
        /// </summary>
        [JsonPropertyName("issuance_place")]
        public StringField IssuancePlace { get; set; }

        /// <summary>
        /// The name of the legal guardian of the passport holder (if applicable).
        /// </summary>
        [JsonPropertyName("legal_guardian")]
        public StringField LegalGuardian { get; set; }

        /// <summary>
        /// The first line of the machine-readable zone (MRZ) of the passport document.
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        /// The second line of the machine-readable zone (MRZ) of the passport document.
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// The name of the mother of the passport holder.
        /// </summary>
        [JsonPropertyName("name_of_mother")]
        public StringField NameOfMother { get; set; }

        /// <summary>
        /// The name of the spouse of the passport holder (if applicable).
        /// </summary>
        [JsonPropertyName("name_of_spouse")]
        public StringField NameOfSpouse { get; set; }

        /// <summary>
        /// The date of issue of the old passport (if applicable), ISO format: YYYY-MM-DD.
        /// </summary>
        [JsonPropertyName("old_passport_date_of_issue")]
        public DateField OldPassportDateOfIssue { get; set; }

        /// <summary>
        /// The number of the old passport (if applicable).
        /// </summary>
        [JsonPropertyName("old_passport_number")]
        public StringField OldPassportNumber { get; set; }

        /// <summary>
        /// The place of issue of the old passport (if applicable).
        /// </summary>
        [JsonPropertyName("old_passport_place_of_issue")]
        public StringField OldPassportPlaceOfIssue { get; set; }

        /// <summary>
        /// The page number of the passport document.
        /// </summary>
        [JsonPropertyName("page_number")]
        public ClassificationField PageNumber { get; set; }

        /// <summary>
        /// The surname of the passport holder.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Page Number: {PageNumber}\n");
            result.Append($":Country: {Country}\n");
            result.Append($":ID Number: {IdNumber}\n");
            result.Append($":Given Names: {GivenNames}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Birth Date: {BirthDate}\n");
            result.Append($":Birth Place: {BirthPlace}\n");
            result.Append($":Issuance Place: {IssuancePlace}\n");
            result.Append($":Gender: {Gender}\n");
            result.Append($":Issuance Date: {IssuanceDate}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":MRZ Line 1: {Mrz1}\n");
            result.Append($":MRZ Line 2: {Mrz2}\n");
            result.Append($":Legal Guardian: {LegalGuardian}\n");
            result.Append($":Name of Spouse: {NameOfSpouse}\n");
            result.Append($":Name of Mother: {NameOfMother}\n");
            result.Append($":Old Passport Date of Issue: {OldPassportDateOfIssue}\n");
            result.Append($":Old Passport Number: {OldPassportNumber}\n");
            result.Append($":Address Line 1: {Address1}\n");
            result.Append($":Address Line 2: {Address2}\n");
            result.Append($":Address Line 3: {Address3}\n");
            result.Append($":Old Passport Place of Issue: {OldPassportPlaceOfIssue}\n");
            result.Append($":File Number: {FileNumber}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
