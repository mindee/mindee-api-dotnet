using System.Collections.Generic;
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


        /// <inheritdoc />
        protected override List<string> BodyLines()
        {
            return ["RAG Documents\n############\n", RagDocuments.ToString()];
        }
    }
}
