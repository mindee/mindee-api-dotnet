using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
// ReSharper disable once RedundantUsingDirective
using Mindee.Extensions.DependencyInjection;
using Mindee.Input;
using Mindee.V1.Product.Invoice;
using Mindee.V2.Product.Extraction;
using Mindee.V2.Product.Extraction.Params;
using Client = Mindee.V2.Client;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "DI")]
    public class DependencyInjectionTest : IAsyncLifetime
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
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string?>());
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddMindeeClient();
                    services.AddMindeeClientV2();
                });

            _host = builder.Build();
            _services = _host.Services;
        }
        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            try
            {
                await _host.StopAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("DependencyInjectionTest teardown: StopAsync timed out after 120s.");
            }
            _host.Dispose();
        }


        [Fact]
        public void ShouldInitBothClients()
        {
            var clientV1 = _services.GetRequiredService<Mindee.V1.Client>();
            Assert.NotNull(clientV1);
            var clientV2 = _services.GetRequiredService<Client>();
            Assert.NotNull(clientV2);
        }

        [Fact(Timeout = 180000)]
        public async Task ShouldMaintainAuthenticationAcrossMultipleRequests()
        {
            var instance1ClientV1 = _services.GetRequiredService<Mindee.V1.Client>();
            var inputSource1 = new LocalInputSource(new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var response1 = await instance1ClientV1.ParseAsync<InvoiceV4>(inputSource1);
            Assert.NotNull(response1);
            Assert.True(response1.Document != null, "First V1 request should return a valid document");

            var instance2ClientV1 = _services.GetRequiredService<Mindee.V1.Client>();
            var inputSource2 = new LocalInputSource(new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var response2 = await instance2ClientV1.ParseAsync<InvoiceV4>(inputSource2);
            Assert.NotNull(response2);
            Assert.True(response2.Document != null, "Second V1 request should return a valid document");

            var instance1ClientV2 = _services.GetRequiredService<Client>();
            var inputSource3 = new LocalInputSource(new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var response3 = await instance1ClientV2.EnqueueInferenceAsync(
                inputSource3,
                new InferenceParameters(Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id")));
            Assert.NotNull(response3);
            Assert.True(response3.Job != null, "First V2 request should return a valid job");

            var inputSource4 = new LocalInputSource(new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var response4 = await instance1ClientV1.ParseAsync<InvoiceV4>(inputSource4);
            Assert.NotNull(response4);
            Assert.True(response4.Document != null, "Third V1 request should return a valid document");

            var instance2ClientV2 = _services.GetRequiredService<Client>();
            var inputSource5 = new LocalInputSource(new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var response5 = await instance2ClientV2.EnqueueInferenceAsync(
                inputSource5,
                new InferenceParameters(Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id")));
            Assert.NotNull(response5);
            Assert.True(response5.Job != null, "Second V2 request should return a valid job");
        }
    }
}
