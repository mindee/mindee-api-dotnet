using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindee.Extensions.DependencyInjection;
using Mindee.Input;
using Mindee.Product.Invoice;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "DI")]
    public class DependencyInjectionTest : IDisposable
    {
        private readonly IHost _host;
        private readonly IServiceProvider _services;

        public DependencyInjectionTest()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string>());
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMindeeClient();
                });

            _host = builder.Build();
            _services = _host.Services;
        }

        [Fact]
        public async Task ShouldMaintainAuthenticationAcrossMultipleRequests()
        {
            var mindeeClient1 = _services.GetRequiredService<MindeeClient>();
            var inputSource1 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response1 = await mindeeClient1.ParseAsync<InvoiceV4>(inputSource1);
            Assert.NotNull(response1);
            Assert.True(response1.Document != null, "First request should return a valid document");

            var mindeeClient2 = _services.GetRequiredService<MindeeClient>();
            var inputSource2 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response2 = await mindeeClient2.ParseAsync<InvoiceV4>(inputSource2);
            Assert.NotNull(response2);
            Assert.True(response2.Document != null, "Second request should return a valid document");

            var inputSource3 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response3 = await mindeeClient1.ParseAsync<InvoiceV4>(inputSource3);
            Assert.NotNull(response3);
            Assert.True(response3.Document != null, "Third request should return a valid document");
        }

        public void Dispose()
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
    }

}
