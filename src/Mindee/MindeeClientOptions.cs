using Mindee.Exceptions;
using System;

namespace Mindee
{
    /// <summary>
    /// Options to pass when calling methods using the predict API.
    /// </summary>
    public sealed class PredictOptions
    {
        /// <summary>
        /// Whether to include all the words on each page.
        /// This performs a full OCR operation on the server and will increase response time.
        /// </summary>
        /// <remarks>It is not available on all API.</remarks>
        public bool AllWords { get; }

        /// <summary>
        /// Whether to include cropper results for each page.
        /// This performs a cropping operation on the server and will increase response time.
        /// </summary>
        /// <remarks>It is not available in API builder.</remarks>
        public bool Cropper { get; }

        /// <summary>
        /// Options to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="allWords"><see cref="AllWords"/></param>
        /// <param name="cropper"><see cref="Cropper"/></param>
        public PredictOptions(bool allWords = false, bool cropper = false)
        {
            AllWords = allWords;
            Cropper = cropper;
        }
    }

    /// <summary>
    /// Options to pass for asynchronous parsing with polling.
    /// </summary>
    public sealed class AsyncPollingOptions
    {
        /// <summary>
        /// Wait this many seconds before the first polling attempt.
        /// </summary>
        public double InitialDelaySec { get; }

        /// <summary>
        /// Wait this many seconds between each polling attempt.
        /// </summary>
        public double IntervalSec { get; }

        /// <summary>
        /// Maximum number of times to poll.
        /// </summary>
        public int MaxRetries { get; }

        /// <summary>
        /// Options to pass for asynchronous parsing with polling.
        /// </summary>
        /// <param name="initialDelaySec"><see cref="InitialDelaySec"/></param>
        /// <param name="intervalSec"><see cref="IntervalSec"/></param>
        /// <param name="maxRetries"><see cref="MaxRetries"/></param>
        public AsyncPollingOptions(double initialDelaySec = 4.0, double intervalSec = 2.0, int maxRetries = 30)
        {
            double minInitialDelaySec = 1;
            double minIntervalSec = 2;
            int minRetries = 2;
            if (initialDelaySec < minInitialDelaySec)
            {
                throw new MindeeException($"Cannot set initial polling delay to less than {Math.Floor(minInitialDelaySec)} seconds.");
            }
            if (intervalSec < minIntervalSec)
            {
                throw new MindeeException($"Cannot set polling interval to less than {Math.Floor(minIntervalSec)} seconds.");
            }
            if (maxRetries < minRetries){
                throw new MindeeException($"Cannot set async retry to less than {minRetries} attempts.");
            }
            InitialDelaySec = initialDelaySec;
            IntervalSec = intervalSec;
            MaxRetries = maxRetries;
        }
    }
}
