using System;
using System.Linq;
using Docnet.Core;
using Docnet.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.Pdf
{
    internal sealed class DocNetApi
        : IPdfOperation
    {
        private readonly DocLib _docLib = DocLib.Instance;
        private readonly ILogger _logger;

        public DocNetApi(ILogger<IPdfOperation> logger)
        {
            _logger = logger;
        }

        public SplitPdf Split(SplitQuery splitQuery)
        {
            if (!CanBeOpen(splitQuery.File))
            {
                throw new MindeeException($"This document is not recognized as a PDF file and can not be split.");
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

            var targetedRange = splitQuery.PageOptions.PageNumbers.Select(pn =>
            {
                if (pn < 0)
                {
                    return (totalPages - Math.Abs(pn));
                }

                return pn;
            }).ToArray();

            if (targetedRange.Count() > totalPages)
            {
                throw new ArgumentOutOfRangeException($"The total indexes of pages to cut is superior to the total page count of the file ({totalPages}).");
            }

            if (targetedRange.Any(pn => pn >= totalPages || pn <= 0))
            {
                throw new ArgumentOutOfRangeException($"Some indexes pages ({string.Join(",", splitQuery.PageOptions.PageNumbers)} are not existing in the file which have {totalPages} pages.");
            }

            string range;
            if (splitQuery.PageOptions.PageOptionsOperation == PageOptionsOperation.KeepOnly)
            {
                range = string.Join(",", targetedRange);
            }
            else if (splitQuery.PageOptions.PageOptionsOperation == PageOptionsOperation.Remove)
            {
                var pageIndiceOriginalDocument = Enumerable.Range(1, totalPages);
                range = string.Join(",", pageIndiceOriginalDocument.Where(v => !targetedRange.Contains(v)));
            }
            else
            {
                throw new InvalidOperationException($"This operation is not available ({splitQuery.PageOptions.PageOptionsOperation}).");
            }

            var splittedFile = _docLib.Split(splitQuery.File, range);

            return new SplitPdf(splittedFile, GetTotalPagesNumber(splittedFile));
        }

        private bool CanBeOpen(byte[] file)
        {
            try
            {
                using (var docReader = _docLib.GetDocReader(file, new Docnet.Core.Models.PageDimensions()))
                {
                    return true;
                }
            }
            catch (DocnetLoadDocumentException ex)
            {
                _logger.LogError(ex, null);
                return false;
            }
        }

        private ushort GetTotalPagesNumber(byte[] file)
        {
            try
            {
                using (var docReader = _docLib.GetDocReader(file, new Docnet.Core.Models.PageDimensions()))
                {
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
