using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    ///     Field having a list of fields.
    /// </summary>
    public class ListField : BaseField
    {
        private List<ObjectField> _objectItems;

        private List<SimpleField> _simpleItems;

        /// <summary>
        ///     List field.
        /// </summary>
        /// <param name="confidence">
        ///     <see cref="BaseField.Confidence" />
        /// </param>
        public ListField(FieldConfidence? confidence) : base(confidence, null) { }

        /// <summary>
        ///     Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("items")]
        public List<DynamicField> Items { get; set; } = [];

        /// <summary>
        ///     List of simple fields.
        /// </summary>
        public List<SimpleField> SimpleItems
        {
            get
            {
                if (_simpleItems != null)
                {
                    return _simpleItems;
                }

                _simpleItems = new List<SimpleField>();
                foreach (var item in Items)
                {
                    if (item.SimpleField != null)
                    {
                        _simpleItems.Add(item.SimpleField);
                    }
                }

                return _simpleItems;
            }
        }

        /// <summary>
        ///     List of object fields.
        /// </summary>
        public List<ObjectField> ObjectItems
        {
            get
            {
                if (_objectItems != null)
                {
                    return _objectItems;
                }

                _objectItems = [];
                foreach (var item in Items.Where(item => item.ObjectField != null))
                {
                    _objectItems.Add(item.ObjectField);
                }

                return _objectItems;
            }
        }

        /// <summary>
        ///     Print the value for all items.
        /// </summary>
        public override string ToString()
        {
            if (Items is null || Items.Count == 0)
            {
                return "\n";
            }

            const string separator = "\n  * ";
            StringBuilder joiner = new();

            joiner.Append('\n');
            joiner.Append("  * ");

            var first = true;
            foreach (var item in Items)
            {
                if (!first)
                {
                    joiner.Append(separator);
                }
                else
                {
                    first = false;
                }

                if (item is null)
                {
                    continue;
                }

                if (item.Type == FieldType.ObjectField)
                {
                    joiner.Append(item.ObjectField?.ToStringFromList());
                }
                else
                {
                    joiner.Append(item);
                }
            }

            return joiner.ToString();
        }
    }
}
