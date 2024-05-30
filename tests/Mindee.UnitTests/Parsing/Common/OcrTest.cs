using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.Parsing.Common
{
    [Trait("Category", "OCR")]
    public class OcrTest
    {
        private async Task<Ocr> LoadOcr()
        {
            var response = await JsonSerializer.DeserializeAsync<PredictResponse<ReceiptV4>>(
                new FileInfo("Resources/extras/ocr/complete.json").OpenRead());

            Assert.NotNull(response);
            Assert.NotNull(response.Document.Ocr);
            return response.Document.Ocr;
        }

        [Fact]
        public async void ShouldHaveCorrectWordCount()
        {
            var ocr = await LoadOcr();
            Assert.NotNull(ocr.MvisionV1);
            Assert.Single(ocr.MvisionV1.Pages);
            Assert.Equal(expected: 95, actual: ocr.MvisionV1.Pages[0].AllWords.Count);
        }

        [Fact]
        public async void StringShouldBeOrdered()
        {
            var ocr = await LoadOcr();
            var expected = File.ReadAllText("Resources/extras/ocr/ocr.txt");
            Assert.Equal(expected, ocr.ToString());
        }
    }
}
