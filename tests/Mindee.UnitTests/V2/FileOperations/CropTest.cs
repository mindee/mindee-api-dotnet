using Mindee.Input;
using Mindee.V2.FileOperations;
using Mindee.V2.Parsing;
using Mindee.V2.Product.Crop;

namespace Mindee.UnitTests.V2.FileOperations
{
    [Trait("Category", "V2")]
    [Trait("Category", "FileOperations")]
    public class CropTest
    {
        private readonly string _cropDataDir = Path.Combine(Constants.V2RootDir, "products", "crop");

        [Fact]
        public void Processes_SinglePage_CropSplit_Correctly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_cropDataDir, "default_sample.jpg")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_cropDataDir, "crop_single.json")));
            var doc = localResponse.DeserializeResponse<CropResponse>();

            var cropOperation = new Crop(inputSample);
            var extractedCrops = cropOperation.ExtractCrops(doc.Inference.Result.Crops);

            Assert.Single(extractedCrops);

            Assert.Equal(0, extractedCrops[0].PageId);
            Assert.Equal(0, extractedCrops[0].ElementId);

            using var bitmap0 = extractedCrops[0].Image;
            Assert.Equal(2822, bitmap0.Width);
            Assert.Equal(1572, bitmap0.Height);
        }

        [Fact]
        public void Processes_MultiPage_ReceiptSplit_Correctly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_cropDataDir, "multipage_sample.pdf")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_cropDataDir, "crop_multiple.json")));
            var doc = localResponse.DeserializeResponse<CropResponse>();

            var cropOperation = new Crop(inputSample);
            var extractedCrops = cropOperation.ExtractCrops(doc.Inference.Result.Crops);

            Assert.Equal(2, extractedCrops.Count);

            Assert.Equal(0, extractedCrops[0].PageId);
            Assert.Equal(0, extractedCrops[0].ElementId);

            using var bitmap0 = extractedCrops[0].Image;
            Assert.Equal(156, bitmap0.Width);
            Assert.Equal(757, bitmap0.Height);

            Assert.Equal(0, extractedCrops[1].PageId);
            Assert.Equal(1, extractedCrops[1].ElementId);

            using var bitmap1 = extractedCrops[1].Image;
            Assert.Equal(188, bitmap1.Width);
            Assert.Equal(691, bitmap1.Height);
        }
    }
}
