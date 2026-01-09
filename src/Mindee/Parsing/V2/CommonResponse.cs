namespace Mindee.Parsing.V2
{
    /// <summary>
    ///     Common response information from Mindee API V2.
    /// </summary>
    public abstract class CommonResponse
    {
        /// <summary>
        ///     The raw server response.
        ///     This is not formatted in any way by the library and may contain newline and tab characters.
        /// </summary>
        public string RawResponse { get; set; }
    }
}
