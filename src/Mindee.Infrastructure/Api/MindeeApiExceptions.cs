using System;

namespace Mindee.Infrastructure.Api
{
    public sealed class MindeeApiException : Exception
    {
        public MindeeApiException()
        {
        }

        public MindeeApiException(string message) : base(message)
        {
        }

        public MindeeApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
