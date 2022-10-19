using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Pdf;

namespace Mindee.UnitTests.Domain.Pdf
{
    public class PdfOperationTest
    {
        private readonly IPdfOperation _pdfOperation;

        public PdfOperationTest()
        {
            _pdfOperation = new DocNetApi(new NullLogger<DocNetApi>());
        }

        [Fact]
        public async Task Split_Wants1Page_MustGetOnly1Page()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), 2, 2);

            var splittedPdf = await _pdfOperation.SplitAsync(splitQuery);

            Assert.NotNull(splittedPdf);
            Assert.NotNull(splittedPdf.File);
            Assert.Equal(1, splittedPdf.TotalPageNumber);
        }

        [Fact]
        public async Task Split_WantsTooManyPages_MustFail()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), 1, 14);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _pdfOperation.SplitAsync(splitQuery));
        }

        [Fact]
        public async Task Split_WantsStartPageTo0_MustFail()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), 0, 3);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _pdfOperation.SplitAsync(splitQuery));
        }

        [Fact]
        public async Task Split_OtherThanAPdf_MustFail()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/passport/passport.jpeg"), 0, 3);

            await Assert.ThrowsAsync<ArgumentException>(() => _pdfOperation.SplitAsync(splitQuery));
        }
    }
}
