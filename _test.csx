#!/usr/bin/env dotnet-script
#r "nuget: Mindee, *-*"
using Mindee;
using Mindee.Parsing.Us.BankCheck;

string apiKey = "";
string filePath = "tests/resources/pdf/blank_1.pdf";

MindeeClient mindeeClient = MindeeClientInit.Create(apiKey);

var documentParsed = await mindeeClient
    .LoadDocument(File.OpenRead(filePath), System.IO.Path.GetFileName(filePath))
    .ParseAsync<BankCheckV1Inference>();

System.Console.WriteLine(documentParsed.ToString());
