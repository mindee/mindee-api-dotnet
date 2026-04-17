using Mindee.Input;
using Mindee.V2;
using Mindee.V2.FileOperations;
using Mindee.V2.Product.Crop;
using Mindee.V2.Product.Crop.Params;
using Mindee.V2.Product.Extraction;
using Mindee.V2.Product.Extraction.Params;

namespace Mindee.IntegrationTests.V2.FileOperations
{
    [Trait("Category", "V2")]
    [Trait("Category", "FileOperations")]
    public class CropTest : IDisposable
    {
        private readonly string? _cropModelId;
        private readonly string? _findocModelId;
        private readonly Client _client;
        private readonly string _outputDir;

        public CropTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _cropModelId = Environment.GetEnvironmentVariable("MindeeV2__Crop__Model__Id");
            _findocModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");

            _outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
            if (!Directory.Exists(_outputDir))
            {
                Directory.CreateDirectory(_outputDir);
            }
        }

        public void Dispose()
        {
            var file1 = Path.Combine(_outputDir, "crop_001.jpg");
            var file2 = Path.Combine(_outputDir, "crop_002.jpg");

            if (File.Exists(file1)) File.Delete(file1);
            if (File.Exists(file2)) File.Delete(file2);
        }

        private void CheckFindocReturn(ExtractionResponse findocResponse)
        {
            Assert.True(findocResponse.Inference.Model.Id.Length > 0);

            var totalAmount = findocResponse.Inference.Result.Fields["total_amount"].SimpleField;
            Assert.NotNull(totalAmount);
            Assert.True(totalAmount.Value > 0);
        }

        [Fact(Timeout = 180000)]
        public async Task Extract_Crops_From_Image_Correctly()
        {
            var inputSource = new LocalInputSource(Path.Combine(
                Constants.V2ProductDir, "crop/default_sample.jpg"));
            var cropParams = new CropParameters(_cropModelId);

            var response = await _client.EnqueueAndGetResultAsync<CropResponse>(
                inputSource, cropParams);

            Assert.NotNull(response);
            Assert.Equal(2, response.Inference.Result.Crops.Count);

            var cropOperation = new Crop(inputSource);
            var extractedImages = cropOperation.ExtractCrops(response.Inference.Result.Crops);

            Assert.Equal(2, extractedImages.Count);
            Assert.Equal("default_sample.jpg_page0-0.jpg", extractedImages[0].Filename);
            Assert.Equal("default_sample.jpg_page0-1.jpg", extractedImages[1].Filename);

            var extractionInput = extractedImages[0].AsInputSource();
            var findocParams = new ExtractionParameters(_findocModelId);

            var invoice0 = await _client.EnqueueAndGetResultAsync<ExtractionResponse>(
                extractionInput, findocParams);

            CheckFindocReturn(invoice0);

            extractedImages.SaveAllToDisk(_outputDir, 50);

            var file1Info = new FileInfo(Path.Combine(_outputDir, "crop_001.jpg"));
            Assert.InRange(file1Info.Length, 100000, 110000);

            var file2Info = new FileInfo(Path.Combine(_outputDir, "crop_002.jpg"));
            Assert.InRange(file2Info.Length, 100000, 110000);
        }

        [Fact(Timeout = 180000)]
        public async Task Extract_Crops_From_Each_Pdf_Page_Correctly()
        {

            var inputSource = new LocalInputSource(
                new FileInfo(Path.Combine(Constants.V2ProductDir, "crop/multipage_sample.pdf")));

            var cropParams = new CropParameters(_cropModelId);

            var response = await _client.EnqueueAndGetResultAsync<CropResponse>(
                inputSource, cropParams);
            var cropOperation = new Crop(inputSource);
            var extractedImages = cropOperation.ExtractCrops(response.Inference.Result.Crops);

            Assert.Equal(5, extractedImages.Count);
            Assert.Equal("multipage_sample.pdf_page0-0.jpg", extractedImages[0].Filename);
            Assert.Equal("multipage_sample.pdf_page1-0.jpg", extractedImages[3].Filename);
        }
    }
}
