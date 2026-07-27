using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Mindee.V2.Parsing;

namespace Mindee.V2.Exceptions
{
    /// <summary>
    ///     Representation of a Mindee API V2 exception.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S3376:Classes should not be empty",
        Justification = "Would be breaking to remove right now. TODO: remove it.")]
    public class MindeeHttpExceptionV2 : Exception, IErrorResponse
    {
        /// <summary>
        ///     Initialize an instance using the provided Error object.
        /// </summary>
        /// <param name="error">ErrorResponse object.</param>
        public MindeeHttpExceptionV2(ErrorResponse error)
            : base($"HTTP {error.Status} - {error.Title} :: {error.Code} - {error.Detail}")
        {
            Detail = error.Detail;
            Status = error.Status;
            Title = error.Title;
            Code = error.Code;
            Errors = error.Errors;
        }

        /// <inheritdoc />
        public MindeeHttpExceptionV2()
        {
        }

        /// <inheritdoc />
        public MindeeHttpExceptionV2(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public MindeeHttpExceptionV2(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc />
        public string Detail { get; set; }

        /// <inheritdoc />
        public int Status { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string Code { get; set; }

        /// <inheritdoc />
        public List<ErrorItem> Errors { get; set; }
    }
}
