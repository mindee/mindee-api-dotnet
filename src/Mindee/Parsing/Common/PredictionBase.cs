using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public abstract class PredictionBase
    {
        /// <summary>
        /// <see cref="Locale"/>
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }
    }
}
