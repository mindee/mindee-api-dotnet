using System;

namespace Mindee.Exceptions
{
    /// <summary>
    ///     Represent a 500 error from Mindee API.
    /// </summary>
    public class Mindee500Exception : MindeeException
    {
        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee500Exception()
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee500Exception(string message) : base(message)
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee500Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
