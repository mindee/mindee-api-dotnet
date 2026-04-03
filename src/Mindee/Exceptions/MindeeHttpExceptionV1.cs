using System.Text.Json.Nodes;
using Mindee.V1.Parsing.Common;

namespace Mindee.Exceptions
{
    /// <summary>
    ///     Error sub-object.
    /// </summary>
    public class MindeeHttpExceptionV1 : MindeeException
    {
        /// <summary>
        /// Name of the error.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Additional details on the error.
        /// </summary>
        public ErrorDetails Details { get; set; }

        /// <summary>
        /// Additional details on the error.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error object for V1.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="code"></param>
        public MindeeHttpExceptionV1(string name, string message, JsonNode details, int code)
            : base(message)
        {
            Name = name;
            Details = new ErrorDetails(details["value"]?.GetValue<string>());
            Code = code;
        }


        /// <summary>
        /// Error object for V1.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <param name="code"></param>
        public MindeeHttpExceptionV1(string name, string message, ErrorDetails details, int code)
            : base(message)
        {
            Name = name;
            Details = details;
            Code = code;
        }

    }
}
