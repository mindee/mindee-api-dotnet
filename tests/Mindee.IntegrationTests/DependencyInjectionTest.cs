using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string>()!);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMindeeClient();
                    services.AddMindeeClientV2();
                });

            _host = builder.Build();
            _services = _host.Services;
        }

        [Fact]
        public void ShouldInitBothClients()
        {
            MindeeClient clientV1 = _services.GetRequiredService<MindeeClient>();
            Assert.NotNull(clientV1);
            MindeeClientV2 clientV2 = _services.GetRequiredService<MindeeClientV2>();
            Assert.NotNull(clientV2);
        }

        [Fact]
        public async Task ShouldMaintainAuthenticationAcrossMultipleRequests()
        {
            MindeeClient instance1ClientV1 = _services.GetRequiredService<MindeeClient>();
            var inputSource1 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response1 = await instance1ClientV1.ParseAsync<InvoiceV4>(inputSource1);
            Assert.NotNull(response1);
            Assert.True(response1.Document != null, "First V1 request should return a valid document");

            MindeeClient instance2ClientV1 = _services.GetRequiredService<MindeeClient>();
            var inputSource2 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response2 = await instance2ClientV1.ParseAsync<InvoiceV4>(inputSource2);
            Assert.NotNull(response2);
            Assert.True(response2.Document != null, "Second V1 request should return a valid document");

            MindeeClientV2 instance1ClientV2 = _services.GetRequiredService<MindeeClientV2>();
            var inputSource3 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response3 = await instance1ClientV2.EnqueueInferenceAsync(
                inputSource3,
                new InferenceParameters(modelId: Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id")));
            Assert.NotNull(response3);
            Assert.True(response3.Job != null, "First V2 request should return a valid job");

            var inputSource4 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response4 = await instance1ClientV1.ParseAsync<InvoiceV4>(inputSource4);
            Assert.NotNull(response4);
            Assert.True(response4.Document != null, "Third V3 request should return a valid document");

            MindeeClientV2 instance2ClientV2 = _services.GetRequiredService<MindeeClientV2>();
            var inputSource5 = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response5 = await instance2ClientV2.EnqueueInferenceAsync(
                inputSource5,
                new InferenceParameters(modelId: Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id")));
            Assert.NotNull(response5);
            Assert.True(response5.Job != null, "Second V2 request should return a valid job");
        }

        public void Dispose()
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
    }

}
