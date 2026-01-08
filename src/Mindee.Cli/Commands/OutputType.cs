namespace Mindee.Cli
{
    /// <summary>
    ///     How to output the response.
    /// </summary>
    internal enum OutputType
    {
        /// <summary>
        ///     Raw JSON.
        /// </summary>
        Raw,

        /// <summary>
        ///     Document-level in rST format.
        /// </summary>
        Summary,

        /// <summary>
        ///     Complete response in rST format.
        /// </summary>
        Full
    }
}
