namespace Mindee.Input
{
    /// <summary>
    ///     Represent options to cut a document.
    /// </summary>
    public sealed class PageOptions
    {
        /// <summary>
        /// </summary>
        /// <param name="pageIndexes">
        ///     <see cref="PageIndexes" />
        /// </param>
        /// <param name="operation">
        ///     <see cref="Operation" />
        /// </param>
        /// <param name="onMinPages">
        ///     <see cref="Operation" />
        /// </param>
        public PageOptions(
            short[] pageIndexes
            , PageOptionsOperation operation = PageOptionsOperation.KeepOnly
            , ushort onMinPages = 0
        )
        {
            PageIndexes = pageIndexes;
            Operation = operation;
            OnMinPages = onMinPages;
        }

        /// <summary>
        ///     List of page indexes.
        ///     A negative index can be used, indicating an offset from the end of the document.
        ///     [0, -1] represents the first and last pages of the document.
        /// </summary>
        public short[] PageIndexes { get; }

        /// <summary>
        ///     <see cref="Input.PageOptionsOperation" />
        /// </summary>
        /// <remarks>KeepOnly by default.</remarks>
        public PageOptionsOperation Operation { get; }

        /// <summary>
        ///     Apply the operation only if the document has at least this many pages.
        /// </summary>
        /// <remarks>0 by default.</remarks>
        public ushort OnMinPages { get; }

        /// <summary>
        ///     Basic info on the options.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var pageNumbers = string.Join(", ", PageIndexes);
            return $"min: {OnMinPages}, operation: {Operation}, pages: ({pageNumbers})";
        }
    }
}
