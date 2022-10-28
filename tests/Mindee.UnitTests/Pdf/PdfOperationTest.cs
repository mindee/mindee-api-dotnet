using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Input;
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
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_Wants1Page_MustGetOnly1Page()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 2 }));

            var splittedPdf = await _pdfOperation.SplitAsync(splitQuery);

            Assert.NotNull(splittedPdf);
            Assert.NotNull(splittedPdf.File);
            Assert.Equal(1, splittedPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_Wants2Page_MustGet2Page()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 1, 2 }));

            var splitPdf = await _pdfOperation.SplitAsync(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(2, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_WantsTooManyPages_MustFail()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 1,2,3,4,5,6,7,8,9,10,11,12,13,14 }));

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _pdfOperation.SplitAsync(splitQuery));
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_WantsStartPageTo0_MustFail()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 0, 1, 2, 3 }));

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _pdfOperation.SplitAsync(splitQuery));
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_OtherThanAPdf_MustFail()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/passport/passport.jpeg"), new PageOptions(new short[] { 1, 2, 3 }));

            await Assert.ThrowsAsync<ArgumentException>(() => _pdfOperation.SplitAsync(splitQuery));
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_ShouldCutTheFirstAndThe2LastPages_MustSuccess()
        {
            var splitQuery = new SplitQuery(File.OpenRead("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 1, -2, -1 }));

            var splitPdf = await _pdfOperation.SplitAsync(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(3, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public async Task NewSplit_ShouldRemovePages_MustSuccess()
        {
            var splitQuery = new SplitQuery(
                File.OpenRead("Resources/pdf/multipage.pdf")
                , new PageOptions(
                    new short[] { 1, 2, 3 }
                    , PageOptionsOperation.Remove));

            var splitPdf = await _pdfOperation.SplitAsync(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(9, splitPdf.TotalPageNumber);
        }
    }
}
