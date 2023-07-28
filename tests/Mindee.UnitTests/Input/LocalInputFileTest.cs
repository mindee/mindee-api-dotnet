using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.UnitTests.Input
{
    [Trait("Category", "File loading")]
    public class LocalInputFileTest
    {
        [Fact]
        public void Can_Load_TYpe_ImageFiles()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.heic"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.jpg"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.jpga"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.tif"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.tiff"));
        }

        [Fact]
        public void Can_Load_Type_PDF()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/pdf/blank.pdf"));
        }

        [Fact]
        public void DoesNot_Load_InvalidFile()
        {
            Assert.Throws<MindeeInputException>(
                () => new LocalInputSource("Resources/receipt/receipt.txt"));
        }

        [Fact]
        public void Can_Load_Using_FileInfo()
        {
            FileInfo fileInfo = new FileInfo("Resources/receipt/receipt.jpg");
            Assert.IsType<LocalInputSource>(new LocalInputSource(fileInfo));
        }
    }
}
