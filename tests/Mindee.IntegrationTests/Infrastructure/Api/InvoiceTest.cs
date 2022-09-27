using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Api.Invoice;

namespace Mindee.IntegrationTests.Infrastructure.Api
{
    public class InvoiceTest
    {
        [Fact]
        public async Task Predict_WithWrongApiKey_MustFail()
        {
            var api = new MindeeApi(
                new NullLogger<MindeeApi>(),
                Options.Create(
                    new MindeeApiSettings() { ApiKey = "WrongKey" }));

            await Assert.ThrowsAsync<HttpRequestException>(
                () => api.PredictAsync(File.OpenRead("sample_2pages.pdf"), "sample_2pages.pdf"));
        }

        [Fact]
        public async Task Predict_WithValidPdf_WichIsNotAnInvoice_MustSuccess()
        {
            var api = new MindeeApi(
                new NullLogger<MindeeApi>(),
                Options.Create(
                    new MindeeApiSettings() { ApiKey = "954d2d94e777466a57afe6a2323ea4b9" }));

            var invoicePredictResponse = await api.PredictAsync(File.OpenRead("sample_2pages.pdf"), "sample_2pages.pdf");

            Assert.NotNull(invoicePredictResponse);
        }

        [Fact]
        public async Task Predict_WithValidPdf_WichIsAnInvoice_MustSuccess()
        {
            var api = new MindeeApi(
                new NullLogger<MindeeApi>(),
                Options.Create(
                    new MindeeApiSettings() { ApiKey = "954d2d94e777466a57afe6a2323ea4b9" }));

            var invoicePredictResponse = await api.PredictAsync(File.OpenRead("inv2.pdf"), "inv2.pdf");

            var expectedInvoiceResponse = JsonSerializer.Deserialize<InvoicePredictResponse>(File.ReadAllText("inv2.json"));

            Assert.NotNull(invoicePredictResponse);
            Assert.NotNull(expectedInvoiceResponse);
            Assert.NotNull(invoicePredictResponse!.Document);
            Assert.NotNull(expectedInvoiceResponse!.Document);
            Assert.Equal(expectedInvoiceResponse.Document.Inference, expectedInvoiceResponse.Document.Inference);
        }

        [Fact]
        public async Task Predict_WithPng_WichIsAnInvoice_MustSuccess()
        {
            var api = new MindeeApi(
                new NullLogger<MindeeApi>(),
                Options.Create(
                    new MindeeApiSettings() { ApiKey = "954d2d94e777466a57afe6a2323ea4b9" }));

            var invoicePredictResponse = await api.PredictAsync(File.OpenRead("inv1.png"), "inv1.png");

            var expectedInvoiceResponse = JsonSerializer.Deserialize<InvoicePredictResponse>(File.ReadAllText("inv1.json"));

            Assert.NotNull(invoicePredictResponse);
            Assert.NotNull(expectedInvoiceResponse);
            Assert.NotNull(invoicePredictResponse!.Document);
            Assert.NotNull(expectedInvoiceResponse!.Document);
            Assert.Equal(expectedInvoiceResponse.Document.Inference, expectedInvoiceResponse.Document.Inference);
        }
    }
}
