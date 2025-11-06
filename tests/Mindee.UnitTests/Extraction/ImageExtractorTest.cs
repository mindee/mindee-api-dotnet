using Mindee.Extraction;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Product.BarcodeReader;
using Mindee.Product.MultiReceiptsDetector;

namespace Mindee.UnitTests.Extraction
{
    [Trait("Category", "ImageExtractor")]
    public class ImageExtractorTest
    {
        [Fact]
        public async Task GivenAnImage_ShouldExtractPositionFields()
        {
            var image = new LocalInputSource("Resources/v1/products/multi_receipts_detector/default_sample.jpg");
            var response = await GetMultiReceiptsDetectorPrediction("complete");
            var inference = response.Document.Inference;

            var extractor = new ImageExtractor(image);
            Assert.Equal(1, extractor.GetPageCount());


            foreach (var page in inference.Pages)
            {
                var subImages = extractor.ExtractImagesFromPage(page.Prediction.Receipts, page.Id);
                for (int i = 0; i < subImages.Count; i++)
                {
                    var extractedImage = subImages[i];
                    Assert.NotNull(extractedImage.Image);
                    extractedImage.WriteToFile("Resources/output/");

                    var source = extractedImage.AsInputSource();
                    Assert.Equal(
                        $"default_sample_page-001_{i + 1:D3}.jpg",
                        source.Filename
                    );
                }
            }
        }

        [Fact]
        public async Task GivenAnImage_ShouldExtractValueFields()
        {
            var image = new LocalInputSource("Resources/v1/products/barcode_reader/default_sample.jpg");
            var response = await GetBarcodeReaderPrediction("complete");
            var inference = response.Document.Inference;

            var extractor = new ImageExtractor(image);
            Assert.Equal(1, extractor.GetPageCount());

            foreach (var page in inference.Pages)
            {
                var codes1D = extractor.ExtractImagesFromPage(page.Prediction.Codes1D, page.Id, "barcodes_1D.jpg");
                for (int i = 0; i < codes1D.Count; i++)
                {
                    var extractedImage = codes1D[i];
                    Assert.NotNull(extractedImage.Image);
                    var source = extractedImage.AsInputSource();
                    Assert.Equal(
                        $"barcodes_1D_page-001_{i + 1:D3}.jpg",
                        source.Filename
                    );
                    extractedImage.WriteToFile("Resources/output/");
                }

                var codes2D = extractor.ExtractImagesFromPage(page.Prediction.Codes2D, page.Id, "barcodes_2D.jpg");
                foreach (var extractedImage in codes2D)
                {
                    Assert.NotNull(extractedImage.Image);
                    extractedImage.WriteToFile("Resources/output/");
                }
            }
        }

        [Fact]
        public async Task GivenAPdf_ShouldExtractPositionFields()
        {
            var image = new LocalInputSource(
                "Resources/v1/products/multi_receipts_detector/multipage_sample.pdf");
            var response = await GetMultiReceiptsDetectorPrediction("multipage_sample");
            var inference = response.Document.Inference;

            var extractor = new ImageExtractor(image);
            Assert.Equal(2, extractor.GetPageCount());

            foreach (var page in inference.Pages)
            {
                var subImages = extractor.ExtractImagesFromPage(page.Prediction.Receipts, page.Id);

                for (int i = 0; i < subImages.Count; i++)
                {
                    var extractedImage = subImages[i];
                    Assert.NotNull(extractedImage.Image);
                    extractedImage.WriteToFile("Resources/output/");

                    var source = extractedImage.AsInputSource();
                    Assert.Equal(
                        $"multipage_sample_page-{page.Id + 1:D3}_{i + 1:D3}.jpg",
                        source.Filename
                    );
                }
            }
        }

        private static async Task<PredictResponse<MultiReceiptsDetectorV1>> GetMultiReceiptsDetectorPrediction(
            string name)
        {
            string fileName = $"Resources/v1/products/multi_receipts_detector/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<MultiReceiptsDetectorV1>(
                UnitTestBase.GetFakePredictParameter());
        }

        private static async Task<PredictResponse<BarcodeReaderV1>> GetBarcodeReaderPrediction(string name)
        {
            string fileName = $"Resources/v1/products/barcode_reader/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BarcodeReaderV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
