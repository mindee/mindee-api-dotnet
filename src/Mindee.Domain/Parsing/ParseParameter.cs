namespace Mindee.Domain.Parsing
{
    public sealed class ParseParameter
    {
        /// <summary>
        /// <see cref="DocumentClient"/>
        /// </summary>
        public DocumentClient DocumentClient { get; }
        
        /// <summary>
        /// To get all the words in the current document.
        /// </summary>
        public bool WithFullText { get; }

        public ParseParameter(DocumentClient documentClient)
            : this(documentClient, false)
        {
        }

        public ParseParameter(DocumentClient documentClient, bool withFullText) 
        {
            DocumentClient = documentClient;
            WithFullText = withFullText;
        }
    }
}
