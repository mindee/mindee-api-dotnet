using System.Diagnostics.CodeAnalysis;

namespace Mindee.Input
{
    /// <summary>
    ///  Base class for input sources used in Mindee API requests.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty",
        Justification = "Used as a base type for strict pattern matching in the public API.")]
    public abstract class InputSource;
}
