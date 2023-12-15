using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.InternationalId
{
    /// <summary>
    /// Document data for International ID, API version 1.
    /// </summary>
    public class InternationalIdV1Document : IPrediction
    {
        /// <summary>
        /// The physical location of the document holder's residence.
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        /// The date of birth of the document holder.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public StringField BirthDate { get; set; }

        /// <summary>
        /// The location where the document holder was born.
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// The country that issued the identification document.
        /// </summary>
        [JsonPropertyName("country_of_issue")]
        public StringField CountryOfIssue { get; set; }

        /// <summary>
        /// The unique identifier assigned to the identification document.
        /// </summary>
        [JsonPropertyName("document_number")]
        public StringField DocumentNumber { get; set; }

        /// <summary>
        /// The type of identification document being used.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        /// The date when the document will no longer be valid for use.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public StringField ExpiryDate { get; set; }

        /// <summary>
        /// The first names or given names of the document holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public IList<StringField> GivenNames { get; set; } = new List<StringField>();

        /// <summary>
        /// The date when the document was issued.
        /// </summary>
        [JsonPropertyName("issue_date")]
        public StringField IssueDate { get; set; }

        /// <summary>
        /// First line of information in a standardized format for easy machine reading and processing.
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        /// Second line of information in a standardized format for easy machine reading and processing.
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// Third line of information in a standardized format for easy machine reading and processing.
        /// </summary>
        [JsonPropertyName("mrz3")]
        public StringField Mrz3 { get; set; }

        /// <summary>
        /// Indicates the country of citizenship or nationality of the document holder.
        /// </summary>
        [JsonPropertyName("nationality")]
        public StringField Nationality { get; set; }

        /// <summary>
        /// The document holder's biological sex, such as male or female.
        /// </summary>
        [JsonPropertyName("sex")]
        public StringField Sex { get; set; }

        /// <summary>
        /// The surnames of the document holder.
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
            result.Append($":Country of Issue: {CountryOfIssue}\n");
            result.Append($":Surnames: {surnames}\n");
            result.Append($":Given Names: {givenNames}\n");
            result.Append($":Gender: {Sex}\n");
            result.Append($":Birth date: {BirthDate}\n");
            result.Append($":Birth Place: {BirthPlace}\n");
            result.Append($":Nationality: {Nationality}\n");
            result.Append($":Issue Date: {IssueDate}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":Address: {Address}\n");
            result.Append($":Machine Readable Zone Line 1: {Mrz1}\n");
            result.Append($":Machine Readable Zone Line 2: {Mrz2}\n");
            result.Append($":Machine Readable Zone Line 3: {Mrz3}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
