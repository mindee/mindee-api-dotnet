using System.Collections.Generic;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Error response detailing a problem. The format adheres to RFC 9457.
    /// </summary>
    public interface IErrorResponse
    {
        /// <summary>
        /// The HTTP status code returned by the server.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// A human-readable explanation specific to the occurrence of the problem.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A machine-readable code specific to the occurrence of the problem.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A list of explicit details on the problem.
        /// </summary>
        public List<ErrorItem> Errors { get; set; }
    }
}
