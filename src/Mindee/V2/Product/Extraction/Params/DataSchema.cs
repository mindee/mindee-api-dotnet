using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mindee.Exceptions;

namespace Mindee.V2.Product.Extraction.Params
{
    /// <summary>
    ///     Modify the Data Schema.
    /// </summary>
    public class DataSchema
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataSchema" /> class from a dictionary.
        /// </summary>
        /// <param name="dataSchema">Dictionary containing the data schema configuration.</param>
        public DataSchema(Dictionary<string, object> dataSchema)
        {
            Setup(dataSchema);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataSchema" /> class from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON string containing the data schema configuration.</param>
        public DataSchema(string jsonString)
        {
            var dataSchema = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
            Setup(dataSchema);
        }

        /// <summary>
        ///     If set, completely replaces the data schema of the model.
        /// </summary>
        [JsonPropertyName("replace")]
        public DataSchemaReplace Replace { get; set; }

        /// <summary>
        ///     Initializes the Replace property.
        /// </summary>
        /// <param name="dataSchema"></param>
        /// <exception cref="MindeeInputException"></exception>
        private void Setup(Dictionary<string, object> dataSchema)
        {
            if (dataSchema == null)
            {
                return;
            }

            if (!dataSchema.TryGetValue("replace", out var value))
            {
                throw new MindeeInputException("Invalid Data Schema format.");
            }

            Replace = JsonSerializer.Deserialize<DataSchemaReplace>(
                JsonSerializer.Serialize(value)
            );
        }

        /// <summary>
        ///     Converts the DataSchema to a JSON string preserving key order.
        /// </summary>
        /// <returns>JSON string representation of the DataSchema.</returns>
        public override string ToString()
        {
            var options =
                new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
