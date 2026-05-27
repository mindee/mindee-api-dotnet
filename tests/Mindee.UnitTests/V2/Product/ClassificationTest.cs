using System.Reflection;
using Mindee.V2.Parsing;
using Mindee.V2.Product;
using Mindee.V2.Product.Classification;
using Mindee.V2.Product.Classification.Params;
using Mindee.V2.Product.Extraction;

namespace Mindee.UnitTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "ClassificationInference")]
    public class ClassificationTest
    {
        [Fact]
        public void Parameters_MustInit()
        {
            var productParams = new ClassificationParameters("invalid-model-id");
            Assert.Equal("invalid-model-id", productParams.ModelId);

            var productAttributes = productParams.GetType().GetCustomAttribute<ProductAttributes>();
            Assert.Equal("classification", productAttributes?.Slug);
        }

        [Fact]
        public void Classification_WhenSingle_MustHaveValidProperties()
        {
            var response = GetInference("classification/default_sample.json");
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

        [Fact]
        public void Classification_WithExtraction_MustHaveValidProperties()
        {
            var response = GetInference("classification/default_sample_extraction.json");
            Assert.NotNull(response.Inference);
            Assert.Equal(
                "invoice",
                response.Inference.Result.Classification.DocumentType
            );

            ExtractionResponse extractionResponse = response
                .Inference
                .Result
                .Classification
                .ExtractionResponse;
            Assert.NotNull(extractionResponse);
            Assert.Equal(
                "Jiro Doi",
                extractionResponse
                    .Inference
                    .Result
                    .Fields["customer_name"]
                    .SimpleField
                    .Value
            );
        }

        private static ClassificationResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2ProductDir + path));
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
