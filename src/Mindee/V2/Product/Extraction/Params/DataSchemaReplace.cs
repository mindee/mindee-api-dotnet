using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Extraction.Params
{
    /// <summary>
    ///     The structure to completely replace the data schema of the model.
    /// </summary>
    public class DataSchemaReplace
    {
        /// <summary>
        ///     List of fields in the Data Schema.
        /// </summary>
        [JsonPropertyName("fields")]
        public List<DataSchemaField> Fields { get; set; }

        /// <summary>
        ///     Converts the DataSchemaReplace to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchemaReplace.</returns>
        public override string ToString()
        {
            var options =
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
