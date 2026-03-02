using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    ///     Job the inference belongs to.
    /// </summary>
    public class InferenceJob
    {
        /// <summary>
        ///     UUID of the Job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Pretty-prints the file section exactly as expected by Inference.ToString().
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder("Job\n===");
            sb.Append($"\n:ID: {Id}");
            return sb.ToString();
        }
    }
}
