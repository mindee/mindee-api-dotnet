using System;

namespace Mindee.Exceptions
{
    /// <summary>
    ///     Represent a 400 error, bad requests or malformed, from Mindee API.
    /// </summary>
    public class MindeeInputException : MindeeException
    {
        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public MindeeInputException()
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public MindeeInputException(string message) : base(message)
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public MindeeInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
