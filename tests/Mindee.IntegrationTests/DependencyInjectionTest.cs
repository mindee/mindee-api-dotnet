using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindee.Extensions.DependencyInjection;
using Mindee.Input;
using Mindee.Product.Invoice;

// using RestSharp;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "DI")]
    public class DependencyInjectionTest
    {
        [Fact]
        public async Task ShouldInstantiateWithDependencyInjection()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMindeeClient();
                });

            var host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var mindeeClient = services.GetRequiredService<MindeeClient>();
                var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
                var response = await mindeeClient.ParseAsync<InvoiceV4>(inputSource);
                Assert.NotNull(response);
            }


            await host.StopAsync();
        }
    }
}
