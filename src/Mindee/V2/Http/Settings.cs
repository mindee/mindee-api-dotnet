using System.Diagnostics.CodeAnalysis;

namespace Mindee.V2.Http
{
    /// <summary>
    ///     Mindee V2 settings.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty",
        Justification = "Used as a base type for strict pattern matching in the public API.")]
    public class Settings : V1.Http.Settings;
}
