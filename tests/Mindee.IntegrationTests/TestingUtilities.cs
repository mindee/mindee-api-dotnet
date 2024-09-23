using Microsoft.Extensions.DependencyInjection;
using Mindee.Extensions.DependencyInjection;

namespace Mindee.IntegrationTests
{
    public static class TestingUtilities
    {
        private static MindeeClient? _mindeeClient;

        /// <summary>g
        /// Gets the API version from an RST output
        /// </summary>
        /// <param name="rstStr">The RST output of a prediction.</param>
        /// <returns>The filename.</returns>
        public static string GetVersion(string rstStr)
        {
            int versionLineStartPos = rstStr.IndexOf(":Product: ", StringComparison.Ordinal);
            int versionEndPos = rstStr.IndexOf("\n", versionLineStartPos, StringComparison.Ordinal);

            string substring = rstStr.Substring(versionLineStartPos, versionEndPos - versionLineStartPos);
            int versionStartPos = substring.LastIndexOf(" v", StringComparison.Ordinal);

            return substring.Substring(versionStartPos + 2);
        }

        /// <summary>
        /// Gets the prediction ID name from an RST output
        /// </summary>
        /// <param name="rstStr">The RST output of a prediction.</param>
        /// <returns>The filename.</returns>
        public static string GetId(string rstStr)
        {
            int idStartPos = rstStr.IndexOf(":Mindee ID: ", StringComparison.Ordinal) + 12;
            int idEndPos = rstStr.IndexOf("\n:Filename:", StringComparison.Ordinal);

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }

        /// <summary>
        /// Gets the file name from an RST output
        /// </summary>
        /// <param name="rstStr">The RST output of a prediction.</param>
        /// <returns>The filename.</returns>
        public static string GetFileName(string rstStr)
        {
            int idStartPos = rstStr.IndexOf(":Filename: ", StringComparison.Ordinal) + 11;
            int idEndPos = rstStr.IndexOf("\n\nInference", StringComparison.Ordinal);

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }

        /// <summary>
        /// Gets or generates a Mindee client instance.
        /// </summary>
        /// <param name="apiKey">The API key for mindee.</param>
        /// <returns>A valid Mindee client instance.</returns>
        public static MindeeClient GetOrGenerateMindeeClient(string? apiKey)
        {
            if (_mindeeClient != null)
                return _mindeeClient;
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMindeeApi(options =>
            {
                options.ApiKey = apiKey;
            }, true);
            return _mindeeClient ??= new MindeeClient(apiKey);
        }

        /// <summary>
        /// Compute the Levenshtein distance between two strings.
        /// Taken from: https://rosettacode.org/wiki/Levenshtein_distance#C#
        /// </summary>
        /// <param name="refString">First string.</param>
        /// <param name="targetString">Second string.</param>
        /// <returns>The distance between the two strings.</returns>
        private static int LevenshteinDistance(string refString, string targetString)
        {
            int refLength = refString.Length;
            int targetLength = targetString.Length;
            int[,] distanceMatrix = new int[refLength + 1, targetLength + 1];

            if (refLength == 0)
            {
                return targetLength;
            }

            if (targetLength == 0)
            {
                return refLength;
            }

            for (int i = 0; i <= refLength; i++)
                distanceMatrix[i, 0] = i;
            for (int j = 0; j <= targetLength; j++)
                distanceMatrix[0, j] = j;

            for (int j = 1; j <= targetLength; j++)
            for (int i = 1; i <= refLength; i++)
                if (refString[i - 1] == targetString[j - 1])
                    distanceMatrix[i, j] = distanceMatrix[i - 1, j - 1];
                else
                    distanceMatrix[i, j] = Math.Min(Math.Min(
                            distanceMatrix[i - 1, j] + 1,
                            distanceMatrix[i, j - 1] + 1),
                        distanceMatrix[i - 1, j - 1] + 1
                    );
            return distanceMatrix[refLength, targetLength];
        }

        /// <summary>
        /// Computes the Levenshtein ratio between two given strings.
        /// </summary>
        /// <param name="refString">First string.</param>
        /// <param name="targetString">Second string.</param>
        /// <returns>The ratio of differences between the two strings.</returns>
        public static double LevenshteinRatio(string refString, string targetString)
        {
            int refLength = refString.Length;
            int targetLength = targetString.Length;
            int maxLength = int.Max(refLength, targetLength);
            if (refLength == 0 && targetLength == 0)
            {
                return 1.0;
            }

            return LevenshteinDistance(refString, targetString) / (double) maxLength;
        }
    }
}
