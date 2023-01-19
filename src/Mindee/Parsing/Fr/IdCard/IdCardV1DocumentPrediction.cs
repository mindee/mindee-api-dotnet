using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// The french id card model for the v1.
    /// </summary>
    public class IdCardV1DocumentPrediction
    {
        /// <summary>
        /// The authority which has issued the card.
        /// </summary>
        [JsonPropertyName("authority")]
        public StringField Authority { get; set; }

        /// <summary>
        /// The birth date of the person.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public StringField BirthDate { get; set; }

        /// <summary>
        /// The birth place of the person.
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// The expiry date of the card.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public StringField ExpiryDate { get; set; }

        /// <summary>
        /// The gender of the person.
        /// </summary>
        [JsonPropertyName("gender")]
        public StringField Gender { get; set; }

        /// <summary>
        /// The list of the names of the person.
        /// </summary>
        [JsonPropertyName("given_names")]
        public List<StringField> GivenNames { get; set; }

        /// <summary>
        /// The id number of the card.
        /// </summary>
        [JsonPropertyName("id_number")]
        public StringField IdNumber { get; set; }

        /// <summary>
        /// The first mrz value.
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        /// The second mrz value.
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// The surname of the person.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Authority: {Authority}\n");
            result.Append($":Given names: {string.Join(" ", GivenNames.Select(gn => gn))}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Gender: {Gender}\n");
            result.Append($":ID Number: {IdNumber}\n");
            result.Append($":Birth date: {BirthDate}\n");
            result.Append($":Birth place: {BirthPlace}\n");
            result.Append($":Expiry date: {ExpiryDate}\n");
            result.Append($":MRZ 1: {Mrz1}\n");
            result.Append($":MRZ 2: {Mrz2}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
