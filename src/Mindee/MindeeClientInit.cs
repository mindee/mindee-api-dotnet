using Microsoft.Extensions.Options;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// Helper to create manually new instance of <see cref="MindeeClient"/>.
    /// </summary>
    public static class MindeeClientInit
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="apiKey">The required API key to use Mindee.</param>
        public static MindeeClient Create(string apiKey)
        {
            var mindeeSettings = new MindeeSettings
            {
                ApiKey = apiKey
            };

            return Create(mindeeSettings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mindeeSettings"><see cref="MindeeSettings"/></param>
        public static MindeeClient Create(MindeeSettings mindeeSettings)
        {
            return new MindeeClient(
                Options.Create(mindeeSettings),
                new DocNetApi());
        }
    }
}
