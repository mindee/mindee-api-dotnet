using System;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Data schema options activated during the inference.
    /// </summary>
    public class DataSchemaActiveOptions
    {
        /// <summary>
        /// Whether the Data Schema has been replaced.
        /// </summary>
        [JsonPropertyName("replace")]
        public bool Replace { get; set; }

        /// <summary>
        /// String representation override.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Data Schema\n-----------\n:Replace: {Replace}";
        }
    }
}
