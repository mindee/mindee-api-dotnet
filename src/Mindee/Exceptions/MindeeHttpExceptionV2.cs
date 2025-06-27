using System;
using Mindee.Parsing.V2;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Representation of a Mindee API V2 exception.
    /// </summary>
    public class MindeeHttpExceptionV2 : Exception
    {
        /// <summary>
        /// Detail relevant to the error.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Specific error code.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the MindeeHttpException class using the provided Error object.
        /// </summary>
        /// <param name="error">Contents of the error object.</param>
        public MindeeHttpExceptionV2(ErrorResponse error) : base(error.Detail)
        {
            Detail = error.Detail;
            Status = error.Status;
        }
    }
}
