using System;
using Mindee.Exceptions;
using Mindee.Input;

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
        /// Whether to include the full text data for async APIs.
        /// This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        public bool FullText { get; }

        /// <summary>
        /// Whether to include cropper results for each page.
        /// This performs a cropping operation on the server and will increase response time.
        /// </summary>
        /// <remarks>It is not available in API builder.</remarks>
        public bool Cropper { get; }

        /// <summary>
        /// If set, will enqueue to a workflow queue instead of a product's endpoint.
        /// </summary>
        public string WorkflowId { get; }

        /// <summary>
        /// Options to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="allWords"><see cref="AllWords"/></param>
        /// <param name="cropper"><see cref="Cropper"/></param>
        /// <param name="fullText"><see cref="FullText"/></param>
        /// <param name="workflowId"><see cref="WorkflowId"/></param>
        public PredictOptions(
            bool allWords = false,
            bool fullText = false,
            bool cropper = false,
            string workflowId = null
        )
        {
            AllWords = allWords;
            Cropper = cropper;
            FullText = fullText;
            WorkflowId = workflowId;
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
        /// Wait this many milliseconds between each polling attempt.
        /// </summary>
        public int InitialDelayMilliSec { get; }

        /// <summary>
        /// Wait this many milliseconds before the first polling attempt.
        /// </summary>
        public int IntervalMilliSec { get; }

        /// <summary>
        /// Options to pass for asynchronous parsing with polling.
        /// </summary>
        /// <param name="initialDelaySec"><see cref="InitialDelaySec"/></param>
        /// <param name="intervalSec"><see cref="IntervalSec"/></param>
        /// <param name="maxRetries"><see cref="MaxRetries"/></param>
        public AsyncPollingOptions(double initialDelaySec = 2.0, double intervalSec = 1.5, int maxRetries = 80)
        {
            double minInitialDelaySec = 1.0;
            double minIntervalSec = 1.0;
            int minRetries = 2;
            if (initialDelaySec < minInitialDelaySec)
            {
                throw new MindeeException(
                    $"Cannot set initial polling delay to less than {Math.Floor(minInitialDelaySec)} second(s).");
            }

            if (intervalSec < minIntervalSec)
            {
                throw new MindeeException(
                    $"Cannot set polling interval to less than {Math.Floor(minIntervalSec)} second(s).");
            }

            if (maxRetries < minRetries)
            {
                throw new MindeeException($"Cannot set async retry to less than {minRetries} attempts.");
            }

            InitialDelaySec = initialDelaySec;
            IntervalSec = intervalSec;
            MaxRetries = maxRetries;
            InitialDelayMilliSec = (int)Math.Floor(InitialDelaySec * 1000);
            IntervalMilliSec = (int)Math.Floor(IntervalSec * 1000);
        }
    }

    /// <summary>
    /// Options for workflow executions.
    /// </summary>
    public sealed class WorkflowOptions
    {
        /// <summary>
        /// Alias to give to the file.
        /// </summary>
        public string Alias { get; }


        /// <summary>
        /// Priority to give to the execution.
        /// </summary>
        public ExecutionPriority? Priority { get; }


        /// <summary>
        /// Whether to include the full text data for async APIs.
        /// This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        public bool FullText { get; }

        /// <summary>
        /// A unique, encrypted URL for accessing the document validation interface without requiring authentication.
        /// </summary>
        public string PublicUrl { get; }

        /// <summary>
        /// Whether to enable Retrieval-Augmented Generation.
        /// </summary>
        public bool Rag { get; }

        /// <summary>
        /// Options for workflow executions.
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="priority"></param>
        /// <param name="fullText"></param>
        /// <param name="publicUrl"></param>
        /// <param name="rag"></param>
        public WorkflowOptions(
            string alias = null,
            ExecutionPriority? priority = null,
            bool fullText = false,
            string publicUrl = null,
            bool rag = false
        )
        {
            Alias = alias;
            Priority = priority;
            FullText = fullText;
            PublicUrl = publicUrl;
            Rag = rag;
        }
    }
}
