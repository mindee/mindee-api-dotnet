using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Ocr result information.
    /// </summary>
    public class Ocr
    {
        /// <summary>
        /// <see cref="Common.MvisionV1"/>
        /// </summary>
        [JsonPropertyName("mvision-v1")]
        public MvisionV1 MvisionV1 { get; set; }
    }

    /// <summary>
    /// List all pages that have ocr results.
    /// </summary>
    public class MvisionV1
    {
        /// <summary>
        /// All the ocr pages.
        /// </summary>
        [JsonPropertyName("pages")]
        public List<Page> Pages { get; set; }
    }

    /// <summary>
    /// Reprensent a page.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// List of words found.
        /// </summary>
        [JsonPropertyName("all_words")]
        public List<AllWord> AllWords { get; set; }
    }

    /// <summary>
    /// Represent a word.
    /// </summary>
    public class AllWord : BaseField
    {
    }
}
