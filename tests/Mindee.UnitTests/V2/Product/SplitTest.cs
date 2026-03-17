using System.Reflection;
using Mindee.V2.Parsing;
using Mindee.V2.Product;
using Mindee.V2.Product.Split;
using Mindee.V2.Product.Split.Params;

namespace Mindee.UnitTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "SplitInference")]
    public class SplitTest
    {
        [Fact]
        public void Parameters_MustInit()
        {
            var productParams = new SplitParameters("invalid-model-id");
            Assert.Equal("invalid-model-id", productParams.ModelId);

            var productAttributes = productParams.GetType().GetCustomAttribute<ProductAttributes>();
            Assert.Equal("split", productAttributes?.Slug);
        }

        [Fact]
        public void Split_WhenSingle_MustHaveValidProperties()
        {
            var response = GetInference("split/split_single.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            var model = inference.Model;
            Assert.NotNull(model);

            var splits = inference.Result.Splits;
            Assert.NotNull(splits);
            Assert.Single(splits);

            var firstSplit = splits.First();
            Assert.Equal("receipt", firstSplit.DocumentType);

            Assert.NotNull(firstSplit.PageRange);
            Assert.Equal(2, firstSplit.PageRange.Count);
            Assert.Equal(0, firstSplit.PageRange[0]);
            Assert.Equal(0, firstSplit.PageRange[1]);
        }

        [Fact]
        public void Split_WhenMultiple_MustHaveValidProperties()
        {
            var response = GetInference("split/split_multiple.json");
            AssertInferenceResponse(response);

            var inference = response.Inference;

            var model = inference.Model;
            Assert.NotNull(model);

            var splits = inference.Result.Splits;
            Assert.NotNull(splits);
            Assert.Equal(3, splits.Count);

            var firstSplit = splits[0];
            Assert.Equal("passport", firstSplit.DocumentType);

            Assert.NotNull(firstSplit.PageRange);
            Assert.Equal(2, firstSplit.PageRange.Count);
            Assert.Equal(0, firstSplit.PageRange[0]);
            Assert.Equal(0, firstSplit.PageRange[1]);

            var secondSplit = splits[1];
            Assert.Equal("invoice", secondSplit.DocumentType);

            Assert.NotNull(secondSplit.PageRange);
            Assert.Equal(2, secondSplit.PageRange.Count);
            Assert.Equal(1, secondSplit.PageRange[0]);
            Assert.Equal(3, secondSplit.PageRange[1]);
        }

        private static SplitResponse GetInference(string path)
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2ProductDir + path));
            return localResponse.DeserializeResponse<SplitResponse>();
        }

        private void AssertInferenceResponse(SplitResponse response)
        {
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.Id);
            Assert.NotNull(response.Inference.File);
            Assert.NotNull(response.Inference.Result);
        }
    }
}
