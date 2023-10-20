using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Represent a single line in a field printable as an rST table.
    /// </summary>
    public abstract class LineItemField : BaseField
    {
        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public abstract string ToTableLine();
    }
}
