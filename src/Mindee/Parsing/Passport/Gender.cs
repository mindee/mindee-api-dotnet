using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Passport
{
    public class Gender : BaseField
    {
        /// <summary>
        /// The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
