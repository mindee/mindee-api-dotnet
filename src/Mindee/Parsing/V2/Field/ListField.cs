using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    /// Field having a list of fields.
    /// </summary>
    public class ListField
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        [JsonPropertyName("items")]
        public List<DynamicField> Items { get; set; } = [];

        /// <summary>
        /// Print the value for all items.
        /// </summary>
        public override string ToString()
        {
            if (Items is null || Items.Count == 0)
            {
                return "\n";
            }

            string separator = "\n  * ";
            StringBuilder sb = new();

            sb.Append('\n');
            sb.Append("  * ");

            bool first = true;
            foreach (DynamicField item in Items)
            {
                if (!first)
                {
                    sb.Append(separator);
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
                    sb.Append(item.ObjectField?.ToStringFromList());
                }
                else
                {
                    sb.Append(item);
                }
            }

            return sb.ToString();
        }

    }
}
