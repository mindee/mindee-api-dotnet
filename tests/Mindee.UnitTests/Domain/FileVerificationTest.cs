using Mindee.Domain;

namespace Mindee.UnitTests.Domain
{
    public class FileVerificationTest
    {
        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithPdf_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.pdf"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithWebp_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.webp"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithPng_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.png"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithJpg_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.jpg"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithJpeg_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.Jpeg"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithHeic_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.heic"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithTiff_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.tiff"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithTif_MustSuccess()
        {
            Assert.True(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.tif"));
        }

        [Fact]
        public void IsFileNameExtensionRespectLimitation_WithMp4_MustFail()
        {
            Assert.False(FileVerification.IsFileNameExtensionRespectLimitation("ravenclaw.mp4"));
        }
    }
}
