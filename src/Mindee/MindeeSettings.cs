namespace Mindee
{
    /// <summary>
    /// Mindee settings.
    /// </summary>
    public class MindeeSettings
    {
        /// <summary>
        /// The API key.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// The Mindee base URL.
        /// </summary>
        public string MindeeBaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// The maximum request duration in seconds.
        /// </summary>
        public int RequestTimeoutSeconds { get; set; } = 120;
    }
}
