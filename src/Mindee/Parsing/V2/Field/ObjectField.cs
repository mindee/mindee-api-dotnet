using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    ///     Field having a set of sub-fields.
    /// </summary>
    public class ObjectField : BaseField
    {
        private Dictionary<string, SimpleField> _simpleFields;
        private Dictionary<string, ListField> _listFields;
        private Dictionary<string, ObjectField> _objectFields;

        /// <summary>
        ///     Object field.
        /// </summary>
        /// <param name="confidence">
        ///     <see cref="BaseField.Confidence" />
        /// </param>
        /// <param name="locations">
        ///     <see cref="BaseField.Locations" />
        /// </param>
        public ObjectField(FieldConfidence? confidence, List<FieldLocation> locations) : base(confidence, locations) { }

        /// <summary>
        ///     Sub-fields of the field.
        /// </summary>
        [JsonPropertyName("fields")]
        public InferenceFields Fields { get; set; }

        /// <summary>
        ///     Simple sub-fields of the field.
        /// </summary>
        public Dictionary<string, SimpleField> SimpleFields
        {
            get
            {
                if (this._simpleFields != null) return this._simpleFields;

                this._simpleFields = new Dictionary<string, SimpleField>();
                foreach (var currentField in Fields)
                {
                    // Strict Check: Only add if the Type says it's a SimpleField
                    if (currentField.Value.Type == FieldType.SimpleField && currentField.Value.SimpleField != null)
                    {
                        this._simpleFields.Add(currentField.Key, currentField.Value.SimpleField);
                    }
                }
                return this._simpleFields;
            }
        }

        /// <summary>
        ///     List sub-fields of the field.
        /// </summary>
        public Dictionary<string, ListField> ListFields
        {
            get
            {
                if (this._listFields != null) return this._listFields;

                this._listFields = new Dictionary<string, ListField>();
                foreach (var currentField in Fields)
                {
                    if (currentField.Value.Type == FieldType.ListField && currentField.Value.ListField != null)
                    {
                        this._listFields.Add(currentField.Key, currentField.Value.ListField);
                    }
                }
                return this._listFields;
            }
        }

        /// <summary>
        ///     Object sub-fields of the field.
        /// </summary>
        public Dictionary<string, ObjectField> ObjectFields
        {
            get
            {
                if (this._objectFields != null) return this._objectFields;

                this._objectFields = new Dictionary<string, ObjectField>();
                foreach (var currentField in Fields)
                {
                    if (currentField.Value.Type == FieldType.ObjectField && currentField.Value.ObjectField != null)
                    {
                        this._objectFields.Add(currentField.Key, currentField.Value.ObjectField);
                    }
                }
                return this._objectFields;
            }
        }

        /// <summary>
        /// Retrieves a sub-field from the fields object as a SimpleField.
        /// </summary>
        /// <param name="fieldName">Name of the field to retrieve.</param>
        /// <returns>A SimpleField instance, if found.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public SimpleField GetSimpleField(string fieldName) => SimpleFields.TryGetValue(fieldName, out var value)
            ? value
            : throw new KeyNotFoundException();

        /// <summary>
        /// Retrieves a sub-field from the fields object as a ListField.
        /// </summary>
        /// <param name="fieldName">Name of the field to retrieve.</param>
        /// <returns>A ListField instance, if found.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public ListField GetListField(string fieldName) => ListFields.TryGetValue(fieldName, out var value)
            ? value
            : throw new KeyNotFoundException();

        /// <summary>
        /// Retrieves a sub-field from the fields object as an ObjectField.
        /// </summary>
        /// <param name="fieldName">Name of the field to retrieve.</param>
        /// <returns>An ObjectField instance, if found.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public ObjectField GetObjectField(string fieldName) => ObjectFields.TryGetValue(fieldName, out var value)
            ? value
            : throw new KeyNotFoundException();

        /// <summary>
        ///     Print the value for all fields.
        /// </summary>
        public override string ToString()
        {
            return "\n" + (Fields != null ? Fields.ToString(1) : "");
        }

        /// <summary>
        ///     Helper to display fields' values when called from a list.
        /// </summary>
        /// <returns>
        ///     A left-aligned string representation of <see cref="Fields" />;
        ///     an empty string when <see cref="Fields" /> is <c>null</c>.
        /// </returns>
        public string ToStringFromList()
        {
            if (Fields == null)
            {
                return string.Empty;
            }

            var raw = Fields.ToString(2);
            return raw.Length > 4 ? raw.Substring(4) : raw;
        }
    }
}
