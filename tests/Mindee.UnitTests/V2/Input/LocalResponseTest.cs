using Mindee.Input;
using Mindee.Parsing.V2;

namespace Mindee.UnitTests.V2.Input
{
    [Trait("Category", "JSON Response loading")]
    [Trait("Category", "V2")]
    public class LocalResponseTest
    {
        // File which the signature applies to.
        private const string FilePath = Constants.V2RootDir + "inference/standard_field_types.json";

        private static void AssertLocalResponse(LocalResponse localResponse)
        {
            // Fake secret key.
            const string secretKey = "ogNjY44MhvKPGTtVsI8zG82JqWQa68woYQH";

            // Real signature using fake secret key.
            const string signature = "b82a515c832fd2c4f4ce3a7e6f53c12e8d10e19223f6cf0e3a9809a7a3da26be";
            Assert.False(localResponse.IsValidHmacSignature(
                secretKey, "invalid signature is invalid"));
            Assert.Equal(signature, localResponse.GetHmacSignature(secretKey));
            Assert.True(localResponse.IsValidHmacSignature(
                secretKey, signature));
            InferenceResponse inferenceResponse = localResponse.DeserializeResponse<InferenceResponse>();
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
