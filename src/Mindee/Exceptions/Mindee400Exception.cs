using System;

namespace Mindee.Exceptions
{
    /// <summary>
    ///     Represent a 400 error, bad requests or malformed, from Mindee API.
    /// </summary>
    public class Mindee400Exception : MindeeException
    {
        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee400Exception()
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee400Exception(string message) : base(message)
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee400Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
