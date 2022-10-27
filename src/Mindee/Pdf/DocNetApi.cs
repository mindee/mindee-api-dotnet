using System;
using System.IO;
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

            if (splitQuery.PageNumberStart == 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(splitQuery.PageNumberStart),
                    "The start number can not be equal to 0.");
            }

            if (splitQuery.PageNumberEnd > totalPages)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(splitQuery.PageNumberEnd),
                    $"The end number can not be > to the max page of the current pdf ({totalPages}.");
            }

            // index pages start to 0
            var splittedFile = _docLib.Split(
                currentFile,
                splitQuery.PageNumberStart - 1,
                splitQuery.PageNumberEnd - 1);

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
