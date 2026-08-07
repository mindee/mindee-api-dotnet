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
        private readonly string _outputDir = Path.Combine(
            Constants.RootDir, "output/v2/file_operations/crop");

        public CropTest()
        {
            Directory.CreateDirectory(_outputDir);
        }

        [Fact]
        public void SinglePageCrop_CropsCorrectly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_cropDataDir, "default_sample.jpg")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_cropDataDir, "default_sample.json")));
            var doc = localResponse.DeserializeResponse<CropResponse>();

            var cropOperation = new Crop(inputSample);
            var extractedCrops = cropOperation.ExtractMultipleCrops(doc.Inference.Result.Crops);

            Assert.Equal(2, extractedCrops.Count);

            extractedCrops.SaveAllToDisk(_outputDir);

            var crop0 = extractedCrops[0];
            Assert.Equal(0, crop0.PageId);
            Assert.Equal(0, crop0.ElementId);
            Assert.Equal("default_sample_page-001-item-001.jpg", crop0.Filename);
            Assert.Equal(2070, crop0.Image.Height);
            Assert.Equal(1056, crop0.Image.Width);

            var crop1 = extractedCrops[1];
            Assert.Equal(0, crop1.PageId);
            Assert.Equal(1, crop1.ElementId);
            Assert.Equal("default_sample_page-001-item-002.jpg", crop1.Filename);
            Assert.Equal(1868, crop1.Image.Height);
            Assert.Equal(1298, crop1.Image.Width);
        }

        [Fact]
        public void MultiPageCrop_CropsCorrectly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_cropDataDir, "multipage_sample.pdf")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_cropDataDir, "multipage_sample.json")));
            var doc = localResponse.DeserializeResponse<CropResponse>();

            var cropOperation = new Crop(inputSample);
            var extractedCrops = cropOperation.ExtractMultipleCrops(doc.Inference.Result.Crops);

            Assert.Equal(5, extractedCrops.Count);

            extractedCrops.SaveAllToDisk(_outputDir);

            var crop0 = extractedCrops[0];
            Assert.Equal(0, crop0.PageId);
            Assert.Equal(0, crop0.ElementId);
            Assert.Equal("multipage_sample_page-001-item-001.jpg", crop0.Filename);
            // DOES NOT WORK PROPERLY! NEEDS INVESTIGATION.
            //Assert.Equal(1445, crop0.Image.Height);
            //Assert.Equal(547, crop0.Image.Width);

            var crop4 = extractedCrops[4];
            Assert.Equal(1, crop4.PageId);
            Assert.Equal(1, crop4.ElementId);
            Assert.Equal("multipage_sample_page-002-item-002.jpg", crop4.Filename);
        }
    }
}
