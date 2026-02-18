using Mindee.Input;
using Mindee.Product.Invoice;

namespace Mindee.IntegrationTests.V1.Input
{
    [Trait("Category", "V1")]
    [Trait("Category", "Send URL")]
    public class UrlInputSourceTest
    {
        [Fact(Timeout = 180000)]
        public async Task GivenARemoteFile_MustRetrieveResponse()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var blankUrl = Environment.GetEnvironmentVariable("MindeeV2__Se__Tests__Blank__Pdf__Url");
            var client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
            var remoteInput =
                new UrlInputSource(
                    blankUrl);
            var localInput = await remoteInput.AsLocalInputSource();
            Assert.Equal("blank_1.pdf", localInput.Filename);
            var result = await client.ParseAsync<InvoiceV4>(localInput);
            Assert.Equal(1, result.Document.NPages);
        }
    }
}
