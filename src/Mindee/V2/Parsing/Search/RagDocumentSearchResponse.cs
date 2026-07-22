using System;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Models search response.
    /// </summary>
    public class RagDocumentSearchResponse : BaseSearchResponse
    {
        /// <summary>
        /// Paginated list of matching RAG documents.
        /// </summary>
        [JsonPropertyName("rag_documents")]
        [JsonConverter(typeof(ObjectListJsonConverter<RagDocuments, RagDocument>))]
        public RagDocuments RagDocuments { get; set; }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder("RAG Documents\n############\n");
            stringBuilder.Append(RagDocuments);
            return ToString(stringBuilder);
        }
    }
}
