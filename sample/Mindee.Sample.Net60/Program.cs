using System.Text.Json;
using Microsoft.Extensions.Options;
using Mindee.Parsing.Receipt;

namespace Mindee.Sample.Net60
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            string filePath = @"C:\Users\33621\Downloads\t2i.pdf";

            var mindeeClient = new MindeeClient(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }));

            var receiptV4Prediction = await mindeeClient.LoadDocument(new FileInfo(filePath))
                .ParseAsync<ReceiptV4Prediction>();

            Console.Write(JsonSerializer.Serialize(receiptV4Prediction, new JsonSerializerOptions { WriteIndented = true }));
            Console.ReadKey();
        }
    }
}
