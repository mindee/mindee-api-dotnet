using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mindee.Input
{
    /// <summary>
    ///     Priority for a workflow execution.
    /// </summary>
    public enum ExecutionPriority
    {
        /// <summary>
        ///     Low priority.
        /// </summary>
        [EnumMember(Value = "low")] Low,

        /// <summary>
        ///     Medium priority.
        /// </summary>
        [EnumMember(Value = "medium")] Medium,

        /// <summary>
        ///     Hight priority.
        /// </summary>
        [EnumMember(Value = "high")] High
    }

    /// <summary>
    ///     Deserializer for the ExecutionPriority enum.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        /// <summary>
        ///     Read a JSON value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return Enum.TryParse<T>(value, true, out var result) ? result : default;
        }

        /// <summary>
        ///     Retrieves a JSON value.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLower());
        }
    }
}
