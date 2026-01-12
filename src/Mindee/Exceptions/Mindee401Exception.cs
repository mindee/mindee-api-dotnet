using System;

namespace Mindee.Exceptions
{
    /// <summary>
    ///     Represent a 401 error, unauthorized, from Mindee API.
    /// </summary>
    public class Mindee401Exception : MindeeException
    {
        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee401Exception()
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee401Exception(string message) : base(message)
        {
        }

        /// <summary>
        ///     <see cref="Exception" />
        /// </summary>
        public Mindee401Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
