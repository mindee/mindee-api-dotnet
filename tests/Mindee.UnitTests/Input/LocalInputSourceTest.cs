using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.UnitTests.Input
{
    [Trait("Category", "File loading")]
    public class LocalInputSourceTest
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

        [Fact]
        public void Can_Load_Using_FileStream()
        {
            Stream fileStream = File.OpenRead("Resources/file_types/receipt.jpg");
            Assert.IsType<LocalInputSource>(new LocalInputSource(fileStream, "receipt.jpg"));
        }

        [Fact]
        public void Can_Load_Using_MemoryStream()
        {
            var fileBytes = File.ReadAllBytes("Resources/file_types/receipt.jpg");
            var memoryStream = new MemoryStream(fileBytes);
            Assert.IsType<LocalInputSource>(new LocalInputSource(memoryStream, "receipt.jpg"));
        }

        [Fact]
        public void Can_Load_Using_FileBytes()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource(
                fileBytes: new byte[] { byte.MinValue },
                filename: "titicaca.jpg"));
        }

        [Fact]
        public void PDF_Input_Has_Text()
        {
            var hasSourceText = new LocalInputSource("Resources/file_types/pdf/multipage.pdf");
            var hasNoSourceText = new LocalInputSource("Resources/file_types/pdf/blank_1.pdf");
            var hasNoSourceTextSinceItsImage = new LocalInputSource("Resources/file_types/receipt.jpg");
            Assert.True(hasSourceText.HasSourceText());
            Assert.False(hasNoSourceText.HasSourceText());
            Assert.False(hasNoSourceTextSinceItsImage.HasSourceText());
        }

        [Fact]
        public void Image_Compresses()
        {
            var receiptInput = new LocalInputSource("Resources/file_types/receipt.jpg");
            receiptInput.Compress(90);
            File.WriteAllBytes("Resources/compress90.jpg", receiptInput.FileBytes);
            var initialFile = new FileInfo("Resources/file_types/receipt.jpg");
            var renderedFile = new FileInfo("Resources/compress90.jpg");
            Assert.True(renderedFile.Length < initialFile.Length);
        }
    }
}
