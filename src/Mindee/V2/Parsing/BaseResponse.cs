namespace Mindee.V2.Parsing
{
    /// <summary>
    /// Base class for all responses from the V2 API.
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        ///     The raw server response.
        ///     This is not formatted in any way by the library and may contain newline and tab characters.
        /// </summary>
        public string RawResponse { get; set; }
    }
}
