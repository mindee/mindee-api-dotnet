namespace Mindee.IntegrationTests
{
    public class TestingUtilities
    {

        public static string GetVersion(string rstStr)
        {
            int versionLineStartPos = rstStr.IndexOf(":Product: ");
            int versionEndPos = rstStr.IndexOf("\n", versionLineStartPos);

            string substring = rstStr.Substring(versionLineStartPos, versionEndPos - versionLineStartPos);
            int versionStartPos = substring.LastIndexOf(" v");

            return substring.Substring(versionStartPos + 2);
        }

        public static string GetId(string rstStr)
        {
            int idStartPos = rstStr.IndexOf(":Mindee ID: ") + 12;
            int idEndPos = rstStr.IndexOf("\n:Filename:");

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }

        public static string GetFileName(string rstStr)
        {
            int idStartPos = rstStr.IndexOf(":Filename: ") + 11;
            int idEndPos = rstStr.IndexOf("\n\nInference");

            return rstStr.Substring(idStartPos, idEndPos - idStartPos);
        }
    }
}
