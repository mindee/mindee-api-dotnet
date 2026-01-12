using System.Collections.Generic;
using System.Text;

namespace Mindee.Parsing.Custom.LineItem
{
    /// <summary>
    ///     Table equivalent.
    /// </summary>
    public class LineItems
    {
        /// <summary>
        /// </summary>
        /// <param name="lines">
        ///     <see cref="Lines" />
        /// </param>
        /// ///
        /// <param name="fieldNames">
        ///     <see cref="FieldNames" />
        /// </param>
        public LineItems(IEnumerable<Line> lines, List<string> fieldNames)
        {
            Lines = lines;
            FieldNames = fieldNames;
        }

        /// <summary>
        ///     All the lines.
        /// </summary>
        public IEnumerable<Line> Lines { get; }

        /// <summary>
        ///     List of field names used to construct the line items.
        /// </summary>
        public List<string> FieldNames { get; }

        /// <summary>
        ///     Output the line items as an RST Table.
        /// </summary>
        public string ToRst(string title)
        {
            var result = new StringBuilder($"\n:{title}:\n");

            var header = new StringBuilder();
            var columnNames = new StringBuilder();
            foreach (var fieldName in FieldNames)
            {
                header.Append(new string('=', fieldName.Length) + " ");
                columnNames.Append($"{fieldName.PadRight(fieldName.Length)} ");
            }

            result.Append(header + "\n");
            result.Append(columnNames + "\n");
            result.Append(header + "\n");

            foreach (var line in Lines)
            {
                foreach (var fieldName in FieldNames)
                {
                    ListFieldValue field;
                    var fieldValue = line.Fields.TryGetValue(fieldName, out field) ? field.ToString() : "";
                    result.Append(fieldValue.PadRight(fieldName.Length + 1));
                }

                result.Append('\n');
            }

            result.Append(header + "\n");

            return SummaryHelper.Clean(result.ToString());
        }

        /// <summary>
        ///     Prettier representation.
        /// </summary>
        public override string ToString()
        {
            return ToRst("Line Items");
        }
    }
}
