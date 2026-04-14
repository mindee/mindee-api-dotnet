using System.Collections.Generic;
using System.Linq;
using Mindee.Exceptions;
using Mindee.Extraction;
using Mindee.Input;

namespace Mindee.V2.FileOperations
{
    /// <summary>
    /// V2 Split operation utility.
    /// </summary>
    public sealed class Split
    {

        /// <summary>
        ///     LocalInputSource object used by the ImageExtractor.
        /// </summary>
        private readonly LocalInputSource _localInput;

        /// <summary>
        ///     Expands a range of pages into a list of page indexes.
        /// </summary>
        /// <param name="start">Start of the range.</param>
        /// <param name="end">End of the range.</param>
        /// <returns>An array of page indexes.</returns>
        public static List<int> ExpandRange(int start, int end)
        {
            if (start > end)
            {
                throw new MindeeInputException("Invalid page range provided.");
            }

            int count = end - start + 1;
            return Enumerable.Range(start, count).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputSource"></param>
        public Split(LocalInputSource inputSource)
        {
            this._localInput = inputSource;
        }

        /// <summary>
        /// Extracts the splits from the input file.
        /// </summary>
        /// <param name="splits">List of subpage indexes to keep.</param>
        /// <returns></returns>
        public SplitFiles ExtractSplits(List<List<int>> splits)
        {
            var pdfExtractor = new PdfExtractor(this._localInput);
            if (splits.Count == 0)
            {
                throw new MindeeInputException("No splits provided for extraction.");
            }

            List<List<int>> expandedPageIndexes = [];
            expandedPageIndexes.AddRange(splits.Select(split => ExpandRange(split[0], split[1])));
            return (SplitFiles)pdfExtractor.ExtractSubDocuments(expandedPageIndexes);
        }
    }
}
