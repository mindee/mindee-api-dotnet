using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Inference;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "FailedInferenceResponse")]
    public class FailedInferenceResponseTest
    {
        [Fact]
        public void WhenFailed_MustLoad()
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2RootDir + "errors/webhook_error_500_failed.json"));
            var response = localResponse.DeserializeResponse<FailedInferenceResponse>();
            Assert.NotNull(response);
            Assert.Equal("12345678-1234-1234-1234-123456789ABC", response.InferenceId);
            Assert.Equal("default_sample.jpg", response.FileName);
            Assert.Equal("dummy-alias.jpg", response.FileAlias);
            Assert.IsType<DateTime>(response.CreatedAt);
            Assert.NotNull(response.Error);
            Assert.Equal(500, response.Error.Status);
            Assert.Equal("500-012", response.Error.Code);
        }
    }
}
