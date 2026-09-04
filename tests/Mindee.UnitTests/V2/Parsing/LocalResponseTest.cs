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

        private static void AssertLocalResponse(LocalResponse localResponse, string fileContent)
        {
            Assert.False(localResponse.IsValidHmacSignature(DummySecretKey, "invalid signature"));
            Assert.Equal(Signature, localResponse.GetHmacSignature(DummySecretKey));
            Assert.True(localResponse.IsValidHmacSignature(DummySecretKey, Signature));

            var response = localResponse.DeserializeResponse<ExtractionResponse>();

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            Assert.Equal("test-model-id", response.Inference.Model.Id);
            Assert.Equal("field_simple_string-value",
                response.Inference.Result.Fields["field_simple_string"].SimpleField.Value);

            Assert.True(JsonNode.DeepEquals(
                JsonNode.Parse(response.RawResponse),
                JsonNode.Parse(fileContent)));
        }

        [Fact(DisplayName = "should load a response from a JSON string")]
        public void JsonString_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, "extraction/standard_field_types.json");
            string fileContent = File.ReadAllText(filePath);
            var localResponse = new LocalResponse(fileContent);

            AssertLocalResponse(localResponse, fileContent);
        }

        [Fact(DisplayName = "should load a response from a JSON file")]
        public void JsonFile_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, "extraction/standard_field_types.json");
            var localResponse = new LocalResponse(new FileInfo(filePath));

            AssertLocalResponse(localResponse, File.ReadAllText(filePath));
        }

        [Fact(DisplayName = "should load a response from a stream")]
        public void Stream_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, "extraction/standard_field_types.json");
            using (var stream = File.OpenRead(filePath))
            {
                var localResponse = new LocalResponse(stream);
                AssertLocalResponse(localResponse, File.ReadAllText(filePath));

                // make sure the stream is not closed by the LocalResponse constructor
                stream.Position = 10;
                var someByte = stream.ReadByte();
                Assert.NotEqual(-1, someByte);
            }
        }

        [Fact(DisplayName = "should load a response from a buffer")]
        public void Buffer_mustLoadValidLocalResponse()
        {
            string filePath = Path.Combine(Constants.V2ProductPath, "extraction/standard_field_types.json");
            var localResponse = new LocalResponse(File.ReadAllBytes(filePath));

            AssertLocalResponse(localResponse, File.ReadAllText(filePath));
        }

        [Fact(DisplayName = "should raise an exception when given an invalid JSON string")]
        public void InvalidJsonString_mustRaiseException()
        {
            var localResponse = new LocalResponse("{invalid json");

            Assert.Throws<JsonException>(
                () => localResponse.DeserializeResponse<ExtractionResponse>()
            );
        }

        [Fact(DisplayName = "should raise an exception when given an empty value")]
        public void EmptyString_mustRaiseException()
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
        public void Null_mustRaiseException()
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
