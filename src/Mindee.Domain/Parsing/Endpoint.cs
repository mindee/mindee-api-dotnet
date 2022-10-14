namespace Mindee.Domain.Parsing
{
    internal class Endpoint
    {
        public string ProductName { get; }
        public string Version { get; }

        public Endpoint(string productName, string version)
        {
            ProductName = productName;
            Version = version;
        }
    }
}
