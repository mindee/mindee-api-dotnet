using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.CustomBuilder
{
    /// <summary>
    /// Custm deserializer for <see cref="CustomV1DocumentPrediction"/>
    /// </summary>
    public class CustomV1DocumentPredictionJsonConverter : JsonConverter<CustomV1DocumentPrediction>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override CustomV1DocumentPrediction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var classificationFields = new Dictionary<string, ClassificationField>();
            var fields = new Dictionary<string, ListField>();

            JsonObject jsonObject = (JsonObject)JsonSerializer.Deserialize(ref reader, typeof(JsonObject), options);

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

            return new CustomV1DocumentPrediction()
            {
                ClassificationFields = classificationFields,
                Fields = fields
            };
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, CustomV1DocumentPrediction, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, CustomV1DocumentPrediction value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
