namespace Mindee.Input
{
    /// <summary>
    /// Represent options to cut a document.
    /// </summary>
    public sealed class PageOptions
    {
        /// <summary>
        /// List of page indexes.
        /// A negative index can be used, indicating an offset from the end of the document.
        /// [1, -1] represents the first and last pages of the document.
        /// </summary>
        public short[] PageNumbers { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumbers"><see cref="PageNumbers"/></param>
        public PageOptions(short[] pageNumbers)
        {
            PageNumbers = pageNumbers;
        }
    }
}
