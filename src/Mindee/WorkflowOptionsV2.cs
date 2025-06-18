using System.Collections.Generic;

namespace Mindee
{
    /// <summary>
    /// Options for a workflow polling call.
    /// </summary>
    public sealed class WorkflowOptionsV2 : PredictOptionsV2
    {
        /// <summary>
        /// ID of the workflow.
        /// </summary>
        public string WorkflowId { get; }

        /// <summary>
        /// Simple wrapper class for PredictOptionsV2, as they are functionally the same for the API.
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="fullText"></param>
        /// <param name="cropper"></param>
        /// <param name="alias"></param>
        /// <param name="webhookIds"></param>
        /// <param name="rag"></param>
        public WorkflowOptionsV2(
            string workflowId,
            bool fullText = false,
            bool cropper = false,
            string alias = null,
            List<string> webhookIds = null,
            bool rag = false
        ) : base(workflowId, fullText, cropper, alias, webhookIds, rag)
        {
            WorkflowId = workflowId;
        }
    }
}
