using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represents the server response after a document is sent to a workflow.
    /// </summary>
    public class WorkflowResponse<TModel> : CommonResponse where TModel : class, new()
    {
        /// <summary>
        /// Set the prediction model used to parse the document.
        /// </summary>
        [JsonPropertyName(("execution"))]
        public Execution<TModel> Execution { get; set; }


        /// <summary>
        /// Default product is GeneratedV1.
        /// </summary>
        public class Default : WorkflowResponse<GeneratedV1>
        {
        }
    }
}
