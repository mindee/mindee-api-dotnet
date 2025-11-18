using System;
using System.Collections.Generic;
using Mindee.Parsing.V2;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Representation of a Mindee API V2 exception.
    /// </summary>
    public class MindeeHttpExceptionV2 : Exception, IErrorResponse
    {
        /// <inheritdoc/>
        public string Detail { get; set; }

        /// <inheritdoc/>
        public int Status { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string Code { get; set; }

        /// <inheritdoc/>
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        /// Initialize an instance using the provided Error object.
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
    }
}
