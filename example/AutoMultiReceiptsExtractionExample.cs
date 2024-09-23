using Mindee;
using Mindee.Extraction;
using Mindee.Input;
using Mindee.Product.MultiReceiptsDetector;
using Mindee.Product.Receipt;

var apiKey = "my-api-key";
var mindeeClient = new MindeeClient(apiKey);
var myFilePath = "/home/sebastian/work/mindee/client-lib-test-data/products/multi_receipts_detector/default_sample.jpg";

var inputSource = new LocalInputSource(myFilePath);
var resutlSplit = await mindeeClient.ParseAsync<MultiReceiptsDetectorV1>(inputSource);

var imageExtractor = new ImageExtractor(inputSource);

foreach (var page in resutlSplit.Document.Inference.Pages)
{
    var subImages = imageExtractor.ExtractImagesFromPage(page.Prediction.Receipts, page.Id);
    foreach (var subImage in subImages)
    {
        // subImage.WriteToFile($"/path/to/my/extracted/file/folder"); // Optionally: write to a file
        var resultReceipt = await mindeeClient.ParseAsync<ReceiptV5>(subImage.AsInputSource());
        Console.WriteLine(resultReceipt.Document);
    }
}
