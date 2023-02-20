using Mindee;
using Mindee.Parsing.Receipt;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = MindeeClientInit.Create(apiKey);

var documentParsed = await mindeeClient
    .LoadDocument(File.OpenRead(filePath), System.IO.Path.GetFileName(filePath))
    .ParseAsync<ReceiptV4Inference>();

System.Console.WriteLine(documentParsed.ToString());
