using Mindee.V2.Parsing;

namespace Mindee.UnitTests.V2.Parsing
{
    public class ErrorResponseTest
    {
        [Fact]
        public void RstOutput_mustBeValid()
        {
            var localResponse = new LocalResponse(
                File.ReadAllText(Constants.V2RootDir + "errors/error_422_invalid_fields.json"));
            var response = localResponse.DeserializeResponse<ErrorResponse>();

            var rstOutput = File.ReadAllText(
                Constants.V2RootDir + "errors/error_422_invalid_fields.rst");

            Assert.NotNull(response);

            Assert.Equal(
                UnitTestBase.NormalizeLineEndings(rstOutput),
                UnitTestBase.NormalizeLineEndings(response.ToString())
            );
        }
    }
}
