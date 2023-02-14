using Mindee.Input;

namespace Mindee.UnitTests
{
    public class FileVerificationTest
    {
        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithPdf_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.pdf"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithWebp_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.webp"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithPng_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.png"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithJpg_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.jpg"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithJpeg_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.Jpeg"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithHeic_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.heic"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithTiff_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.tiff"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithTif_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.tif"));
        }

        [Fact]
        [Trait("Category", "File verifications")]
        public void IsFileNameExtensionRespectLimitation_WithMp4_MustFail()
        {
            Assert.False(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.mp4"));
        }
    }
}
