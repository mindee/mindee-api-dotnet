using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Field having a list of fields.
    /// </summary>
    public class ListField : BaseField
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("items")]
        public List<DynamicField> Items { get; set; } = [];

        /// <summary>
        /// List field.
        /// </summary>
        /// <param name="confidence"><see cref="BaseField.Confidence"/></param>
        public ListField(FieldConfidence? confidence) : base(confidence, null) { }

        private List<SimpleField> _simpleItems;

        /// <summary>
        /// List of simple fields.
        /// </summary>
        public List<SimpleField> SimpleItems
        {
            get
            {
                if (_simpleItems != null)
                    return _simpleItems;
                _simpleItems = new List<SimpleField>();
                foreach (DynamicField item in Items)
                {
                    if (item.SimpleField != null)
                        _simpleItems.Add(item.SimpleField);
                }
                return _simpleItems;
            }
        }

        private List<ObjectField> _objectItems;

        /// <summary>
        /// List of object fields.
        /// </summary>
        public List<ObjectField> ObjectItems
        {
            get
            {
                if (_objectItems != null)
                    return _objectItems;
                _objectItems = new List<ObjectField>();
                foreach (DynamicField item in Items)
                {
                    if (item.ObjectField != null)
                        _objectItems.Add(item.ObjectField);
                }
                return _objectItems;
            }
        }

        /// <summary>
        /// Print the value for all items.
        /// </summary>
        public override string ToString()
        {
            if (Items is null || Items.Count == 0)
                return "\n";

            string separator = "\n  * ";
            StringBuilder sb = new();

            sb.Append('\n');
            sb.Append("  * ");

            bool first = true;
            foreach (DynamicField item in Items)
            {
                if (!first)
                    sb.Append(separator);
                else
                    first = false;

                if (item is null)
                    continue;
                if (item.Type == FieldType.ObjectField)
                    sb.Append(item.ObjectField?.ToStringFromList());
                else
                    sb.Append(item);
            }

            return sb.ToString();
        }

    }
}
