using Mindee.V2.Parsing;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Error Response")]
    public class ErrorResponseTest
    {
        [Fact(DisplayName = "should load and pretty print an error response")]
        public void RstOutput_mustBeValid()
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2ResourcePath + "errors/error_422_invalid_fields.json"));
            var response = localResponse.DeserializeResponse<ErrorResponse>();

            var rstOutput = File.ReadAllText(
                Constants.V2ResourcePath + "errors/error_422_invalid_fields.rst");

            Assert.NotNull(response);

            Assert.Equal(
                UnitTestBase.NormalizeLineEndings(rstOutput),
                UnitTestBase.NormalizeLineEndings(response.ToString())
            );
        }
    }
}
