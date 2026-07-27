using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using Mindee.Exceptions;
using Mindee.V1.Parsing.Common;

namespace Mindee.V1.Exceptions
{
    /// <summary>
    ///     Error sub-object.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S3376:Classes should not be empty",
        Justification = "Would be breaking to remove right now. TODO: remove it.")]
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

        /// <inheritdoc />
        public MindeeHttpExceptionV1()
        {
        }

        /// <inheritdoc />
        public MindeeHttpExceptionV1(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public MindeeHttpExceptionV1(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
