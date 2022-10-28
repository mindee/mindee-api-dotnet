using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Docnet.Core;
using Docnet.Core.Exceptions;
using Microsoft.Extensions.Logging;

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

            if (splitQuery.PageOptions.PageNumbers.Length > totalPages)
            {
                throw new ArgumentOutOfRangeException($"The total indexes of pages to cut is superior to the total page count of the file ({totalPages}).");
            }

            if(splitQuery.PageOptions.PageNumbers.Any(pn => pn >= totalPages || pn <= 0))
            {
                throw new ArgumentOutOfRangeException($"Some indexes pages ({string.Join(",", splitQuery.PageOptions.PageNumbers)} are not existing in the file which have {totalPages} pages.");
            }

            string range = string.Join(",", splitQuery.PageOptions.PageNumbers);

            var splittedFile = _docLib.Split(
                currentFile,
                range);

            return new SplitPdf(splittedFile, GetTotalPagesNumber(splittedFile));
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
    }
}
