using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.CustomBuilder.Table
{
    /// <summary>
    /// Table equivalent.
    /// </summary>
    public class LineItems
    {
        /// <summary>
        /// All the lines.
        /// </summary>
        public IEnumerable<Line> Rows { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="rows"><see cref="Rows"/></param>
        public LineItems(IEnumerable<Line> rows)
        {
            Rows = rows;
        }

        /// <summary>
        /// Prettier representation.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("\n:Table:\n");

            if (Rows.Any())
            {
                var header = new StringBuilder();
                var columnNames = new StringBuilder();
                foreach (var columnName in Rows
                    .First(r => r.Fields.Count == Rows.Max(r => r.Fields.Count))
                        .Fields.Keys)
                {
                    header.Append(new string('=', columnName.Length) + " ");
                    columnNames.Append($"{columnName.PadRight(columnName.Length)} ");
                }

                result.Append(header.ToString() + "\n");
                result.Append(columnNames.ToString() + "\n");
                result.Append(header.ToString() + "\n");

                result.Append(string.Join("\n", Rows.Select(line => line.ToString())));

                result.Append("\n" + header.ToString() + "\n");
            }

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
