using System;
using System.IO;
using System.Text.Json;

namespace Mindee.V2.Parsing
{
    /// <summary>
    /// Implementation of BaseLocalResponse for V2.
    /// </summary>
    public class LocalResponse : Mindee.Parsing.BaseLocalResponse
    {
        /// <inheritdoc />
        public LocalResponse(string input) : base(input)
        {
        }

        /// <inheritdoc />
        public LocalResponse(FileInfo input) : base(input)
        {
        }


        /// <summary>
        ///     Load a local inference.
        ///     Typically used when wanting to load a V2 webhook callback.
        /// </summary>
        /// <returns></returns>
        public TResponse DeserializeResponse<TResponse>()
            where TResponse : BaseResponse, new()
        {
            var model = JsonSerializer.Deserialize<TResponse>(FileBytes);

            if (model == null)
            {
                throw new InvalidOperationException(
                    "Could not deserialize the local file into the expected response type.");
            }

            model.RawResponse = ToString();
            return model;
        }

        /// <summary>
        /// Deserializes a Job Response.
        /// </summary>
        /// <returns></returns>
        public JobResponse DeserializeJobResponse()
        {
            return JsonSerializer.Deserialize<JobResponse>(FileBytes);
        }
    }
}
