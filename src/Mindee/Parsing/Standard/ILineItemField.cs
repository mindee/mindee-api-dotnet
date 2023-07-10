namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// Represent a single line in a field printable as an rST table.
    /// </summary>
    public interface ILineItemField
    {
        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public string ToTableLine();
    }
}
