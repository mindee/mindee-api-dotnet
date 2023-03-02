using System;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Represent a 429 error, too many requests, from Mindee API.
    /// </summary>
    public class Mindee429Exception : MindeeException
    {
        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee429Exception()
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee429Exception(string message) : base(message)
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee429Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
