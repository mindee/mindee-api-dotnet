using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.Parsing.Common
{
    [Trait("Category", "OCR")]
    public class OcrTest
    {
        [Fact]
        public async Task Can_Load_Ocr()
        {
            var response = await JsonSerializer.DeserializeAsync<PredictResponse<ReceiptV4>>(
                new FileInfo("Resources/ocr/complete_with_ocr.json").OpenRead());

            Assert.NotNull(response);
            Assert.NotNull(response.Document.Ocr);
            Assert.NotNull(response.Document.Ocr.MvisionV1);
            Assert.Single(response.Document.Ocr.MvisionV1.Pages);
            Assert.Equal(expected: 61, actual: response.Document.Ocr.MvisionV1.Pages[0].AllWords.Count);
        }
    }
}
