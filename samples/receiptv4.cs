using Mindee;
using Mindee.Parsing.Receipt;

string apiKey = "MyApiKey";
string filePath = "MyFilePath";

MindeeClient mindeeClient = MindeeClientInit.Create(apiKey);

var documentParsed = await mindeeClient
    .LoadDocument(File.OpenRead(filePath), System.IO.Path.GetFileName(filePath))
    .ParseAsync<ReceiptV4Inference>();

System.Console.WriteLine(documentParsed.Inference.DocumentPrediction.ToString());
