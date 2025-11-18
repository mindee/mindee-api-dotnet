using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.V1.Parsing.Common
{
    [Trait("Category", "Cropper")]
    public class CropperTest
    {
        [Fact]
        public async Task Should_GetCropperResult()
        {
            var response = await JsonSerializer.DeserializeAsync<PredictResponse<ReceiptV4>>(
                new FileInfo(Constants.V1RootDir + "extras/cropper/complete.json").OpenRead());

            Assert.NotNull(response);
            Assert.NotEmpty(response.Document.Inference.Pages);
            var page = response.Document.Inference.Pages.First();
            Assert.NotNull(page.Extras);
            var cropping = page.Extras.Cropper.Cropping;
            Assert.Equal("Polygon with 24 points.", cropping.First().ToString());
        }
    }
}
