using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Field having a set of sub-fields.
    /// </summary>
    public class ObjectField : BaseField
    {
        /// <summary>
        /// Sub-fields of the field.
        /// </summary>
        [JsonPropertyName("fields")]
        public InferenceFields Fields { get; set; }

        private Dictionary<string, SimpleField> _simpleFields;

        /// <summary>
        /// Simple sub-fields of the field.
        /// </summary>
        public Dictionary<string, SimpleField> SimpleFields
        {
            get
            {
                if (_simpleFields != null)
                    return _simpleFields;
                _simpleFields = new Dictionary<string, SimpleField>();
                foreach (var field in Fields)
                {
                    _simpleFields.Add(field.Key, field.Value.SimpleField);
                }
                return _simpleFields;
            }
        }

        /// <summary>
        /// Object field.
        /// </summary>
        /// <param name="confidence"><see cref="BaseField.Confidence"/></param>
        /// <param name="locations"><see cref="BaseField.Locations"/></param>
        public ObjectField(FieldConfidence? confidence, List<FieldLocation> locations) : base(confidence, locations) { }


        /// <summary>
        /// Print the value for all fields.
        /// </summary>
        public override string ToString()
        {
            return "\n" + (Fields != null ? Fields.ToString(1) : "");
        }

        /// <summary>
        /// Helper to display fields' values when called from a list.
        /// </summary>
        /// <returns>
        /// A left-aligned string representation of <see cref="Fields"/>;
        /// an empty string when <see cref="Fields"/> is <c>null</c>.
        /// </returns>
        public string ToStringFromList()
        {
            if (Fields == null)
            {
                return string.Empty;
            }

            string raw = Fields.ToString(2);
            return raw.Length > 4 ? raw.Substring(4) : raw;
        }
    }
}
