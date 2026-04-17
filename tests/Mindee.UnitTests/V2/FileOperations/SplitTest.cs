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
        private readonly string _splitDataDir = Path.Combine(Constants.V2RootDir, "products", "split");
        private readonly string _finDocDataDir = Path.Combine(Constants.V2RootDir, "products", "extraction", "financial_document");

        [Fact]
        public void Processes_SinglePage_Split_Correctly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_finDocDataDir, "default_sample.jpg")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_splitDataDir, "split_single.json")));
            var doc = localResponse.DeserializeResponse<SplitResponse>();

            var splitOperation = new Split(inputSample);
            List<SplitRange> splits = doc.Inference.Result.Splits;
            var extractedSplits = splitOperation.ExtractSplits(splits.Select(s => s.PageRange).ToList());

            Assert.Single(extractedSplits);

            Assert.Equal(1, extractedSplits[0].PageCount);
        }

        [Fact]
        public void Processes_MultiPage_ReceiptSplit_Correctly()
        {
            var inputSample = new LocalInputSource(
                new FileInfo(Path.Combine(_splitDataDir, "invoice_5p.pdf")));

            var localResponse = new LocalResponse(
                new FileInfo(Path.Combine(_splitDataDir, "split_multiple.json")));
            var doc = localResponse.DeserializeResponse<SplitResponse>();

            var splitOperation = new Split(inputSample);
            List<SplitRange> splits = doc.Inference.Result.Splits;
            var extractedSplits = splitOperation.ExtractSplits(splits.Select(s => s.PageRange).ToList());

            Assert.Equal(3, extractedSplits.Count);

            Assert.Equal(1, extractedSplits[0].PageCount);
            Assert.Equal(3, extractedSplits[1].PageCount);
            Assert.Equal(1, extractedSplits[2].PageCount);
        }
    }
}
