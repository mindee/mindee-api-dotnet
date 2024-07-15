namespace Mindee.IntegrationTests
{
    public class TestingUtilities
    {
        public static string GetVersion(string rstStr)
        {
            int versionLineStartPos = rstStr.IndexOf(":Product: ", StringComparison.Ordinal);
            int versionEndPos = rstStr.IndexOf("\n", versionLineStartPos, StringComparison.Ordinal);

            string substring = rstStr.Substring(versionLineStartPos, versionEndPos - versionLineStartPos);
            int versionStartPos = substring.LastIndexOf(" v", StringComparison.Ordinal);

            return substring.Substring(versionStartPos + 2);
        }

        public static string GetId(string rstStr)
        {
            int idStartPos = rstStr.IndexOf(":Mindee ID: ", StringComparison.Ordinal) + 12;
            int idEndPos = rstStr.IndexOf("\n:Filename:", StringComparison.Ordinal);

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }

        public static string GetFileName(string rstStr)
        {
            int idStartPos = rstStr.IndexOf(":Filename: ", StringComparison.Ordinal) + 11;
            int idEndPos = rstStr.IndexOf("\n\nInference", StringComparison.Ordinal);

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }
    }
}
