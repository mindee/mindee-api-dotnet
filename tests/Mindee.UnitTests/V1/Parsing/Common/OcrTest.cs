using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.V1.Parsing.Common
{
    [Trait("Category", "OCR")]
    public class OcrTest
    {
        private async Task<Ocr> LoadOcr()
        {
            var response = await JsonSerializer.DeserializeAsync<PredictResponse<ReceiptV4>>(
                new FileInfo(Constants.V1RootDir + "extras/ocr/complete.json").OpenRead());

            Assert.NotNull(response);
            Assert.NotNull(response.Document.Ocr);
            return response.Document.Ocr;
        }

        [Fact]
        public async Task ShouldHaveCorrectWordCount()
        {
            var ocr = await LoadOcr();
            Assert.NotNull(ocr.MvisionV1);
            Assert.Single(ocr.MvisionV1.Pages);
            Assert.Equal(95, ocr.MvisionV1.Pages[0].AllWords.Count);
        }

        [Fact]
        public async Task StringShouldBeOrdered()
        {
            var ocr = await LoadOcr();
            var expected = File.ReadAllText(Constants.V1RootDir + "extras/ocr/ocr.txt");
            Assert.Equal(expected, ocr.ToString());
        }
    }
}
