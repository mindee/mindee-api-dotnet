using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Input;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Possible field types.
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Simple field.
        /// </summary>
        SimpleField,
        /// <summary>
        /// Object field.
        /// </summary>
        ObjectField,
        /// <summary>
        /// List field.
        /// </summary>
        ListField,
    }

    /// <summary>
    /// Return the field class dynamically.
    /// </summary>
    [JsonConverter(typeof(DynamicFieldJsonConverter))]
    public class DynamicField
    {
        /// <summary>
        /// The type of field.
        /// </summary>
        public FieldType Type;

        /// <summary>
        /// Value as simple field.
        /// </summary>
        public SimpleField SimpleField;

        /// <summary>
        /// Value as list field.
        /// </summary>
        public ListField ListField;

        /// <summary>
        /// Value as object field.
        /// </summary>
        public ObjectField ObjectField;

        /// <summary>
        /// List of the location candidates for the value.
        /// </summary>
        [JsonPropertyName("locations")]
        public List<FieldLocation> Locations { get; set; }

        /// <summary>
        /// Confidence associated with the field.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter<FieldConfidence>))]
        [JsonPropertyName("confidence")]
        public FieldConfidence? Confidence { get; set; }

        /// <summary>
        /// Return the field class dynamically.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="simpleField"></param>
        /// <param name="listField"></param>
        /// <param name="objectField"></param>
        public DynamicField(
            FieldType type,
            SimpleField simpleField = null,
            ListField listField = null,
            ObjectField objectField = null
            )
        {
            Type = type;
            SimpleField = simpleField;
            ListField = listField;
            ObjectField = objectField;
        }

        /// <summary>
        /// String representation of the field.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (SimpleField != null)
                return SimpleField.ToString();
            if (ListField != null)
                return ListField.ToString();
            if (ObjectField != null)
                return ObjectField.ToString();
            return "";
        }
    }
}
