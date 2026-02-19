namespace Mindee.V1.Http
{
    /// <summary>
    ///     Mindee settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        ///     The API key.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        ///     The Mindee base URL.
        /// </summary>
        public string MindeeBaseUrl { get; set; } = string.Empty;

        /// <summary>
        ///     The maximum request duration in seconds.
        /// </summary>
        public int RequestTimeoutSeconds { get; set; } = 120;
    }
}
