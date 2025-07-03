using System;
using Mindee.Input;

namespace Mindee.Http
{
    /// <summary>
    /// Parameter required to use the predict feature.
    /// </summary>
    public class PredictParameter : GenericParameter
    {
        /// <summary>
        /// Whether to retrieve the full text OCR on OTS products.
        /// </summary>
        /// <remarks>Not available on all APIs.</remarks>
        public bool AllWords { get; }

        /// <summary>
        /// Whether to include a cropping operation to the file.
        /// </summary>
        public bool Cropper { get; }


        /// <summary>
        /// Result parameters for requests.
        /// </summary>
        /// <param name="localSource">Local input source containing the file.<see cref="GenericParameter.LocalSource"/></param>
        /// <param name="urlSource">Source URL to use.<see cref="GenericParameter.UrlSource"/></param>
        /// <param name="allWords">Whether to include the full OCR response in the payload (compatible APIs only).<see cref="AllWords"/></param>
        /// <param name="fullText">Whether to include the full text in the payload (compatible APIs only)<see cref="GenericParameter.FullText"/></param>
        /// <param name="cropper">Whether to crop the document before enqueuing on the API.<see cref="Cropper"/></param>
        /// <param name="workflowId">If set, will enqueue to the workflows queue.<see cref="GenericParameter.WorkflowId"/></param>
        /// <param name="rag">If set, will enqueue to the workflows queue.<see cref="GenericParameter.Rag"/></param>
        public PredictParameter(
            LocalInputSource localSource,
            UrlInputSource urlSource,
            bool allWords,
            bool fullText,
            bool cropper, string workflowId,
            bool rag) : base(localSource, urlSource, fullText, workflowId, rag)
        {
            AllWords = allWords;
            Cropper = cropper;
        }
    }
}
