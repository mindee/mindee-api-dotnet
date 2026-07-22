using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SettingsV2 = Mindee.V2.Http.Settings;

namespace Mindee.Cli.Commands.V2
{
    abstract class BaseCommand : Command
    {
        protected readonly Option<string?> ApiKeyOption;

        public BaseCommand(string name, string description) : base(name, description)
        {
            ApiKeyOption = new Option<string?>("--api-key", "-k") { Description = "Mindee V2 API key." };
            Options.Add(ApiKeyOption);
        }

        protected void ConfigureApiKey(IServiceProvider services)
        {
            Validators.Add(commandResult =>
            {
                var apiKey = commandResult.GetValue(ApiKeyOption);
                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    return;
                }

                try
                {
                    _ = services.GetRequiredService<IOptions<SettingsV2>>().Value;
                }
                catch (OptionsValidationException)
                {
                    commandResult.AddError(
                        "The Mindee V2 API key is missing. " +
                        "Please provide it via the '--api-key' option or your configured environment variable.");
                }
            });
        }
    }
}
