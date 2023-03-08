using System;
using System.IO;
using System.Threading.Tasks;
using Mindee;
using Mindee.Parsing.Receipt;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        string filePath = @"Path\to\the\file.ext";

        var mindeeClient = MindeeClientInit.Create("MyKey");

        var receiptV4Prediction =
            await mindeeClient.LoadDocument(new FileInfo(filePath))
                .ParseAsync<ReceiptV4Inference>();

        Console.Write(receiptV4Prediction.ToString());
        Console.ReadKey();
    }
}
