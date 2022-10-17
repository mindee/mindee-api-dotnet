using System;

namespace Mindee.Exceptions
{
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
