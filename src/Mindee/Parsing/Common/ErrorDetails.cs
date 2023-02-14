using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent an error information from the API response.
    /// </summary>

    [JsonConverter(typeof(ErrorDetailsJsonConverter))]
    public class ErrorDetails
    {
        /// <summary>
        /// A code to identify it.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"><see cref="Value"/></param>
        public ErrorDetails(string value)
        {
            Value = value;
        }

        /// <summary>
        /// To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
