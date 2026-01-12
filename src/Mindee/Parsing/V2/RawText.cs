using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    ///     Raw text extracted from the document.
    /// </summary>
    public class RawText
    {
        /// <summary>
        ///     Pages of raw text content.
        /// </summary>
        [JsonPropertyName("pages")]
        public List<RawTextPage> Pages { get; set; }

        /// <summary>
        ///     Text content of all pages.
        ///     Each page is separated by 2 newline characters.
        /// </summary>
        public override string ToString()
        {
            if (Pages == null || Pages.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            foreach (var page in Pages)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append("\n\n");
                }

                stringBuilder.Append(page);
            }

            stringBuilder.Append("\n");
            return stringBuilder.ToString();
        }
    }
}
