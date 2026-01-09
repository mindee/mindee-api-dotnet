using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.InternationalId;

namespace Mindee.UnitTests.V1.Parsing.Common
{
    [Trait("Category", "FullTextOcr")]
    public class FullTextOcrTest
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        private Inference<InternationalIdV2Document, InternationalIdV2Document> LoadInference()
        {
            var json = File.ReadAllText(Constants.V1RootDir + "extras/full_text_ocr/complete.json");
            var prediction = JsonSerializer.Deserialize<AsyncPredictResponse<InternationalIdV2>>(json, JsonOptions);
            if (prediction == null)
            {
                throw new Exception();
            }

            return prediction.Document.Inference;
        }

        private List<Page<InternationalIdV2Document>> LoadPages()
        {
            var json = File.ReadAllText(Constants.V1RootDir + "extras/full_text_ocr/complete.json");
            var prediction = JsonSerializer.Deserialize<AsyncPredictResponse<InternationalIdV2>>(json, JsonOptions);
            if (prediction == null)
            {
                throw new Exception();
            }

            return prediction.Document.Inference.Pages;
        }

        [Fact]
        public void Should_GetFullTextOcrResult()
        {
            var expectedText = File.ReadAllLines(Constants.V1RootDir + "extras/full_text_ocr/full_text_ocr.txt");
            var pages = LoadPages();
            var inference = LoadInference();

            // Act
            var fullTextOcr = inference.Extras.FullTextOcr;
            var page0Ocr = pages[0].Extras.FullTextOcr.Content;

            // Assert
            Assert.Equal(string.Join("\n", expectedText), fullTextOcr);
            Assert.Equal(string.Join("\n", expectedText), page0Ocr);
        }
    }
}
