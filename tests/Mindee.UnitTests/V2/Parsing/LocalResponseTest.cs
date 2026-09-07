using System.Text.Json;
using System.Text.Json.Nodes;
using Mindee.V2.Parsing;
using Mindee.V2.Product.Extraction;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Load Local Response")]
    public class LocalResponseTest
    {
        private const string Signature = "e51bdf80f1a08ed44ee161100fc30a25cb35b4ede671b0a575dc9064a3f5dbf1";
        private const string DummySecretKey = "ogNjY44MhvKPGTtVsI8zG82JqWQa68woYQH";
        private const string FilePath = "extraction/standard_field_types.json";

        /// <summary>
        /// Asserts that a local response is valid.
        /// </summary>
        private static void AssertLocalResponse(LocalResponse localResponse, string fileContent)
        {
            Assert.Equal(Signature, localResponse.GetHmacSignature(DummySecretKey));

            Assert.False(localResponse.IsValidHmacSignature(DummySecretKey, "invalid signature"));
            Assert.False(localResponse.IsValidHmacSignature(DummySecretKey, null));
            Assert.False(localResponse.IsValidHmacSignature(null, Signature));
            Assert.False(localResponse.IsValidHmacSignature(null, null));
            Assert.False(localResponse.IsValidHmacSignature(DummySecretKey, ""));
            Assert.True(localResponse.IsValidHmacSignature(DummySecretKey, Signature));
            Assert.True(localResponse.IsValidHmacSignature(DummySecretKey, Signature.ToUpper()));

            var response = localResponse.DeserializeResponse<ExtractionResponse>();

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            Assert.Equal("test-model-id", response.Inference.Model.Id);
            Assert.Equal("field_simple_string-value",
                response.Inference.Result.Fields["field_simple_string"].SimpleField.Value);

            Assert.True(JsonNode.DeepEquals(
                JsonNode.Parse(response.RawResponse),
                JsonNode.Parse(fileContent)));

            Assert.Equal(fileContent.Replace("\r", "").Replace("\n", ""), localResponse.ToString());
        }

        [Fact(DisplayName = "should load a response from a JSON string")]
        public void ValidString_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, FilePath);
            string fileContent = File.ReadAllText(filePath);
            var localResponse = new LocalResponse(fileContent);

            AssertLocalResponse(localResponse, fileContent);
        }

        [Fact(DisplayName = "should load a response from a buffer")]
        public void ValidBuffer_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, FilePath);
            var localResponse = new LocalResponse(File.ReadAllBytes(filePath));

            AssertLocalResponse(localResponse, File.ReadAllText(filePath));
        }

        [Fact(DisplayName = "should load a response from a JSON file")]
        public void ValidFile_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, FilePath);
            var localResponse = new LocalResponse(new FileInfo(filePath));

            AssertLocalResponse(localResponse, File.ReadAllText(filePath));
        }

        [Fact(DisplayName = "should load a response from a stream")]
        public void ValidStream_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, FilePath);
            using (var stream = File.OpenRead(filePath))
            {
                var localResponse = new LocalResponse(stream);
                AssertLocalResponse(localResponse, File.ReadAllText(filePath));

                // Explicitly verify the stream is not closed by the LocalResponse constructor
                stream.Position = 0;
                Assert.NotEqual(-1, stream.ReadByte());
            }
        }

        [Fact(DisplayName = "should raise an exception when given an invalid JSON string")]
        public void InvalidString_mustRaiseException()
        {
            var localResponse = new LocalResponse("{invalid json");

            Assert.Throws<JsonException>(
                () => localResponse.DeserializeResponse<ExtractionResponse>()
            );
        }

        [Fact(DisplayName = "should raise an exception when given an empty value")]
        public void EmptyValue_mustRaiseException()
        {
            Assert.Throws<ArgumentException>(
                () => new LocalResponse("")
            );
            Assert.Throws<ArgumentException>(
                () => new LocalResponse([])
            );
            Assert.Throws<ArgumentException>(
                () => new LocalResponse(Stream.Null)
            );
        }

        [Fact(DisplayName = "should raise an exception when given a null value")]
        public void NullValue_mustRaiseException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new LocalResponse((string?)null)
            );
            Assert.Throws<ArgumentNullException>(
                () => new LocalResponse((byte[]?)null)
            );
            Assert.Throws<ArgumentNullException>(
                () => new LocalResponse((Stream?)null)
            );
            Assert.Throws<ArgumentNullException>(
                () => new LocalResponse((FileInfo?)null)
            );
        }
    }
}
