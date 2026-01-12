using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
// ReSharper disable once RedundantUsingDirective
using Mindee.Extensions.DependencyInjection;

namespace Mindee.IntegrationTests
{
    public static class TestingUtilities
    {
        private static MindeeClient? _mindeeClient;
        private static MindeeClientV2? _mindeeClientV2;

        /// <summary>
        ///     g
        ///     Gets the API version from an RST output
        /// </summary>
        /// <param name="rstStr">The RST output of a prediction.</param>
        /// <returns>The filename.</returns>
        public static string GetVersion(string rstStr)
        {
            var versionLineStartPos = rstStr.IndexOf(":Product: ", StringComparison.Ordinal);
            var versionEndPos = rstStr.IndexOf("\n", versionLineStartPos, StringComparison.Ordinal);

            var substring = rstStr.Substring(versionLineStartPos, versionEndPos - versionLineStartPos);
            var versionStartPos = substring.LastIndexOf(" v", StringComparison.Ordinal);

            return substring.Substring(versionStartPos + 2);
        }

        /// <summary>
        ///     Gets the prediction ID name from an RST output
        /// </summary>
        /// <param name="rstStr">The RST output of a prediction.</param>
        /// <returns>The filename.</returns>
        public static string GetId(string rstStr)
        {
            var idStartPos = rstStr.IndexOf(":Mindee ID: ", StringComparison.Ordinal) + 12;
            var idEndPos = rstStr.IndexOf("\n:Filename:", StringComparison.Ordinal);

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }

        /// <summary>
        ///     Gets the file name from an RST output
        /// </summary>
        /// <param name="rstStr">The RST output of a prediction.</param>
        /// <returns>The filename.</returns>
        public static string GetFileName(string rstStr)
        {
            var idStartPos = rstStr.IndexOf(":Filename: ", StringComparison.Ordinal) + 11;
            var idEndPos = rstStr.IndexOf("\n\nInference", StringComparison.Ordinal);

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }

        /// <summary>
        ///     Gets or generates a Mindee client instance.
        /// </summary>
        /// <param name="apiKey">The API key for mindee.</param>
        /// <returns>A valid Mindee client instance.</returns>
        public static MindeeClient GetOrGenerateMindeeClient(string? apiKey)
        {
            if (_mindeeClient != null)
            {
                return _mindeeClient;
            }

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApi(options =>
            {
                options.ApiKey = apiKey;
            }, true);
            return _mindeeClient ??= new MindeeClient(apiKey);
        }

        /// <summary>
        ///     Gets or generates a Mindee client V2 instance.
        /// </summary>
        /// <param name="apiKey">The API key for mindee.</param>
        /// <returns>A valid Mindee client V2 instance.</returns>
        public static MindeeClientV2 GetOrGenerateMindeeClientV2(string? apiKey)
        {
            if (_mindeeClientV2 != null)
            {
                return _mindeeClientV2;
            }

            var serviceCollection = new ServiceCollection();
            var logger = NullLoggerFactory.Instance;
            serviceCollection.AddMindeeApiV2(options =>
            {
                options.ApiKey = apiKey;
            }, logger, true);
            return _mindeeClientV2 ??= new MindeeClientV2(apiKey);
        }

        /// <summary>
        ///     Compute the Levenshtein distance between two strings.
        ///     Taken from: https://rosettacode.org/wiki/Levenshtein_distance#C#
        /// </summary>
        /// <param name="refString">First string.</param>
        /// <param name="targetString">Second string.</param>
        /// <returns>The distance between the two strings.</returns>
        private static int LevenshteinDistance(string refString, string targetString)
        {
            var refLength = refString.Length;
            var targetLength = targetString.Length;
            var distanceMatrix = new int[refLength + 1, targetLength + 1];

            if (refLength == 0)
            {
                return targetLength;
            }

            if (targetLength == 0)
            {
                return refLength;
            }

            for (var i = 0; i <= refLength; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (var j = 0; j <= targetLength; j++)
            {
                distanceMatrix[0, j] = j;
            }

            for (var j = 1; j <= targetLength; j++)
                for (var i = 1; i <= refLength; i++)
                {
                    if (refString[i - 1] == targetString[j - 1])
                    {
                        distanceMatrix[i, j] = distanceMatrix[i - 1, j - 1];
                    }
                    else
                    {
                        distanceMatrix[i, j] = Math.Min(Math.Min(
                                distanceMatrix[i - 1, j] + 1,
                                distanceMatrix[i, j - 1] + 1),
                            distanceMatrix[i - 1, j - 1] + 1
                        );
                    }
                }

            return distanceMatrix[refLength, targetLength];
        }

        /// <summary>
        ///     Computes the Levenshtein ratio between two given strings.
        /// </summary>
        /// <param name="refString">First string.</param>
        /// <param name="targetString">Second string.</param>
        /// <returns>The ratio of differences between the two strings.</returns>
        public static double LevenshteinRatio(string refString, string targetString)
        {
            var refLength = refString.Length;
            var targetLength = targetString.Length;
            var maxLength = Math.Max(refLength, targetLength);
            if (refLength == 0 && targetLength == 0)
            {
                return 1.0;
            }

            return 1.0 - (LevenshteinDistance(refString, targetString) / (double)maxLength);
        }
    }
}
