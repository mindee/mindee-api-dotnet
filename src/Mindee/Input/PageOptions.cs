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
        /// <see cref="Input.PageOptionsOperation"/>
        /// </summary>
        /// <remarks>KeepOnly by default.</remarks>
        public PageOptionsOperation PageOptionsOperation { get; }

        /// <summary>
        /// Apply the operation only if document has at least this many pages.
        /// </summary>
        /// <remarks>0 by default.</remarks>
        public ushort OnMinPages { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageNumbers"><see cref="PageNumbers"/></param>
        /// <param name="pageOptionsOperation"><see cref="PageOptionsOperation"/></param>
        /// <param name="onMinPages"><see cref="PageOptionsOperation"/></param>
        public PageOptions(
            short[] pageNumbers
            , PageOptionsOperation pageOptionsOperation = PageOptionsOperation.KeepOnly
            , ushort onMinPages = 0
            )
        {
            PageNumbers = pageNumbers;
            PageOptionsOperation = pageOptionsOperation;
            OnMinPages = onMinPages;
        }
    }
}
