using System.Collections.Generic;
using System.Text;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// List of search models.
    /// </summary>
    public class SearchModels : List<SearchModel>
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
            foreach (SearchModel item in this)
            {
                stringBuilder.Append($"* :Name: {item.Name}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :ID: {item.Id}");
                stringBuilder.Append('\n');
                stringBuilder.Append($"  :Model Type: {item.ModelType}");
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }
    }
}
