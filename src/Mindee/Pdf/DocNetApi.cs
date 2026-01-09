using System;
using System.Linq;
using Docnet.Core;
using Docnet.Core.Exceptions;
using Docnet.Core.Models;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.Pdf
{
    internal sealed class DocNetApi
        : IPdfOperation
    {
        private readonly ILogger _logger;

        public DocNetApi(ILogger logger = null)
        {
            _logger = logger;
        }

        public SplitPdf Split(SplitQuery splitQuery)
        {
            if (!CanBeOpen(splitQuery.File))
            {
                throw new MindeeException("This document is not recognized as a PDF file and cannot be split.");
            }

            var totalPages = GetTotalPagesNumber(splitQuery.File);

            if (totalPages == 0)
            {
                throw new InvalidOperationException("The total pages of the pdf is zero. We can not do a split on it.");
            }

            if (totalPages < splitQuery.PageOptions.OnMinPages)
            {
                return new SplitPdf(splitQuery.File, totalPages);
            }

            var targetedRange = splitQuery.PageOptions.PageIndexes.Select(pageIndex =>
            {
                if (pageIndex < 0)
                {
                    return totalPages - Math.Abs(pageIndex);
                }

                return pageIndex + 1;
            }).ToArray();

            if (targetedRange.Count() > totalPages)
            {
                throw new ArgumentOutOfRangeException(
                    $"The total indexes of pages to cut is superior to the total page count of the file ({totalPages}).");
            }

            if (targetedRange.Any(pageIndex => pageIndex > totalPages || pageIndex <= 0))
            {
                throw new ArgumentOutOfRangeException(
                    $"Some indexes ({string.Join(",", splitQuery.PageOptions.PageIndexes)}) do not exist in the file ({totalPages} pages).");
            }

            string range;
            if (splitQuery.PageOptions.Operation == PageOptionsOperation.KeepOnly)
            {
                range = string.Join(",", targetedRange);
            }
            else if (splitQuery.PageOptions.Operation == PageOptionsOperation.Remove)
            {
                var pageIndiceOriginalDocument = Enumerable.Range(1, totalPages);
                range = string.Join(",", pageIndiceOriginalDocument.Where(v => !targetedRange.Contains(v)));
            }
            else
            {
                throw new InvalidOperationException(
                    $"This operation is not available ({splitQuery.PageOptions.Operation}).");
            }

            lock (DocLib.Instance)
            {
                var splittedFile = DocLib.Instance.Split(splitQuery.File, range);

                return new SplitPdf(splittedFile, GetTotalPagesNumber(splittedFile));
            }
        }

        private bool CanBeOpen(byte[] file)
        {
            try
            {
                lock (DocLib.Instance)
                {
                    using var docReader = DocLib.Instance.GetDocReader(file, new PageDimensions());
                    return true;
                }
            }
            catch (DocnetLoadDocumentException ex)
            {
                _logger?.LogError(ex, null);
                return false;
            }
        }

        private ushort GetTotalPagesNumber(byte[] file)
        {
            try
            {
                lock (DocLib.Instance)
                {
                    using var docReader = DocLib.Instance.GetDocReader(file, new PageDimensions());
                    return (ushort)docReader.GetPageCount();
                }
            }
            catch (DocnetLoadDocumentException ex)
            {
                throw new ArgumentException(nameof(file), ex.ToString());
            }
        }
    }
}
