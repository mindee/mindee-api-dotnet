using System.Collections.Generic;
using System.Text;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// List of search models.
    /// </summary>
    public class RagDocuments : List<RagDocument>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (RagDocument item in this)
            {
                stringBuilder.Append($"  :ID: {item.Id}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"* :Model ID: {item.ModelId}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :Filename: {item.Filename}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :Created At: {item.CreatedAt}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :Total Matches: {item.TotalMatches}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :Last Match At: {item.LastMatchAt}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :Status: {item.Status}");
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }
    }
}
