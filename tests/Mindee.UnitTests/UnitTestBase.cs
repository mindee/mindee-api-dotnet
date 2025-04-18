using System.Net;
using System.Net.Http; // Necessary for .NET 4.7.2
using Microsoft.Extensions.DependencyInjection;
using Mindee.Extensions.DependencyInjection;
using Mindee.Http;
using Mindee.Input;
using Moq;
using Moq.Protected;
using RestSharp;

namespace Mindee.UnitTests
{
    internal static class UnitTestBase
    {
        internal static PredictParameter GetFakePredictParameter()
        {
            return new PredictParameter(
                localSource: new LocalInputSource(
                    fileBytes: new byte[] { byte.MinValue },
                    filename: "titicaca.pdf"),
                urlSource: null,
                cropper: false,
                allWords: false,
                fullText: false,
                workflowId: null);
        }

        private static ServiceProvider InitServiceProvider(HttpStatusCode statusCode, string fileContent)
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.Configure<MindeeSettings>(options =>
            {
                options.ApiKey = "MyKey";
                options.MindeeBaseUrl = "https://api.mindee.net";
                options.RequestTimeoutSeconds = 120;
            });

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(fileContent, System.Text.Encoding.UTF8, "application/json")
                });


            services.AddMindeeApi(options =>
            {
                options.ApiKey = "MyKey";
                options.MindeeBaseUrl = "https://api.mindee.net";
                options.RequestTimeoutSeconds = 120;
            });

            services.AddSingleton(new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://api.mindee.net"),
                ConfigureMessageHandler = _ => mockHttpMessageHandler.Object,
            }));

            return services.BuildServiceProvider();
        }

        internal static MindeeApi GetMindeeApi(string fileName)
        {
            var fileContent = File.ReadAllText(fileName);

            var serviceProvider = InitServiceProvider(
                HttpStatusCode.OK,
                fileContent: fileContent
            );

            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            return mindeeApi;
        }
    }
}
