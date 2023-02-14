using System;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Represent a Mindee exception.
    /// </summary>
    public sealed class MindeeException : Exception
    {
        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public MindeeException()
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public MindeeException(string message) : base(message)
        {
        }

        /// <summary>
        /// <see cref="Exception"/>
        /// </summary>
        public MindeeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
