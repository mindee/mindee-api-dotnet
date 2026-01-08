using Mindee.Input;
using Mindee.Product.Invoice;

namespace Mindee.IntegrationTests.V1.Input
{
    [Trait("Category", "V1")]
    [Trait("Category", "Send URL")]
    public class UrlInputSourceTest
    {
        [Fact]
        public async Task GivenARemoteFile_MustRetrieveResponse()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
            var remoteInput =
                new UrlInputSource(
                    "https://github.com/mindee/client-lib-test-data/blob/main/v1/products/invoice_splitter/invoice_5p.pdf?raw=true");
            var localInput = await remoteInput.AsLocalInputSource();
            Assert.Equal("invoice_5p.pdf", localInput.Filename);
            var result = await client.ParseAsync<InvoiceV4>(localInput);
            Assert.Equal(5, result.Document.NPages);
        }
    }
}
