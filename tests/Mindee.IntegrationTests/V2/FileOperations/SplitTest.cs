using Mindee.Input;
using Mindee.V2;
using Mindee.V2.FileOperations;
using Mindee.V2.Product.Extraction;
using Mindee.V2.Product.Extraction.Params;
using Mindee.V2.Product.Split;
using Mindee.V2.Product.Split.Params;

namespace Mindee.IntegrationTests.V2.FileOperations
{
    [Trait("Category", "V2")]
    [Trait("Category", "FileOperations")]
    public class SplitTest : IDisposable
    {
        private readonly string? _splitModelId;
        private readonly string? _findocModelId;
        private readonly Client _client;
        private readonly string _outputDir;

        public SplitTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _splitModelId = Environment.GetEnvironmentVariable("MindeeV2__Split__Model__Id");
            _findocModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");

            _outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
            if (!Directory.Exists(_outputDir))
            {
                Directory.CreateDirectory(_outputDir);
            }
        }

        public void Dispose()
        {
            var file1 = Path.Combine(_outputDir, "split_001.pdf");
            var file2 = Path.Combine(_outputDir, "split_002.pdf");

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
        public async Task Extract_Splits_From_Pdf_Correctly()
        {
            var inputSource = new LocalInputSource(
                Constants.V2ProductDir + "split/default_sample.pdf");
            var splitParams = new SplitParameters(_splitModelId);

            var response = await _client.EnqueueAndGetResultAsync<SplitResponse>(
                inputSource, splitParams);

            Assert.NotNull(response);
            Assert.Equal(2, response.Inference.Result.Splits.Count);

            var splitOperation = new Split(inputSource);
            var extractedSplits = splitOperation.ExtractSplits(
                response.Inference.Result.Splits.Select(s => s.PageRange).ToList());

            Assert.Equal(2, extractedSplits.Count);
            Assert.Equal("default_sample_001-001.pdf", extractedSplits[0].Filename);
            Assert.Equal("default_sample_002-002.pdf", extractedSplits[1].Filename);

            var extractionInput = extractedSplits[0].AsInputSource();
            var findocParams = new ExtractionParameters(_findocModelId);

            var invoice0 = await _client.EnqueueAndGetResultAsync<ExtractionResponse>(
                extractionInput, findocParams);

            CheckFindocReturn(invoice0);

            extractedSplits.SaveAllToDisk(_outputDir);

            for (int i = 0; i < extractedSplits.Count; i++)
            {
                var fileName = $"split_{i + 1:D3}.pdf";
                var filePath = Path.Combine(_outputDir, fileName);
                var fileInfo = new FileInfo(filePath);

                Assert.True(fileInfo.Exists);
                Assert.True(fileInfo.Length > 0);

                var localInput = new LocalInputSource(fileInfo);
                Assert.Equal(extractedSplits[i].PageCount, localInput.GetPageCount());
            }
        }
    }
}
