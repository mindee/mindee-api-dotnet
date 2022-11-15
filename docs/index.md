## Quick Start
Here's the TL;DR of getting started.

First, get an [API Key](https://developers.mindee.com/docs/create-api-key)

Then, install this library.

Using the .NET Core command-line interface (CLI) tools:
```shell
dotnet add package Mindee
```
Or using the NuGet Command Line Interface (CLI):
```shell
nuget install Mindee
```
Or using the Package Manager Console:
```shell
Install-Package Mindee
```

In your Startup.cs or Program.cs file, configure the DI as below :
```csharp
services.AddMindeeClient();
```
This call will configure the client entry point and the pdf library used internally.

Then, in your controller or service instance, pass the class ``MindeeClient`` as an argument of your constructor (or methods if you are using this possibility).

Let's parse an invoice :
```csharp
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<ReceiptV3Prediction>();
```

You can also use the client with your custom documents:
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync(new Endpoint(
        ProductName,
        Version, 
        OrganizationName));
```
or with you custom representation of your model!
```csharp
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<YourAwesomeModel>();
```

Complete details on the working of the library are available in the following guides: 
* [Overview](https://developers.mindee.com/docs/dotnet-overview-1)
* [.NET Custom APIs OCR](https://developers.mindee.com/docs/dotnet-api-builder-1)
* [.NET invoices OCR](https://developers.mindee.com/docs/dotnet-invoice-ocr-1)
* [.NET receipts OCR](https://developers.mindee.com/docs/dotnet-receipt-ocr-1)
* [.NET passports OCR](https://developers.mindee.com/docs/dotnet-passport-ocr-1)
* [Mindee API Reference](https://github.com/mindee/mindee-api-dotnet/blob/main/docs/api-reference/Mindee)

You can view the source code on [GitHub](https://github.com/mindee/mindee-api-dotnet).

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
