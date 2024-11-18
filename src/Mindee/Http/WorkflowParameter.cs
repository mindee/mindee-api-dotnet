using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the workflow feature.
    /// </summary>
    public class WorkflowParameter : GenericParameter
    {
        /// <summary>
        /// Alias to give to the file.
        /// </summary>
        public string Alias { get; }


        /// <summary>
        /// Priority to give to the execution.
        /// </summary>
        public string Priority { get; }

        /// <summary>
        /// Workflow parameters.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="GenericParameter.LocalSource"/></param>
        /// <param name="urlSource">Source URL to use.<see cref="GenericParameter.UrlSource"/></param>
        /// <param name="fullText">Whether to include the full text in the payload (compatible APIs only)<see cref="GenericParameter.FullText"/></param>
        /// <param name="alias">Alias to give to the document.<see cref="Alias"/></param>
        /// <param name="priority">Priority to give to the document.<see cref="Priority"/></param>
        public WorkflowParameter(
            LocalInputSource localSource,
            UrlInputSource urlSource, bool fullText,
            string alias, string priority) : base(localSource, urlSource,
            fullText)
        {
            Alias = alias;
            Priority = priority;
        }
    }
}
