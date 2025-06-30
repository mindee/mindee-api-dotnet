using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Field having a set of sub-fields.
    /// </summary>
    public class ObjectField : BaseField
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("fields")]
        public Dictionary<string, SimpleField> Fields { get; set; }
    }
}
