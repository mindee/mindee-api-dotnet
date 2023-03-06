using System;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Represent a 403 error, forbidden, from Mindee API.
    /// </summary>
    public class Mindee403Exception : MindeeException
    {
        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee403Exception()
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee403Exception(string message) : base(message)
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee403Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
