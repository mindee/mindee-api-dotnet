using Mindee.V2.Parsing;
using Mindee.V2.Product.Classification;

namespace Mindee.UnitTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "ClassificationInference")]
    public class ClassificationTest
    {
        [Fact]
        public void Classification_WhenSingle_MustHaveValidProperties()
        {
            var response = GetInference("products/classification/classification_single.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            Assert.Equal("12345678-1234-1234-1234-123456789abc", inference.Id);
            Assert.Equal("test-model-id", inference.Model.Id);
            Assert.Equal("12345678-1234-1234-1234-jobid1234567", inference.Job.Id);

            Assert.Equal("default_sample.jpg", inference.File.Name);
            Assert.Equal(1, inference.File.PageCount);
            Assert.Equal("image/jpeg", inference.File.MimeType);

            var classification = inference.Result.Classification;
            Assert.Equal("invoice", classification.DocumentType);
        }
        private static ClassificationResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2RootDir + path));
            return localResponse.DeserializeResponse<ClassificationResponse>();
        }

        private void AssertInferenceResponse(ClassificationResponse response)
        {
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.Id);
            Assert.NotNull(response.Inference.File);
            Assert.NotNull(response.Inference.Result);
        }
    }
}
