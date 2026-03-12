using System.CommandLine;
using System.Text.Json;
using Mindee.V2.Parsing.Search;
using V2Client = Mindee.V2.Client;

namespace Mindee.Cli.Commands.V2
{
    /// <summary>
    /// Lists all models for a given API key.
    /// </summary>
    public class SearchModelsCommand : Command
    {
        private readonly Option<string?>? _nameOption;
        private readonly Option<string?>? _modelTypeOption;
        private readonly Option<bool>? _rawOption;

        /// <summary>
        ///
        /// </summary>
        public SearchModelsCommand() : base("search-models", "Search available models.")
        {
            _nameOption = new Option<string?>("--name", "-n")
            {
                Description = "Filter by model name partial match (case insensitive).",
                DefaultValueFactory = _ => null
            };
            Options.Add(_nameOption);

            var availableModels = new List<string> { "extraction", "crop", "classification", "ocr", "split" };
            var modelTypeDescription = """
                                       Filter by exact model type (case sensitive).
                                       Available options:
                                       """;
            modelTypeDescription += string.Join("\n - ", availableModels);
            _modelTypeOption = new Option<string?>("--model-type", "-m")
            {
                Description = modelTypeDescription,
                DefaultValueFactory = _ => null
            };
            Options.Add(_modelTypeOption);

            _rawOption = new Option<bool>("--raw-json", "-r")
            {
                Description = "Whether to output the raw JSON response.",
                DefaultValueFactory = _ => false
            };
            Options.Add(_rawOption);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mindeeClientV2">V2 Client instance</param>
        public void ConfigureAction(V2Client mindeeClientV2)
        {
            this.SetAction(parseResult =>
            {
                var name = _nameOption != null ? parseResult.GetValue(_nameOption) : null;
                var modelType = _modelTypeOption != null ? parseResult.GetValue(_modelTypeOption) : null;
                var raw = _rawOption != null && parseResult.GetValue(_rawOption);

                var handler = new Handler(mindeeClientV2);
                return handler
                    .InvokeAsync(name, modelType, raw)
                    .GetAwaiter().GetResult();
            });
        }

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="mindeeClientV2"></param>
        public class Handler(V2Client mindeeClientV2)
        {
            private readonly JsonSerializerOptions _jsonSerializerOptions = new()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            /// <summary>
            /// Invoke the command.
            /// </summary>
            /// <param name="name"></param>
            /// <param name="modelType"></param>
            /// <param name="raw"></param>
            /// <returns></returns>
            public async Task<int> InvokeAsync(string? name, string? modelType, bool raw)
            {
                var response = await mindeeClientV2.SearchModels(name, modelType);
                PrintToConsole(Console.Out, raw, response);
                return 0;
            }


            private void PrintToConsole(
                TextWriter console,
                bool raw,
                SearchResponse response)
            {
                console.Write(raw ? JsonSerializer.Serialize(response, _jsonSerializerOptions) : response.ToString());
            }
        }
    }
}
