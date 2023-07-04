
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.CarteVitale
{
    /// <summary>
    /// Document data for Carte Vitale, API version 1.
    /// </summary>
    public class CarteVitaleV1Document
    {
        /// <summary>
        /// The given name(s) of the card holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public List<StringField> GivenNames { get; set; }

        /// <summary>
        /// The surname of the card holder.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

        /// <summary>
        /// The Social Security Number (Numéro de Sécurité Sociale) of the card holder
        /// </summary>
        [JsonPropertyName("social_security")]
        public StringField SocialSecurity { get; set; }

        /// <summary>
        /// The date the card was issued.
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public DateField IssuanceDate { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string givenNames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                GivenNames.Select(item => item));

            StringBuilder result = new StringBuilder();
            result.Append($":Given Name(s): {givenNames}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Social Security Number: {SocialSecurity}\n");
            result.Append($":Issuance Date: {IssuanceDate}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
