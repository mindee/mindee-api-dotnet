using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.Invoice;

namespace Mindee.IntegrationTests.Infrastructure.Api
{
    public class MindeeApiInvoiceTest
    {
        [Fact]
        public async Task Predict_WithWrongApiKey_MustFail()
        {
            var api = GetMindeeApi("WrongKey");

            await Assert.ThrowsAsync<MindeeException>(
                () => api.PredictAsync<InvoiceV3Prediction>(new PredictParameter(File.ReadAllBytes("sample_2pages.pdf"), "sample_2pages.pdf")));
        }

        [Fact]
        public async Task Predict_WithValidPdf_WichIsNotAnInvoice_MustSuccess()
        {
            var api = GetMindeeApi("validKey");

            var invoicePredictResponse = await api.PredictAsync<InvoiceV3Prediction>(new PredictParameter(File.ReadAllBytes("sample_2pages.pdf"), "sample_2pages.pdf"));

            Assert.NotNull(invoicePredictResponse);
        }

        [Fact]
        public async Task Predict_WithValidPdf_WichIsAnInvoice_MustSuccess()
        {
            var api = GetMindeeApi("validKey");

            var invoicePredictResponse = await api.PredictAsync<InvoiceV3Prediction>(new PredictParameter(File.ReadAllBytes("inv2.pdf"), "inv2.pdf"));

            var expectedInvoiceResponse = JsonSerializer.Deserialize<PredictResponse<InvoiceV3Prediction>>(File.ReadAllText("inv2.json"));

            Assert.NotNull(invoicePredictResponse);
            Assert.NotNull(expectedInvoiceResponse);
            Assert.NotNull(expectedInvoiceResponse!.Document);
            Assert.Equal(expectedInvoiceResponse.Document.Inference, expectedInvoiceResponse.Document.Inference);
        }

        [Fact]
        public async Task Predict_WithPng_WichIsAnInvoice_MustSuccess()
        {
            var api = GetMindeeApi("validKey");

            var invoicePredictResponse = await api.PredictAsync<InvoiceV3Prediction>(new PredictParameter(File.ReadAllBytes("inv1.png"), "inv1.png"));

            var expectedInvoiceResponse = JsonSerializer.Deserialize<PredictResponse<InvoiceV3Prediction>>(File.ReadAllText("inv1.json"));

            Assert.NotNull(invoicePredictResponse);
            Assert.NotNull(expectedInvoiceResponse);
            Assert.NotNull(expectedInvoiceResponse!.Document);
            Assert.Equal(expectedInvoiceResponse.Document.Inference, expectedInvoiceResponse.Document.Inference);
        }

        private MindeeApi GetMindeeApi(string apiKey)
        {
            return new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" })
                , new NullLogger<MindeeApi>()
                );
        }
    }
}
