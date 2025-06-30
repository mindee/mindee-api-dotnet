using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Field having a list of fields.
    /// </summary>
    public class ListField
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("items")]
        public List<DynamicField> Items { get; set; } = [];
    }
}
