using Mindee;
using Mindee.Extraction;
using Mindee.Input;
using Mindee.Product.MultiReceiptsDetector;
using Mindee.Product.Receipt;


async Task processReceipts(string filePath)
{
    var mindeeClient = new MindeeClient("my-api-key");
    var inputSource = new LocalInputSource(filePath);
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
}


var myFilePath = "path/to/my/file.ext";

await processReceipts(myFilePath);
