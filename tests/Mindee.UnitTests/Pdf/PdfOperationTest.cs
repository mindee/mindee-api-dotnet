using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Exceptions;
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
        public void Split_Wants1Page_MustGetOnly1Page()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 2 }));

            var splittedPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splittedPdf);
            Assert.NotNull(splittedPdf.File);
            Assert.Equal(1, splittedPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_Wants2Page_MustGet2Page()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 1, 2 }));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(2, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_WantsTooManyPages_MustFail()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }));

            Assert.Throws<ArgumentOutOfRangeException>(() => _pdfOperation.Split(splitQuery));
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_WantsStartPageTo0_MustFail()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 0, 1, 2, 3 }));

            Assert.Throws<ArgumentOutOfRangeException>(() => _pdfOperation.Split(splitQuery));
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_OtherThanAPdf_MustFail()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes("Resources/passport/passport.jpeg"), new PageOptions(new short[] { 1, 2, 3 }));

            Assert.Throws<MindeeException>(() => _pdfOperation.Split(splitQuery));
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_ShouldCutTheFirstAndThe2LastPages_MustSuccess()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes("Resources/pdf/multipage.pdf"), new PageOptions(new short[] { 1, -2, -1 }));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(3, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_ShouldRemovePages_MustSuccess()
        {
            var splitQuery = new SplitQuery(
                File.ReadAllBytes("Resources/pdf/multipage.pdf")
                , new PageOptions(
                    new short[] { 1, 2, 3 }
                    , PageOptionsOperation.Remove));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(9, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "Pdf operations")]
        public void Split_ShouldNotRemovePagesBecauseMinPagesAreNotMet()
        {
            var splitQuery = new SplitQuery(
                File.ReadAllBytes("Resources/pdf/multipage_cut-2.pdf")
                , new PageOptions(
                    new short[] { 1 }
                    , PageOptionsOperation.Remove
                    , 5));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(2, splitPdf.TotalPageNumber);
        }
    }
}
