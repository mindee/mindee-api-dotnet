using System.Text.RegularExpressions;

namespace Mindee.Parsing.Common
{
    internal static class SummaryHelper
    {
        public static string Clean(string summary)
        {
            Regex cleanSpace = new Regex(" \n", RegexOptions.Multiline);

            return cleanSpace.Replace(summary, "\n");
        }
    }
}
