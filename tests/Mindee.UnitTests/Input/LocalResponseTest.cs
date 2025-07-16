using Mindee.Input;

namespace Mindee.UnitTests.Input
{
    [Trait("Category", "JSON Response loading")]
    public class LocalResponseTest
    {
        // Fake secret key.
        private const string SecretKey = "ogNjY44MhvKPGTtVsI8zG82JqWQa68woYQH";

        // Real signature using fake secret key.
        private const string Signature = "5ed1673e34421217a5dbfcad905ee62261a3dd66c442f3edd19302072bbf70d0";

        // ResultFile which the signature applies to.
        private const string FilePath = "Resources/async/get_completed_empty.json";

        [Fact]
        public void LoadDocument_WithFile_MustReturnValidLocalResponse()
        {
            var localResponse = new LocalResponse(new FileInfo(FilePath));

            Assert.False(localResponse.IsValidHmacSignature(
                SecretKey, "invalid signature is invalid"));
            Assert.Equal(Signature, localResponse.GetHmacSignature(SecretKey));
            Assert.True(localResponse.IsValidHmacSignature(
                SecretKey, Signature));
        }

        [Fact]
        public void LoadDocument_WithString_MustReturnValidLocalResponse()
        {
            var localResponse = new LocalResponse(File.ReadAllText(FilePath));

            Assert.False(localResponse.IsValidHmacSignature(
                SecretKey, "invalid signature is invalid"));
            Assert.Equal(Signature, localResponse.GetHmacSignature(SecretKey));
            Assert.True(localResponse.IsValidHmacSignature(
                SecretKey, Signature));
        }
    }
}
