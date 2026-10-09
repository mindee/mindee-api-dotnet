using Microsoft.Extensions.Logging;

namespace Mindee
{
    /// <summary>
    ///     Legacy Static Mindee logger, only used in V1.
    ///     TODO: refactor to delete this class.
    /// </summary>
    public static class MindeeLogger
    {
        private static ILogger _instance;

        /// <summary>
        ///     Assign a LoggerFactory.
        /// </summary>
        /// <param name="loggerFactory">
        ///     <c>ILoggerFactory</c>
        /// </param>
        public static void Assign(ILoggerFactory loggerFactory)
        {
            _instance = loggerFactory.CreateLogger("MindeeClient");
            _instance.LogDebug("Logger initialized");
        }

        /// <summary>
        ///     Get the logger instance.
        ///     Will be null if a logger factory has not been <see cref="Assign" />.
        /// </summary>
        public static ILogger GetLogger()
        {
            return _instance;
        }
    }
}
