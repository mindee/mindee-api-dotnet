## Quick Start
Here's the TL;DR of getting started.

First, get an [API Key](https://developers.mindee.com/docs/create-api-key)

Then, install this library:
```shell
dotnet add package Mindee
```

In your Startup.cs or Program.cs file, configure the dependency injection (DI) as follows:
```csharp
services.AddMindeeClient();
```

This call will configure the client entry point and the PDF library used internally.

Then, in your controller or service instance, pass the class ``MindeeClient`` as an argument of your constructor
(or methods if you are using this possibility).

Let's parse an invoice:
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo("/path/to/the/file.ext"))
    .ParseAsync<ReceiptV4Inference>();
```

You can also use the client with your custom documents:
```csharp
CustomEndpoint myEndpoint = new CustomEndpoint(
    EndpointName: "wnine",
    AccountName: "john",
    Version: "1.1" // optional
);

var prediction = await _mindeeClient
    .LoadDocument(new FileInfo("/path/to/the/file.ext"))
    .ParseAsync(myEndpoint);
```

Or even with a custom representation of your model:
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo("/path/to/the/file.ext"))
    .ParseAsync<YourAwesomeModel>();
```

Complete details on the working of the library are available in the following guides: 
* [Overview](dotnet-getting-started.md)
* [.NET Custom APIs OCR](dotnet-api-builder.md)
* [.NET invoices OCR](dotnet-invoice-ocr.md)
* [.NET receipts OCR](dotnet-receipt-ocr.md)
* [.NET passports OCR](dotnet-passport-ocr.md)
* [.NET shipping containers OCR](dotnet-shipping-containers-ocr.md)
* [.NET US bank check OCR](dotnet-us-bank-check-ocr.md)
* [.NET licenses plates OCR](dotnet-licenses-plates-ocr.md)
* [.NET API Reference](api-reference/Mindee/index.md)

You can view the source code on [GitHub](https://github.com/mindee/mindee-api-dotnet).

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
