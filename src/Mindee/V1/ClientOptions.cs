using System;
using Mindee.Exceptions;
using Mindee.V1.Parsing.Common;

namespace Mindee.V1
{
    /// <summary>
    ///     ResultOptions to pass when calling methods using the predict API.
    /// </summary>
    public sealed class PredictOptions
    {
        /// <summary>
        ///     ResultOptions to pass when calling methods using the predict API.
        /// </summary>
        /// <param name="allWords">
        ///     <see cref="AllWords" />
        /// </param>
        /// <param name="cropper">
        ///     <see cref="Cropper" />
        /// </param>
        /// <param name="fullText">
        ///     <see cref="FullText" />
        /// </param>
        /// <param name="workflowId">
        ///     <see cref="WorkflowId" />
        /// </param>
        /// <param name="rag">
        ///     <see cref="Rag" />
        /// </param>
        public PredictOptions(
            bool allWords = false,
            bool fullText = false,
            bool cropper = false,
            string workflowId = null,
            bool rag = false
        )
        {
            AllWords = allWords;
            Cropper = cropper;
            FullText = fullText;
            WorkflowId = workflowId;
            Rag = rag;
        }

        /// <summary>
        ///     Whether to include all the words on each page.
        ///     This performs a full OCR operation on the server and will increase response time.
        /// </summary>
        /// <remarks>Not available on all APIs.</remarks>
        public bool AllWords { get; }

        /// <summary>
        ///     Whether to include the full text data for async APIs.
        ///     This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        public bool FullText { get; }

        /// <summary>
        ///     Whether to include cropper results for each page.
        ///     This performs a cropping operation on the server and will increase response time.
        /// </summary>
        public bool Cropper { get; }

        /// <summary>
        ///     If set, will enqueue to a workflow queue instead of a product's endpoint.
        /// </summary>
        public string WorkflowId { get; }


        /// <summary>
        ///     If set, will enable Retrieval-Augmented Generation.
        /// </summary>
        public bool Rag { get; }
    }

    /// <summary>
    ///     ResultOptions for workflow executions.
    /// </summary>
    public sealed class WorkflowOptions
    {
        /// <summary>
        ///     ResultOptions for workflow executions.
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

        /// <summary>
        ///     Alias to give to the file.
        /// </summary>
        public string Alias { get; }


        /// <summary>
        ///     Priority to give to the execution.
        /// </summary>
        public ExecutionPriority? Priority { get; }


        /// <summary>
        ///     Whether to include the full text data for async APIs.
        ///     This performs a full OCR operation on the server and will increase response time and payload size.
        /// </summary>
        public bool FullText { get; }

        /// <summary>
        ///     A unique, encrypted URL for accessing the document validation interface without requiring authentication.
        /// </summary>
        public string PublicUrl { get; }

        /// <summary>
        ///     Whether to enable Retrieval-Augmented Generation.
        /// </summary>
        public bool Rag { get; }
    }
}
