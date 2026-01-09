#nullable enable

using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using Mindee.Parsing.V2;

namespace Mindee.Http
{
    /// <summary>
    ///     Communicate with the Mindee HTTP API V2.
    ///     <p>
    ///         You may use this base class to make your own custom class.
    ///         However, we may introduce breaking changes in minor versions as needed.
    ///     </p>
    /// </summary>
    public abstract class HttpApiV2
    {
        /// <summary>
        ///     Set this to your logger.
        /// </summary>
        protected ILogger<HttpApiV2>? Logger;

        /// <summary>
        ///     Do a prediction according parameters for custom model defined in the Studio.
        /// </summary>
        /// <param name="predictParameter">
        ///     <see cref="InferencePostParameters" />
        /// </param>
        public abstract Task<JobResponse> ReqPostEnqueueInferenceAsync(InferencePostParameters predictParameter);

        /// <summary>
        ///     Get a job for an enqueued document.
        /// </summary>
        /// <param name="jobId">The job ID as returned by the predict_async route.</param>
        public abstract Task<JobResponse> ReqGetJobAsync(string jobId);

        /// <summary>
        ///     Get a document inference.
        /// </summary>
        /// <param name="inferenceId">The inference ID as given by the job.</param>
        public abstract Task<InferenceResponse> ReqGetInferenceAsync(string inferenceId);

        /// <summary>
        ///     Get the error from the server return.
        /// </summary>
        /// <param name="responseContent">HTTP Status of the response</param>
        /// <param name="statusCode">String content of the response</param>
        /// <exception cref="MindeeHttpExceptionV2"></exception>
        protected ErrorResponse GetErrorFromContent(int statusCode, string? responseContent)
        {
            Logger?.LogInformation("Parsing error response ...");
            try
            {
                if (responseContent != null && responseContent.Contains("\"status\":"))
                {
                    var error = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                    if (error != null)
                    {
                        return error;
                    }
                }

                return MakeUnknownError(statusCode, responseContent);
            }
            catch (JsonException)
            {
                return MakeUnknownError(statusCode, responseContent);
            }
        }

        /// <summary>
        ///     Attempt to deserialize a response from the server.
        /// </summary>
        /// <param name="responseContent"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        protected TResponse DeserializeResponse<TResponse>(string? responseContent)
            where TResponse : CommonResponse, new()
        {
            Logger?.LogInformation("Parsing HTTP 2xx response ...");

            if (responseContent == null || string.IsNullOrWhiteSpace(responseContent))
            {
                throw new MindeeException("Empty response from server.");
            }

            var model = JsonSerializer.Deserialize<TResponse>(responseContent);
            if (model != null)
            {
                model.RawResponse = responseContent;
                return model;
            }

            var jobErrorResponse = JsonSerializer.Deserialize<JobResponse>(responseContent);
            if (jobErrorResponse?.Job?.Error != null)
            {
                throw new Mindee500Exception(jobErrorResponse.Job.Error.Detail);
            }

            throw new MindeeException(
                "Couldn't deserialize response either as a polling error or a valid model response.");
        }

        private static ErrorResponse MakeUnknownError(int statusCode, string? responseContent)
        {
            return new ErrorResponse(
                statusCode,
                "Unknown Error",
                code: "000-000",
                detail: responseContent ?? "An unknown error occurred.",
                errors: null);
        }
    }
}
