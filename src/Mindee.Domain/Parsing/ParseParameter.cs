namespace Mindee.Domain.Parsing
{
    public sealed class ParseParameter
    {
        public DocumentClient DocumentClient { get; }

        public ParseParameter(DocumentClient documentClient)
        {
            DocumentClient = documentClient;
        }
    }
}
