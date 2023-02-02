using System.Text.Json;
using Mindee.Parsing.Common;

namespace Mindee.UnitTests.Parsing.Common
{
    [Trait("Category", "Error property")]
    public class ErrorTest
    {
        [Fact]
        public async Task Given_Details_As_object_MustBeDeserialized()
        {
            var error = await JsonSerializer.DeserializeAsync<Error>(
                new FileInfo("Resources/errors/with_object_response_in_detail.json").OpenRead());

            Assert.NotNull(error);
            Assert.NotNull(error.Details);
            Assert.Equal("{\"document\":[\"error message\"]}", error.Details.ToString());
        }

        [Fact]
        public async Task Given_Details_As_String_MustBeDeserialized()
        {
            var error = await JsonSerializer.DeserializeAsync<Error>(
                new FileInfo("Resources/errors/with_string_response_in_detail.json").OpenRead());

            Assert.NotNull(error);
            Assert.NotNull(error.Details);
            Assert.Equal("error message", error.Details.ToString());
        }
    }
}
