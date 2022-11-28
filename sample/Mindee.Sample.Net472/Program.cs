﻿using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mindee.Parsing.Receipt;

namespace Mindee.Sample.Net472
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            string filePath = @"C:\Users\33621\Downloads\myReceipt.pdf";

            var mindeeClient = new MindeeClient(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }));

            var receiptV4Prediction = await mindeeClient.LoadDocument(new FileInfo(filePath))
                .ParseAsync<ReceiptV4Prediction>();

            Console.WriteLine(JsonSerializer.Serialize(receiptV4Prediction, new JsonSerializerOptions { WriteIndented = true }));
            Console.ReadKey();
        }
    }
}
