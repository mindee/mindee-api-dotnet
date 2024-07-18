using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.InternationalId
{
    /// <summary>
    /// International ID API version 2.1 document data.
    /// </summary>
    public class InternationalIdV2Document : IPrediction
    {
        /// <summary>
        /// The physical address of the document holder.
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        /// The date of birth of the document holder.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public DateField BirthDate { get; set; }

        /// <summary>
        /// The place of birth of the document holder.
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// The country where the document was issued.
        /// </summary>
        [JsonPropertyName("country_of_issue")]
        public StringField CountryOfIssue { get; set; }

        /// <summary>
        /// The unique identifier assigned to the document.
        /// </summary>
        [JsonPropertyName("document_number")]
        public StringField DocumentNumber { get; set; }

        /// <summary>
        /// The type of personal identification document.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        /// The date when the document becomes invalid.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// The list of the document holder's given names.
        /// </summary>
        [JsonPropertyName("given_names")]
        public IList<StringField> GivenNames { get; set; } = new List<StringField>();

        /// <summary>
        /// The date when the document was issued.
        /// </summary>
        [JsonPropertyName("issue_date")]
        public DateField IssueDate { get; set; }

        /// <summary>
        /// The Machine Readable Zone, first line.
        /// </summary>
        [JsonPropertyName("mrz_line1")]
        public StringField MrzLine1 { get; set; }

        /// <summary>
        /// The Machine Readable Zone, second line.
        /// </summary>
        [JsonPropertyName("mrz_line2")]
        public StringField MrzLine2 { get; set; }

        /// <summary>
        /// The Machine Readable Zone, third line.
        /// </summary>
        [JsonPropertyName("mrz_line3")]
        public StringField MrzLine3 { get; set; }

        /// <summary>
        /// The country of citizenship of the document holder.
        /// </summary>
        [JsonPropertyName("nationality")]
        public StringField Nationality { get; set; }

        /// <summary>
        /// The unique identifier assigned to the document holder.
        /// </summary>
        [JsonPropertyName("personal_number")]
        public StringField PersonalNumber { get; set; }

        /// <summary>
        /// The biological sex of the document holder.
        /// </summary>
        [JsonPropertyName("sex")]
        public StringField Sex { get; set; }

        /// <summary>
        /// The state or territory where the document was issued.
        /// </summary>
        [JsonPropertyName("state_of_issue")]
        public StringField StateOfIssue { get; set; }

        /// <summary>
        /// The list of the document holder's family names.
        /// </summary>
        [JsonPropertyName("surnames")]
        public IList<StringField> Surnames { get; set; } = new List<StringField>();

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string surnames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 10)),
                Surnames.Select(item => item));
            string givenNames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                GivenNames.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Document Type: {DocumentType}\n");
            result.Append($":Document Number: {DocumentNumber}\n");
            result.Append($":Surnames: {surnames}\n");
            result.Append($":Given Names: {givenNames}\n");
            result.Append($":Sex: {Sex}\n");
            result.Append($":Birth Date: {BirthDate}\n");
            result.Append($":Birth Place: {BirthPlace}\n");
            result.Append($":Nationality: {Nationality}\n");
            result.Append($":Personal Number: {PersonalNumber}\n");
            result.Append($":Country of Issue: {CountryOfIssue}\n");
            result.Append($":State of Issue: {StateOfIssue}\n");
            result.Append($":Issue Date: {IssueDate}\n");
            result.Append($":Expiration Date: {ExpiryDate}\n");
            result.Append($":Address: {Address}\n");
            result.Append($":MRZ Line 1: {MrzLine1}\n");
            result.Append($":MRZ Line 2: {MrzLine2}\n");
            result.Append($":MRZ Line 3: {MrzLine3}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
