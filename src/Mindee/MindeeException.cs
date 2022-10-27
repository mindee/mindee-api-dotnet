using System;

namespace Mindee.Exceptions
{
    /// <summary>
    /// Represent a Mindee exception.
    /// </summary>
    public sealed class MindeeException : Exception
    {
        public MindeeException()
        {
        }

        public MindeeException(string message) : base(message)
        {
        }

        public MindeeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
