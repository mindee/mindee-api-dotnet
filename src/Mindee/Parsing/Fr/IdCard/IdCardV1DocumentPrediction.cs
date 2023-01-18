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
    [Endpoint("idcard_fr", "1")]
    public sealed class IdCardV1DocumentPrediction
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
            StringBuilder result = new StringBuilder("----- FR ID Card V1 -----\n");
            result.Append($"Authority: {Authority.Value}\n");
            result.Append($"Given names: {string.Join(" ", GivenNames.Select(gn => gn.Value))}\n");
            result.Append($"Surname: {Surname.Value}\n");
            result.Append($"Gender: {Gender.Value}\n");
            result.Append($"ID Number: {IdNumber.Value}\n");
            result.Append($"Birth date: {BirthDate.Value}\n");
            result.Append($"Birth place: {BirthPlace.Value}\n");
            result.Append($"Expiry date: {ExpiryDate.Value}\n");
            result.Append($"MRZ 1: {Mrz1.Value}\n");
            result.Append($"MRZ 2: {Mrz2.Value}\n");

            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
