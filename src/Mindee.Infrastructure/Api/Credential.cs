namespace Mindee.Infrastructure.Api
{
    internal class Credential
    {
        public string ProductName { get; }
        public string Version { get; }

        public Credential(string productName, string version)
        {
            ProductName = productName;
            Version = version;
        }
    }
}
