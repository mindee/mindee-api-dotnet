using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mindee.Geometry
{
    /// <summary>
    /// Custom de-serialize for <see cref="Polygon"/>
    /// </summary>
    public class PolygonJsonConverter : JsonConverter<Polygon>
    {
        /// <summary>
        /// <see cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        /// </summary>
        public override Polygon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var points = JsonSerializer.Deserialize<List<List<double>>>(ref reader, options);
            if (points.Count > 0)
            {
                return new Polygon(points);
            }
            return null;
        }

        /// <summary>
        /// <see cref="Write(Utf8JsonWriter, Polygon, JsonSerializerOptions)"/>
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Polygon value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToJsonString());
        }
    }
}
