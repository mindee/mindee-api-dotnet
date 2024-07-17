using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Mindee.DependencyInjection;
using Mindee.Exceptions;
using Mindee.Http;
using Mindee.Product.Invoice;
using Mindee.Product.Receipt;
using Moq;
using Moq.Protected;
using RestSharp;

namespace Mindee.UnitTests.Http
{
    [Trait("Category", "Mindee Api")]
    public sealed class MindeeApiTest
    {

        private const string DateOutputFormat = "yyyy-MM-ddTHH:mm:ss.fffff";

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
        [Fact]
        public async Task Predict_WithWrongKey()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.BadRequest,
                File.ReadAllText("Resources/errors/error_401_invalid_token.json")
            );

            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            await Assert.ThrowsAsync<Mindee400Exception>(
                () => _ = mindeeApi.PredictPostAsync<ReceiptV4>(
                    UnitTestBase.GetFakePredictParameter()
                )
            );
        }

        [Fact]
        public async Task Predict_WithValidResponse()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.OK,
                File.ReadAllText("Resources/products/invoices/response_v4/complete.json")
            );

            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            var response = await mindeeApi.PredictPostAsync<InvoiceV4>(
                UnitTestBase.GetFakePredictParameter()
            );

            var expectedParse = File.ReadAllText("Resources/products/invoices/response_v4/summary_full.rst");
            var expectedJson = File.ReadAllText("Resources/products/invoices/response_v4/complete.json");

            Assert.NotNull(response);
            Assert.Equal(expectedParse, response.Document.ToString());
            Assert.Equal(expectedJson, response.RawResponse);
        }

        [Fact]
        public async Task Predict_WithErrorResponse()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.BadRequest,
                File.ReadAllText("Resources/errors/error_400_with_object_in_detail.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            await Assert.ThrowsAsync<Mindee400Exception>(
                           () => mindeeApi.PredictPostAsync<ReceiptV4>(
                               UnitTestBase.GetFakePredictParameter())
                           );
        }

        [Fact]
        public async Task Predict_500Error()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.InternalServerError,
                File.ReadAllText("Resources/errors/error_500_inference_fail.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();
            await Assert.ThrowsAsync<Mindee500Exception>(
                           () => mindeeApi.PredictPostAsync<ReceiptV4>(
                               UnitTestBase.GetFakePredictParameter())
                           );
        }

        [Fact]
        public async Task Predict_429Error()
        {
            var serviceProvider = InitServiceProvider(
                (HttpStatusCode)429,
                File.ReadAllText("Resources/errors/error_429_too_many_requests.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            await Assert.ThrowsAsync<Mindee429Exception>(
               () => mindeeApi.PredictPostAsync<ReceiptV4>(
                   UnitTestBase.GetFakePredictParameter())
               );
        }

        [Fact]
        public async Task Predict_401Error()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.Unauthorized,
                File.ReadAllText("Resources/errors/error_401_invalid_token.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            await Assert.ThrowsAsync<Mindee401Exception>(
                () => mindeeApi.PredictPostAsync<ReceiptV4>(
                    UnitTestBase.GetFakePredictParameter())
                );
        }

        [Fact]
        public async Task PredictAsyncPostAsync_WithFailForbidden()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.Forbidden,
                File.ReadAllText("Resources/async/post_fail_forbidden.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            await Assert.ThrowsAsync<Mindee403Exception>(
                () => mindeeApi.PredictAsyncPostAsync<ReceiptV4>(
                    UnitTestBase.GetFakePredictParameter())
                );
        }

        [Fact]
        public async Task PredictAsyncPostAsync_WithSuccess()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.OK,
                File.ReadAllText("Resources/async/post_success.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            var response = await mindeeApi.PredictAsyncPostAsync<InvoiceV4>(
                UnitTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("waiting", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
            Assert.Equal("2023-02-16T12:33:49.60294", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt?.ToString(DateOutputFormat));
        }

        [Fact]
        public async Task DocumentQueueGetAsync_WithJobProcessing()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.OK,
                File.ReadAllText("Resources/async/get_processing.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            var response = await mindeeApi.DocumentQueueGetAsync<InvoiceV4>("76c90710-3a1b-4b91-8a39-31a6543e347c");

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("processing", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
            Assert.Equal("2023-03-16T12:33:49.60294", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt);
        }

        [Fact]
        public async Task DocumentQueueGetAsync_WithJobFailed()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.OK,
                File.ReadAllText("Resources/async/get_failed_job_error.json")
            );
            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            await Assert.ThrowsAsync<Mindee500Exception>(
                () => mindeeApi.DocumentQueueGetAsync<InvoiceV4>("d88406ed-47bd-4db0-b3f3-145c8667a343")
            );
        }

        [Fact]
        public async Task DocumentQueueGetAsync_WithSuccess()
        {
            var serviceProvider = InitServiceProvider(
                HttpStatusCode.OK,
                File.ReadAllText("Resources/async/get_completed.json")
            );

            var mindeeApi = serviceProvider.GetRequiredService<MindeeApi>();

            var response = await mindeeApi.DocumentQueueGetAsync<InvoiceV4>("my-job-id");

            Assert.NotNull(response);
            Assert.NotNull(response.Document);
            Assert.Contains(response.ApiRequest.Resources, r => r.Equals("job"));
            Assert.Contains(response.ApiRequest.Resources, r => r.Equals("document"));
        }
    }
}
