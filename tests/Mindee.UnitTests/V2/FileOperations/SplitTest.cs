using Mindee.Input;
using Mindee.V2.FileOperations;
using Mindee.V2.Parsing;
using Mindee.V2.Product.Split;

namespace Mindee.UnitTests.V2.FileOperations
{
    [Trait("Category", "V2")]
    [Trait("Category", "FileOperations")]
    public class SplitTest
    {
        private readonly string _splitDataDir = Path.Combine(Constants.V2ResourcePath, "products", "split");
        private readonly string _finDocDataDir = Path.Combine(
            Constants.V2ResourcePath, "products", "extraction", "financial_document");
        private readonly string _outputDir = Path.Combine(
            Constants.V2ResourcePath, "output/v2/file_operations/split");

        public SplitTest()
        {
            Directory.CreateDirectory(_outputDir);
        }

        [Fact]
        public void SinglePage_SplitsCorrectly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_finDocDataDir, "default_sample.jpg")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_splitDataDir, "split_single.json")));
            var doc = localResponse.DeserializeResponse<SplitResponse>();

            var splitOperation = new Split(inputSample);
            List<SplitRange> splits = doc.Inference.Result.Splits;
            var extractedSplits = splitOperation.ExtractMultipleSplits(splits.Select(s => s.PageRange).ToList());

            Assert.Single(extractedSplits);

            Assert.Equal(1, extractedSplits[0].PageCount);
        }

        [Fact]
        public void MultiplePages_SplitsCorrectly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_splitDataDir, "default_sample.pdf")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_splitDataDir, "default_sample.json")));
            var doc = localResponse.DeserializeResponse<SplitResponse>();

            var splitOperation = new Split(inputSample);
            List<SplitRange> splits = doc.Inference.Result.Splits;
            var extractedSplits = splitOperation.ExtractMultipleSplits(splits.Select(s => s.PageRange).ToList());

            Assert.Equal(2, extractedSplits.Count);

            extractedSplits.SaveAllToDisk(_outputDir);

            var split0 = extractedSplits[0];
            Assert.Equal("default_sample_pages-001-001.pdf", split0.Filename);
            Assert.Equal(1, split0.PageCount);
            Assert.Equal(new int[] { 0 }, split0.PageIndexes);

            var split1 = extractedSplits[1];
            Assert.Equal("default_sample_pages-002-002.pdf", split1.Filename);
            Assert.Equal(1, split1.PageCount);
            Assert.Equal(new int[] { 1 }, split1.PageIndexes);
        }
    }
}
