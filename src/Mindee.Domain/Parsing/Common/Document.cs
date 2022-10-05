namespace Mindee.Domain.Parsing.Common
{
    /// <summary>
    /// Define the parsed document.
    /// </summary>
    /// <typeparam name="TPredictionModel">The prediction model which defines values.</typeparam>
    public class Document<TPredictionModel>
        where TPredictionModel : class, new()
    {
        /// <summary>
        /// <see cref="Inference{TPredictionModel}"/>
        /// </summary>
        public Inference<TPredictionModel> Inference { get; set; }

        /// <summary>
        /// <see cref="Ocr"/>
        /// </summary>
        public Ocr Ocr { get; set; }
    }
}
