using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Docnet.Core;
using Docnet.Core.Exceptions;
using Microsoft.Extensions.Logging;
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

        public async Task<SplitPdf> SplitAsync(SplitQuery splitQuery)
        {
            MemoryStream ms = new MemoryStream();
            await splitQuery.Stream.CopyToAsync(ms);

            var currentFile = ms.ToArray();

            var totalPages = GetTotalPagesNumber(currentFile);

            if (totalPages == 0)
            {
                throw new InvalidOperationException("The total pages of the pdf is zero. We can not do a split on it.");
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

            var splittedFile = _docLib.Split(currentFile, range);

            return new SplitPdf(splittedFile, GetTotalPagesNumber(splittedFile));
        }

        public bool CanBeOpen(byte[] file)
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
