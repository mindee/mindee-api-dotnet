using System.Collections.Generic;

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
    }
}
