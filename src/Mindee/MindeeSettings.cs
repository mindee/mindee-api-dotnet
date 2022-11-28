namespace Mindee
{
    /// <summary>
    /// Mindee's settings.
    /// </summary>
    public sealed class MindeeSettings
    {
        /// <summary>
        /// The api key.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// The Mindee base url.
        /// </summary>
        public string MindeeBaseUrl { get; set; } = string.Empty;
    }
}
