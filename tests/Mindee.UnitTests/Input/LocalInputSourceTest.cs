using Docnet.Core;
using Docnet.Core.Models;
using Mindee.Exceptions;
using Mindee.Input;
using SkiaSharp;
using Xunit.Abstractions;

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
        public void Image_Quality_Compress_From_Input_Source()
        {
            var receiptInput = new LocalInputSource("Resources/file_types/receipt.jpg");
            receiptInput.Compress(40);
            File.WriteAllBytes("Resources/output/compress_indirect.jpg", receiptInput.FileBytes);
            Assert.True(true);
            var initialFileInfo = new FileInfo("Resources/file_types/receipt.jpg");
            var renderedFileInfo = new FileInfo("Resources/output/compress_indirect.jpg");
            Assert.True(renderedFileInfo.Length < initialFileInfo.Length);
        }

        [Fact]
        public void Image_Quality_Compresses_From_Compressor()
        {
            // Note: input image has a quality of ~85, but Skiasharp messes with headers, which results in a different
            // total byte size, which means we can't just compare quality 75 to quality 75.
            var receiptInput = new LocalInputSource("Resources/file_types/receipt.jpg");
            var compresses = new List<byte[]>
            {
                Compressor.CompressImage(receiptInput.FileBytes, 100),
                Compressor.CompressImage(receiptInput.FileBytes),
                Compressor.CompressImage(receiptInput.FileBytes, 50),
                Compressor.CompressImage(receiptInput.FileBytes, 10),
                Compressor.CompressImage(receiptInput.FileBytes, 1)
            };
            File.WriteAllBytes("Resources/output/compress100.jpg", compresses[0]);
            File.WriteAllBytes("Resources/output/compress75.jpg", compresses[1]);
            File.WriteAllBytes("Resources/output/compress50.jpg", compresses[2]);
            File.WriteAllBytes("Resources/output/compress10.jpg", compresses[3]);
            File.WriteAllBytes("Resources/output/compress1.jpg", compresses[4]);
            Assert.True(true);
            var initialFileInfo = new FileInfo("Resources/file_types/receipt.jpg");
            var renderedFileInfos = new List<FileInfo>
            {
                new("Resources/output/compress100.jpg"),
                new("Resources/output/compress75.jpg"),
                new("Resources/output/compress50.jpg"),
                new("Resources/output/compress10.jpg"),
                new("Resources/output/compress1.jpg")
            };
            Assert.True(initialFileInfo.Length < renderedFileInfos[0].Length);
            Assert.True(initialFileInfo.Length < renderedFileInfos[1].Length);
            Assert.True(renderedFileInfos[1].Length > renderedFileInfos[2].Length);
            Assert.True(renderedFileInfos[2].Length > renderedFileInfos[3].Length);
            Assert.True(renderedFileInfos[3].Length > renderedFileInfos[4].Length);
        }

        [Fact]
        public void Image_Resize_From_InputSource()
        {
            var imageResizeInput = new LocalInputSource("Resources/file_types/receipt.jpg");
            imageResizeInput.Compress(75, 250, 1000);
            File.WriteAllBytes("Resources/output/resize_indirect.jpg", imageResizeInput.FileBytes);
            var initialFileInfo = new FileInfo("Resources/file_types/receipt.jpg");
            var renderedFileInfo = new FileInfo("Resources/output/resize_indirect.jpg");
            Assert.True(renderedFileInfo.Length < initialFileInfo.Length);

            using var original = SKBitmap.Decode(imageResizeInput.FileBytes);
            Assert.Equal(250, original.Width);
            Assert.Equal(333, original.Height);
        }

        [Fact]
        public void Image_Resize_From_Compressor()
        {
            var imageResizeInput = new LocalInputSource("Resources/file_types/receipt.jpg");
            var resizes = new List<byte[]>
            {
                Compressor.CompressImage(imageResizeInput.FileBytes, 75, 500),
                Compressor.CompressImage(imageResizeInput.FileBytes, 75, 250, 500),
                Compressor.CompressImage(imageResizeInput.FileBytes, 75, 500, 250),
                Compressor.CompressImage(imageResizeInput.FileBytes, 75, null, 250)
            };
            File.WriteAllBytes("Resources/output/resize500xnull.jpg", resizes[0]);
            File.WriteAllBytes("Resources/output/resize250x500.jpg", resizes[1]);
            File.WriteAllBytes("Resources/output/resize500x250.jpg", resizes[2]);
            File.WriteAllBytes("Resources/output/resizenullx250.jpg", resizes[3]);
            var initialFileInfo = new FileInfo("Resources/file_types/receipt.jpg");
            var renderedFileInfos = new List<FileInfo>
            {
                new("Resources/output/resize500xnull.jpg"),
                new("Resources/output/resize250x500.jpg"),
                new("Resources/output/resize500x250.jpg"),
                new("Resources/output/resizenullx250.jpg"),
            };
            Assert.True(initialFileInfo.Length > renderedFileInfos[0].Length);
            Assert.True(renderedFileInfos[0].Length > renderedFileInfos[1].Length);
            Assert.True(renderedFileInfos[1].Length > renderedFileInfos[2].Length);
            Assert.Equal(renderedFileInfos[2].Length, renderedFileInfos[3].Length);
        }

        [Fact]
        public void Pdf_Resize_From_InputSource()
        {
            var pdfResizeInput = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
            pdfResizeInput.Compress(75);
            File.WriteAllBytes("Resources/output/resize_indirect.pdf", pdfResizeInput.FileBytes);
            var initialFileInfo = new FileInfo("Resources/products/invoice_splitter/default_sample.pdf");
            var renderedFileInfo = new FileInfo("Resources/output/resize_indirect.pdf");
            Assert.True(renderedFileInfo.Length < initialFileInfo.Length);
        }

        [Fact]
        public void Pdf_Resize_From_Compressor()
        {
            var pdfRersizeInput = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
            var resizes = new List<byte[]>
            {
                Compressor.CompressPdf(pdfRersizeInput.FileBytes),
                Compressor.CompressPdf(pdfRersizeInput.FileBytes, 75),
                Compressor.CompressPdf(pdfRersizeInput.FileBytes, 50),
                Compressor.CompressPdf(pdfRersizeInput.FileBytes, 10)
            };
            File.WriteAllBytes("Resources/output/compress85.pdf", resizes[0]);
            File.WriteAllBytes("Resources/output/compress75.pdf", resizes[1]);
            File.WriteAllBytes("Resources/output/compress50.pdf", resizes[2]);
            File.WriteAllBytes("Resources/output/compress10.pdf", resizes[3]);
            var initialFileInfo = new FileInfo("Resources/products/invoice_splitter/default_sample.pdf");
            var renderedFileInfos = new List<FileInfo>
            {
                new("Resources/output/compress85.pdf"),
                new("Resources/output/compress75.pdf"),
                new("Resources/output/compress50.pdf"),
                new("Resources/output/compress10.pdf"),
            };
            Assert.True(initialFileInfo.Length > renderedFileInfos[0].Length);
            Assert.True(renderedFileInfos[0].Length > renderedFileInfos[1].Length);
            Assert.True(renderedFileInfos[1].Length > renderedFileInfos[2].Length);
            Assert.True(renderedFileInfos[2].Length > renderedFileInfos[3].Length);
        }

        [Fact]
        public void Pdf_Resize_With_Text_Keeps_Text()
        {
            lock (DocLib.Instance)
            {
                var initialWithtext = new LocalInputSource("Resources/file_types/pdf/multipage.pdf");
                var compressedWithText = Compressor.CompressPdf(initialWithtext.FileBytes, 100);
                using var originalReader = DocLib.Instance.GetDocReader(initialWithtext.FileBytes, new PageDimensions(1));
                using var compressedReader = DocLib.Instance.GetDocReader(compressedWithText, new PageDimensions(1));
                Assert.Equal(originalReader.GetPageCount(), compressedReader.GetPageCount());

                for (var i = 0; i < originalReader.GetPageCount(); i++)
                {
                    Assert.Equal(originalReader.GetPageReader(i).GetText(), compressedReader.GetPageReader(i).GetText());
                }
            }
        }
    }
}
