using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represents the server response after a document is sent to a workflow.
    /// </summary>
    public class WorkflowResponse<TInferenceModel> where TInferenceModel : class, new()
    {
        /// <summary>
        /// Set the prediction model used to parse the document.
        /// </summary>
        [JsonPropertyName(("execution"))]
        public Execution<TInferenceModel> Execution { get; set; }


        /// <summary>
        /// Default product is GeneratedV1.
        /// </summary>
        public class Default : WorkflowResponse<GeneratedV1> { }
    }
}
