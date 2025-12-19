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
            const string signature = "1df388c992d87897fe61dfc56c444c58fc3c7369c31e2b5fd20d867695e93e85";
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
