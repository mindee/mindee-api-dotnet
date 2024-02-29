using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.W9
{
    /// <summary>
    /// Page data for W9, API version 1.
    /// </summary>
    public sealed class W9V1Page : W9V1Document
    {
        /// <summary>
        /// The street address (number, street, and apt. or suite no.) of the applicant.
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        /// The business name or disregarded entity name, if different from Name.
        /// </summary>
        [JsonPropertyName("business_name")]
        public StringField BusinessName { get; set; }

        /// <summary>
        /// The city, state, and ZIP code of the applicant.
        /// </summary>
        [JsonPropertyName("city_state_zip")]
        public StringField CityStateZip { get; set; }

        /// <summary>
        /// The employer identification number.
        /// </summary>
        [JsonPropertyName("ein")]
        public StringField Ein { get; set; }

        /// <summary>
        /// Name as shown on the applicant's income tax return.
        /// </summary>
        [JsonPropertyName("name")]
        public StringField Name { get; set; }

        /// <summary>
        /// Position of the signature date on the document.
        /// </summary>
        [JsonPropertyName("signature_date_position")]
        public PositionField SignatureDatePosition { get; set; }

        /// <summary>
        /// Position of the signature on the document.
        /// </summary>
        [JsonPropertyName("signature_position")]
        public PositionField SignaturePosition { get; set; }

        /// <summary>
        /// The applicant's social security number.
        /// </summary>
        [JsonPropertyName("ssn")]
        public StringField Ssn { get; set; }

        /// <summary>
        /// The federal tax classification, which can vary depending on the revision date.
        /// </summary>
        [JsonPropertyName("tax_classification")]
        public StringField TaxClassification { get; set; }

        /// <summary>
        /// Depending on revision year, among S, C, P or D for Limited Liability Company Classification.
        /// </summary>
        [JsonPropertyName("tax_classification_llc")]
        public StringField TaxClassificationLlc { get; set; }

        /// <summary>
        /// Tax Classification Other Details.
        /// </summary>
        [JsonPropertyName("tax_classification_other_details")]
        public StringField TaxClassificationOtherDetails { get; set; }

        /// <summary>
        /// The Revision month and year of the W9 form.
        /// </summary>
        [JsonPropertyName("w9_revision_date")]
        public StringField W9RevisionDate { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Name: {Name}\n");
            result.Append($":SSN: {Ssn}\n");
            result.Append($":Address: {Address}\n");
            result.Append($":City State Zip: {CityStateZip}\n");
            result.Append($":Business Name: {BusinessName}\n");
            result.Append($":EIN: {Ein}\n");
            result.Append($":Tax Classification: {TaxClassification}\n");
            result.Append($":Tax Classification Other Details: {TaxClassificationOtherDetails}\n");
            result.Append($":W9 Revision Date: {W9RevisionDate}\n");
            result.Append($":Signature Position: {SignaturePosition}\n");
            result.Append($":Signature Date Position: {SignatureDatePosition}\n");
            result.Append($":Tax Classification LLC: {TaxClassificationLlc}\n");
            result.Append(base.ToString());
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
