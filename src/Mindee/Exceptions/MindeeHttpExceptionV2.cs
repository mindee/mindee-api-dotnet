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
        /// <param name="status">Status code of the error.</param>
        /// <param name="detail">Detail sent alongside the error.</param>
        public MindeeHttpExceptionV2(int status, String detail) : base(detail)
        {
            Detail = detail;
            Status = status;
        }
    }
}
