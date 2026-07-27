using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Inference.Field
{
    /// <summary>
    ///     Field having a list of fields.
    /// </summary>
    public class ListField : BaseField
    {
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
                if (field != null)
                {
                    return field;
                }

                field = [];
                foreach (var item in Items.Where(item => item.SimpleField != null))
                {
                    field.Add(item.SimpleField);
                }

                return field;
            }
        }

        /// <summary>
        ///     List of object fields.
        /// </summary>
        public List<ObjectField> ObjectItems
        {
            get
            {
                if (field != null)
                {
                    return field;
                }

                field = [];
                foreach (var item in Items.Where(item => item.ObjectField != null))
                {
                    field.Add(item.ObjectField);
                }

                return field;
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
