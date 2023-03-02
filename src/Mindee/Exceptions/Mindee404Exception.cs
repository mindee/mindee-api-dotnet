using System;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Represent a 404 error, resource not found, from Mindee API.
    /// </summary>
    public class Mindee404Exception : MindeeException
    {
        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee404Exception()
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee404Exception(string message) : base(message)
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public Mindee404Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
