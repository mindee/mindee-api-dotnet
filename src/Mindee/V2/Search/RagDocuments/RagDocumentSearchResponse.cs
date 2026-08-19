using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.V2.Parsing.Search;
using Mindee.V2.Product;

namespace Mindee.V2.Search.RagDocuments
{
    /// <summary>
    /// Models search response.
    /// </summary>
    [ProductAttributes("rag-documents")]
    public class RagDocumentSearchResponse : BaseSearchResponse
    {
        /// <summary>
        /// Paginated list of matching RAG documents.
        /// </summary>
        [JsonPropertyName("rag_documents")]
        [JsonConverter(typeof(ObjectListJsonConverter<SearchRagDocuments, SearchRagDocument>))]
        public SearchRagDocuments RagDocuments { get; set; }

        /// <inheritdoc />
        protected override List<string> BodyLines()
        {
            return ["RAG Documents\n############\n", RagDocuments.ToString()];
        }
    }
}
