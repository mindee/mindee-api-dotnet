namespace Mindee.Parsing
{
    internal class Endpoint
    {
        public string ProductName { get; }
        public string Version { get; }
        public string OrganizationName { get; }

        public Endpoint(
            string productName
            , string version
            , string organizationName = "mindee")
        {
            ProductName = productName;
            Version = version;
            OrganizationName = organizationName;
        }
    }
}
