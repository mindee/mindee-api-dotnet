using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Mindee.Parsing.Custom;

namespace Mindee.Product.Custom
{
    /// <summary>
    ///     Custm deserializer for <see cref="CustomV1Document" />
    /// </summary>
    public class CustomV1DocumentJsonConverter : JsonConverter<CustomV1Document>
    {
        /// <summary>
        ///     <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)" />
        /// </summary>
        public override CustomV1Document Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            var classificationFields = new Dictionary<string, ClassificationField>();
            var fields = new Dictionary<string, ListField>();

            var jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

            foreach (var jsonNode in jsonObject)
            {
                if (jsonNode.Value.AsObject().ContainsKey("value"))
                {
                    classificationFields.Add(
                        jsonNode.Key,
                        jsonNode.Value.Deserialize<ClassificationField>()
                    );
                }
                else
                {
                    fields.Add(
                        jsonNode.Key,
                        jsonNode.Value.Deserialize<ListField>()
                    );
                }
            }

            return new CustomV1Document { ClassificationFields = classificationFields, Fields = fields };
        }

        /// <summary>
        ///     <see cref="Write(Utf8JsonWriter, CustomV1Document, JsonSerializerOptions)" />
        /// </summary>
        public override void Write(Utf8JsonWriter writer, CustomV1Document value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
