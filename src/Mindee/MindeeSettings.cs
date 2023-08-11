namespace Mindee
{
    /// <summary>
    /// Mindee settings.
    /// </summary>
    public sealed class MindeeSettings
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
        /// The request to Mindee API should time out about.
        /// </summary>
        public int RequestTimeoutSeconds { get; set; } = 120;
    }
}
