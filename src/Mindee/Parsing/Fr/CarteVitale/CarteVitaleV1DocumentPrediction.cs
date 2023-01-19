using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Fr.IdCard
{
    /// <summary>
    /// The french carte vitale model for the v1.
    /// </summary>
    public class CarteVitaleV1DocumentPrediction
    {
        /// <summary>
        /// The list of the names of the person.
        /// </summary>
        [JsonPropertyName("given_names")]
        public List<StringField> GivenNames { get; set; }

        /// <summary>
        /// The surname of the person.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

        /// <summary>
        /// The social security number.
        /// </summary>
        [JsonPropertyName("social_security")]
        public StringField SocialSecurityNumber { get; set; }

        /// <summary>
        /// The date of issuance of it.
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public StringField IssuanceDate { get; set; }

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Given names: {string.Join(" ", GivenNames.Select(gn => gn))}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Social Security Number: {SocialSecurityNumber}\n");
            result.Append($":Issuance date: {IssuanceDate}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
