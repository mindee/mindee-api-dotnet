using Mindee.V2.Parsing;
using Mindee.V2.Product.Ocr;

namespace Mindee.UnitTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "OcrInference")]
    public class OcrTest
    {
        [Fact]
        public void Ocr_WhenSingle_MustHaveValidProperties()
        {
            var response = GetInference("ocr/ocr_single.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            Assert.Equal("12345678-1234-1234-1234-123456789abc", inference.Id);
            Assert.Equal("test-model-id", inference.Model.Id);

            Assert.Equal("default_sample.jpg", inference.File.Name);
            Assert.Equal(1, inference.File.PageCount);
            Assert.Equal("image/jpeg", inference.File.MimeType);

            var pages = inference.Result.Pages;
            Assert.NotNull(pages);
            Assert.Single(pages);

            var firstPage = pages.First();
            Assert.NotNull(firstPage.Words);

            var firstWord = firstPage.Words[0];
            Assert.Equal("Shipper:", firstWord.Content);
            Assert.Equal(4, firstWord.Polygon.Count);

            var fifthWord = firstPage.Words[4];
            Assert.Equal("INC.", fifthWord.Content);
            Assert.Equal(4, fifthWord.Polygon.Count);
        }

        [Fact]
        public void Ocr_WhenMultiple_MustHaveValidProperties()
        {
            var response = GetInference("ocr/ocr_multiple.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            var job = inference.Job;
            Assert.Equal("12345678-1234-1234-1234-jobid1234567", job.Id);

            var model = inference.Model;
            Assert.NotNull(model);

            var pages = inference.Result.Pages;
            Assert.NotNull(pages);
            Assert.Equal(3, pages.Count);

            foreach (var page in pages)
            {
                Assert.NotNull(page.Words);
                Assert.NotNull(page.Content);
                Assert.IsType<string>(page.Content);
            }
        }

        private static OcrResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2ProductDir + path));
            return localResponse.DeserializeResponse<OcrResponse>();
        }

        private void AssertInferenceResponse(OcrResponse response)
        {
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.Id);
            Assert.NotNull(response.Inference.File);
            Assert.NotNull(response.Inference.Result);
        }
    }
}
