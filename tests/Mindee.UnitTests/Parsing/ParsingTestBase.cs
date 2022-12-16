using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Parsing;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Parsing
{
    internal static class ParsingTestBase
    {
        internal static PredictParameter GetFakePredictParameter()
        {
            return
                new PredictParameter(
                    new byte[] { byte.MinValue },
                        "Bou");
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
