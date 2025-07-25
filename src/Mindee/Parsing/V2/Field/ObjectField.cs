using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
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
        public InferenceFields Fields { get; set; }

        /// <summary>
        /// Print the value for all fields.
        /// </summary>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (KeyValuePair<string, DynamicField> fieldPair in Fields)
            {
                output.Append($"{fieldPair.Key}: {fieldPair.Value}\n");
            }
            return output.ToString();
        }
    }
}
