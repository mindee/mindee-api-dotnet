using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Mindee.ClientOptions
{
    /// <summary>
    /// Base class for all clients.
    /// </summary>
    public abstract class BaseClient
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected readonly ILogger Logger;
        /// <summary>
        /// The factory used to create the <see cref="Logger"/>.
        /// </summary>
        protected readonly ILoggerFactory LoggerFactory;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="loggerFactory">Factory for the logger.</param>
        protected BaseClient(ILoggerFactory loggerFactory = null)
        {
            LoggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
            Logger = LoggerFactory.CreateLogger(GetType());
        }

        /// <summary>
        /// Wait and retry logic for polling operations.
        /// </summary>
        protected async Task ThrottlePollingAttemptAsync(
            int tryCounter,
            int maxRetries,
            BasePollingOptions pollingOptions,
            CancellationToken cancellationToken)
        {
            if (tryCounter >= maxRetries)
            {
                Logger?.LogDebug("Not waiting, max retries reached.");
                return;
            }

            Logger?.LogDebug($"Waiting {pollingOptions.IntervalMilliSec} before next attempt...");
            await Task.Delay(pollingOptions.IntervalMilliSec, cancellationToken);
        }
    }
}
