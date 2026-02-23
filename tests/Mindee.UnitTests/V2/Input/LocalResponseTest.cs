using Mindee.V2.Parsing;
using Mindee.V2.Product.Extraction;

namespace Mindee.UnitTests.V2.Input
{
    [Trait("Category", "JSON Response loading")]
    [Trait("Category", "V2")]
    public class LocalResponseTest
    {
        // File which the signature applies to.
        private const string FilePath = Constants.V2RootDir + "products/extraction/standard_field_types.json";

        private static void AssertLocalResponse(LocalResponse localResponse)
        {
            // Fake secret key.
            const string secretKey = "ogNjY44MhvKPGTtVsI8zG82JqWQa68woYQH";

            // Real signature using fake secret key.
            const string signature = "e51bdf80f1a08ed44ee161100fc30a25cb35b4ede671b0a575dc9064a3f5dbf1";
            Assert.False(localResponse.IsValidHmacSignature(
                secretKey, "invalid signature is invalid"));
            Assert.Equal(signature, localResponse.GetHmacSignature(secretKey));
            Assert.True(localResponse.IsValidHmacSignature(
                secretKey, signature));
            ExtractionResponse inferenceResponse = localResponse.DeserializeResponse<ExtractionResponse>();
            Assert.NotNull(inferenceResponse);
            Assert.NotNull(inferenceResponse.Inference);
        }

        [Fact]
        public void LoadDocument_WithFile_MustReturnValidLocalResponse()
        {
            var localResponse = new LocalResponse(new FileInfo(FilePath));
            AssertLocalResponse(localResponse);
        }

        [Fact]
        public void LoadDocument_WithString_MustReturnValidLocalResponse()
        {
            var localResponse = new LocalResponse(File.ReadAllText(FilePath));
            AssertLocalResponse(localResponse);
        }
    }
}
