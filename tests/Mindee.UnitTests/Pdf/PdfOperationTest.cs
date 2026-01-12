using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Pdf;

namespace Mindee.UnitTests.Pdf
{
    public class PdfOperationTest
    {
        private readonly IPdfOperation _pdfOperation;

        public PdfOperationTest()
        {
            _pdfOperation = new DocNetApi(new NullLogger<DocNetApi>());
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_Wants1Page_MustGetOnly1Page()
        {
            var splitQuery = new SplitQuery(
                File.ReadAllBytes(Constants.RootDir + "file_types/pdf/multipage.pdf"),
                new PageOptions(new short[] { 1 }));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(1, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_Wants2Pages_MustGet2Pages()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes(
                    Constants.RootDir + "file_types/pdf/multipage.pdf"),
                new PageOptions(new short[] { 0, 1 }));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(2, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_WantsTooManyPages_MustFail()
        {
            var pageOptions = new PageOptions(new short[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });
            var splitQuery = new SplitQuery(File.ReadAllBytes(
                Constants.RootDir + "file_types/pdf/multipage.pdf"), pageOptions);

            Assert.Throws<ArgumentOutOfRangeException>(() => _pdfOperation.Split(splitQuery));
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_OtherThanAPdf_MustFail()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes(
                    Constants.RootDir + "file_types/receipt.jpga"),
                new PageOptions(new short[] { 0, 1, 2 }));

            Assert.Throws<MindeeException>(() => _pdfOperation.Split(splitQuery));
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_ShouldCutTheFirstAndThe2LastPages_MustSuccess()
        {
            var splitQuery = new SplitQuery(File.ReadAllBytes(
                    Constants.RootDir + "file_types/pdf/multipage.pdf"),
                new PageOptions(new short[] { 0, -2, -1 }));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(3, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_ShouldRemovePages_MustSuccess()
        {
            var splitQuery = new SplitQuery(
                File.ReadAllBytes(Constants.RootDir + "file_types/pdf/multipage.pdf")
                , new PageOptions(
                    new short[] { 0, 1, 2 }
                    , PageOptionsOperation.Remove));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(9, splitPdf.TotalPageNumber);
        }

        [Fact]
        [Trait("Category", "PDF operations")]
        public void Split_ShouldNotRemovePagesBecauseMinPagesAreNotMet()
        {
            var splitQuery = new SplitQuery(
                File.ReadAllBytes(Constants.RootDir + "file_types/pdf/multipage_cut-2.pdf")
                , new PageOptions(
                    new short[] { 0 }
                    , PageOptionsOperation.Remove
                    , 5));

            var splitPdf = _pdfOperation.Split(splitQuery);

            Assert.NotNull(splitPdf);
            Assert.NotNull(splitPdf.File);
            Assert.Equal(2, splitPdf.TotalPageNumber);
        }
    }
}
