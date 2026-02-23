using System.IO;
using System.Text.Json;

namespace Mindee.V2.Parsing
{
    /// <summary>
    /// Implementation of BaseLocalResponse for V1.
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
        public TInferenceModel DeserializeResponse<TInferenceModel>() where TInferenceModel : CommonResponse, new()
        {
            var model = JsonSerializer.Deserialize<TInferenceModel>(FileBytes);
            model.RawResponse = ToString();

            return model;
        }
    }
}
