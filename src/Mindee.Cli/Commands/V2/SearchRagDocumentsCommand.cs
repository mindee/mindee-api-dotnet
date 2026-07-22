using System.CommandLine;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mindee.V2.Parsing.Search;
using Mindee.V2.Search.Model;
using Mindee.V2.Search.Models;
using SettingsV2 = Mindee.V2.Http.Settings;
using V2Client = Mindee.V2.Client;

namespace Mindee.Cli.Commands.V2
{
    /// <summary>
    /// Lists all models for a given API key.
    /// </summary>
    class SearchRagDocumentsCommand : BaseCommand
    {
        private readonly Option<string?>? _filenameOption;
        private readonly Option<string> _modelIdOption;
        private readonly Option<bool>? _rawOption;

        /// <summary>
        ///
        /// </summary>
        public SearchRagDocumentsCommand() : base("search-rag-docs", "Search available RAG documents for a given model.")
        {
            _modelIdOption =
                new Option<string>("--model-id", "-m") { Description = "Filter by model ID", Required = true };
            Options.Add(_modelIdOption);

            _filenameOption = new Option<string?>("--filename", "-f")
            {
                Description = "Filter by file name partial match (case insensitive).",
                DefaultValueFactory = _ => null
            };
            Options.Add(_filenameOption);

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
        /// <param name="services">Service provider for dependency resolution</param>
        public void ConfigureAction(IServiceProvider services)
        {
            base.ConfigureApiKey(services);

            this.SetAction(parseResult =>
            {
                var modelId = _modelIdOption != null ? parseResult.GetValue(_modelIdOption) : null;
                var filename = _filenameOption != null ? parseResult.GetValue(_filenameOption) : null;
                var raw = _rawOption != null && parseResult.GetValue(_rawOption);
                var apiKey = parseResult.GetValue(ApiKeyOption);
                V2Client mindeeClientV2;
                try
                {
                    mindeeClientV2 = !string.IsNullOrWhiteSpace(apiKey)
                        ? new V2Client(apiKey)
                        : services.GetRequiredService<V2Client>();
                }
                catch (OptionsValidationException)
                {
                    Console.Error.WriteLine(
                        "The Mindee V2 API key is missing. " +
                        "Please provide it via the '--api-key' option or your configured environment variable.");
                    return 1;
                }

                var handler = new Handler(mindeeClientV2);
                return handler
                    .InvokeAsync(modelId, filename, raw)
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
            /// <param name="modelId"></param>
            /// <param name="filename"></param>
            /// <param name="raw"></param>
            /// <returns></returns>
            public async Task<int> InvokeAsync(string? modelId, string? filename, bool raw)
            {
                var response = await mindeeClientV2.SearchRagDocuments(
                    new RagDocumentSearchParameters(modelId: modelId, filename: filename));
                PrintToConsole(Console.Out, raw, response);
                return 0;
            }

            private void PrintToConsole(
                TextWriter console,
                bool raw,
                RagDocumentSearchResponse response)
            {
                console.Write(raw ? JsonSerializer.Serialize(response, _jsonSerializerOptions) : response.ToString());
            }
        }
    }
}
