using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Http;
using Mindee.Input;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests
{
    internal static class UnitTestBase
    {
        internal static PredictParameter GetFakePredictParameter()
        {
            return new PredictParameter(
                localSource: new LocalInputSource(
                    fileBytes: new byte[] { byte.MinValue },
                    filename: "titicaca.pdf"),
                urlSource: null,
                cropper: false,
                allWords: false);
        }

        internal static MindeeApi GetMindeeApi(string fileName)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText(fileName));

            return new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );
        }

        public static string CleaningFilenameFromResult(string expectedSummary)
        {
            var indexFilename = expectedSummary.IndexOf("Filename");
            var indexFilenameEOL = expectedSummary.IndexOf("\n", indexFilename);
            string cleanedSummary = expectedSummary.Remove(indexFilename, indexFilenameEOL - indexFilename + 1);

            return cleanedSummary;
        }
    }
}
