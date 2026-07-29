using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing
{
    /// <summary>
    ///     Error response detailing a problem. The format adheres to RFC 9457.
    /// </summary>
    public class ErrorResponse : BaseResponse, IErrorResponse
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ErrorResponse() { }

        /// <summary>
        ///     Constructor with all attributes.
        /// </summary>
        public ErrorResponse(int status, string title, string detail, string code, List<ErrorItem> errors)
        {
            Status = status;
            Title = title;
            Detail = detail;
            Code = code;
            Errors = errors;
        }

        /// <inheritdoc />
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("errors")]
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        ///     To make the error prettier to display.
        /// </summary>
        public override string ToString()
        {
            var result = new System.Text.StringBuilder();

            result.AppendLine("Error Details");
            result.AppendLine("=============");

            result.AppendLine($":HTTP Status: {Status}");
            result.AppendLine($":Title: {Title}");
            result.AppendLine($":Code: {Code}");
            result.AppendLine($":Detail: {Detail}");

            if (Errors != null && Errors.Count > 0)
            {
                result.AppendLine();
                result.AppendLine("Error Items");
                result.AppendLine("-----------");

                foreach (var (error, i) in Errors.Select((error, i) => (error, i)))
                {
                    result.AppendLine($"**Error {i + 1}:**");
                    result.AppendLine($"  :Pointer: {error.Pointer}");
                    result.AppendLine($"  :Detail: {error.Detail}");

                    if (i < Errors.Count - 1)
                        result.AppendLine();
                }
            }

            return result.ToString();
        }
    }
}
