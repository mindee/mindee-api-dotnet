namespace Mindee.Exceptions
{
    /// <summary>
    /// Error sub-object.
    /// </summary>
    public class MindeeHttpExceptionError
    {
        /// <summary>
        /// Pointer to the problem node.
        /// </summary>
        public string Pointer { get; set; }

        /// <summary>
        /// Error detail description.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MindeeHttpExceptionError"/> class.
        /// </summary>
        public MindeeHttpExceptionError()
        {
            Pointer = string.Empty;
            Detail = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MindeeHttpExceptionError"/> class.
        /// </summary>
        /// <param name="pointer">The pointer to the problem node.</param>
        /// <param name="detail">The error detail description.</param>
        public MindeeHttpExceptionError(string pointer, string detail)
        {
            Pointer = pointer;
            Detail = detail;
        }
    }
}
