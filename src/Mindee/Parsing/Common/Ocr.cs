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
        public List<Word> AllWords { get; set; }
    }

    /// <summary>
    /// Represent a word.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Define the coordinates of the zone in the page where the values has been found.
        /// </summary>
        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        /// <summary>
        /// Represent the content.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
