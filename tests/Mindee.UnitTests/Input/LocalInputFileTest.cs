using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.UnitTests.Input
{
    [Trait("Category", "File loading")]
    public class LocalInputFileTest
    {
        [Fact]
        public void Can_Load_Type_ImageFiles()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/file_types/receipt.jpg"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/file_types/receipt.heic"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/file_types/receipt.jpga"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/file_types/receipt.tif"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/file_types/receipt.tiff"));
        }

        [Fact]
        public void Can_Load_Type_PDF()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/file_types/pdf/blank_1.pdf"));
        }

        [Fact]
        public void DoesNot_Load_InvalidFile()
        {
            Assert.Throws<MindeeInputException>(
                () => new LocalInputSource("Resources/file_types/receipt.txt"));
        }

        [Fact]
        public void Can_Load_Using_FileInfo()
        {
            FileInfo fileInfo = new FileInfo("Resources/file_types/receipt.jpg");
            Assert.IsType<LocalInputSource>(new LocalInputSource(fileInfo));
        }
    }
}
