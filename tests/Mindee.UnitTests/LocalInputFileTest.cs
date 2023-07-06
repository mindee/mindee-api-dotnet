using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.UnitTests
{
    public class LocalInputFileTest
    {
        [Fact]
        [Trait("Category", "File loading")]
        public void Can_Load_ImageFiles()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.heic"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.jpg"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.jpga"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.tif"));
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/receipt/receipt.tiff"));
        }

        [Fact]
        [Trait("Category", "File loading")]
        public void Can_Load_PDF()
        {
            Assert.IsType<LocalInputSource>(new LocalInputSource("Resources/pdf/blank.pdf"));
        }

        [Fact]
        [Trait("Category", "File loading")]
        public void DoesNot_Load_InvalidFiles()
        {
            Assert.Throws<MindeeInputException>(
                () => new LocalInputSource("Resources/receipt/receipt.txt"));
        }
    }
}
