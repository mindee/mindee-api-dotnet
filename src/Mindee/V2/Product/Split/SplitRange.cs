using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Split
{
    /// <summary>
    /// A single document as identified when splitting a multi-document source file.
    /// </summary>
    public class SplitRange
    {
        /// <summary>
        /// 0-based page indexes, where the first integer indicates the start page and the second integer indicates the end page.
        /// </summary>
        [JsonPropertyName("page_range")]
        public List<int> PageRange { get; set; }

        /// <summary>
        /// Type or classification of the detected object.
        /// </summary>
        [JsonPropertyName("document_type")]
        public string DocumentType { get; set; }

        /// <summary>
        /// String representation of the split item.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string pageRange = string.Join(",", PageRange);
            return $"* :Page Range: {pageRange}\n  :Document Type: {DocumentType}";
        }
    }
}
