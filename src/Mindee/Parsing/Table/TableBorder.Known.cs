namespace Spectre.Console;

/// <summary>
/// Represents a border.
/// </summary>
public abstract partial class TableBorder
{
    /// <summary>
    /// Gets an ASCII border with a double header border.
    /// </summary>
    public static TableBorder AsciiDoubleHead { get; } = new AsciiDoubleHeadTableBorder();
}
