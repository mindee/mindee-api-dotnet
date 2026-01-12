using Microsoft.Extensions.Logging;

namespace Mindee
{
    /// <summary>
    ///     Global Mindee logger.
    /// </summary>
    public class MindeeLogger
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
