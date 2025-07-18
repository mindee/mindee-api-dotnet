using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using Mindee.Parsing.V2;

#nullable enable

namespace Mindee.Http
{
    /// <summary>
    /// Communicate with the Mindee HTTP API V2.
    /// <p>
    /// You may use this base class to make your own custom class.
    /// However, we may introduce breaking changes in minor versions as needed.
    /// </p>
    /// </summary>
    public abstract class HttpApiV2
    {
        /// <summary>
        /// Set this to your logger.
        /// </summary>
        protected ILogger<HttpApiV2>? Logger;

        /// <summary>
        /// Do a prediction according parameters for custom model defined in the Studio.
        /// </summary>
        /// <param name="predictParameter"><see cref="PredictParameterV2"/></param>
        public abstract Task<JobResponse> ReqPostEnqueueInferenceAsync(PredictParameterV2 predictParameter);


        /// <summary>
        /// Get a job for an enqueued document.
        /// </summary>
        /// <param name="jobId">The job ID as returned by the predict_async route.</param>
        public abstract Task<JobResponse> ReqGetJobAsync(string jobId);

        /// <summary>
        /// Get a document inference.
        /// </summary>
        /// <param name="inferenceId">The inference ID as given by the job.</param>
        public abstract Task<InferenceResponse> ReqGetInferenceAsync(string inferenceId);

        /// <summary>
        /// Get the error from the server return.
        /// </summary>
        /// <param name="responseContent"></param>
        /// <returns></returns>
        /// <exception cref="MindeeHttpExceptionV2"></exception>
        protected ErrorResponse GetErrorFromContent(string? responseContent)
        {
            Logger?.LogInformation("Parsing error response ...");
            try
            {
                using var doc = JsonDocument.Parse(responseContent ?? "{}");
                if (doc.RootElement.TryGetProperty("status", out var statusProp) &&
                    doc.RootElement.TryGetProperty("detail", out var detailsProp))
                {
                    return new ErrorResponse { Status = statusProp.GetInt32(), Detail = detailsProp.GetString() };
                }

                return new ErrorResponse { Detail = "Unknown error", Status = 500 };
            }
            catch (JsonException)
            {
                return new ErrorResponse { Detail = "Unknown error", Status = 500 };
            }
        }

        /// <summary>
        /// Attempt to deserialize a response from the server.
        /// </summary>
        /// <param name="responseContent"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        protected TResponse DeserializeResponse<TResponse>(string? responseContent)
            where TResponse : CommonResponse, new()
        {
            Logger?.LogInformation("Parsing HTTP 2xx response ...");

            if (responseContent == null || string.IsNullOrWhiteSpace(responseContent))
                throw new MindeeException("Empty response from server.");

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
    }
}
