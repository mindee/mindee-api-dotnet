using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Regroup the common properties on a prediction result.
    /// </summary>
    public abstract class PredictionBase
    {
        /// <summary>
        /// <see cref="Common.Locale"/>
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }
    }
}
