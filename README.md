[![License: MIT](https://img.shields.io/github/license/mindee/mindee-api-dotnet)](https://opensource.org/licenses/MIT) [![GitHub Workflow Status](https://github.com/mindee/mindee-api-dotnet/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/mindee)](https://www.nuget.org/packages/Mindee) [![NuGet](https://img.shields.io/nuget/dt/Mindee)](https://www.nuget.org/packages/Mindee)

# Mindee API Helper Library for .NET
Quickly and easily connect to Mindee's API services using .NET.

## Requirements
The following .NET versions are tested and officially supported:
* Standard 2.0
* 4.7.2, 4.8 (Windows only)
* 6.0, 7.0, 8.0 (Linux, macOS x64, Windows)

## Quick Start
Here's the TL;DR of getting started.

First, get an [API Key](https://developers.mindee.com/docs/create-api-key)

Then, install this library:
```shell
dotnet add package Mindee
```

### Define the API Key
The API key is retrieved using `IConfiguration`.

So you could define it in multiple ways: 
* From an environment variable
```
Mindee__ApiKey
```
* From an appsettings.json file
```
"Mindee": {
    "ApiKey":  "my-api-key"
},
```

### Instantiate The Client
You can instantiate the client either manually or by using dependency injection.

#### Dependency Injection
In your Startup.cs or Program.cs file, configure the dependency injection (DI) as follows:
```csharp
services.AddMindeeClient();
```
This call will configure the client entry point and the PDF library used internally.

Then, in your controller or service instance, pass as an argument the class ``MindeeClient``.


#### Manually
Or, you could also simply instantiate a new instance of `MindeeClient`:
```csharp
using Mindee;

MindeeClient mindeeClient = new MindeeClient("my-api-key");
```

### Loading a File and Parsing It

#### Global Documents
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Invoice;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<InvoiceV4>(inputSource);

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

#### Region-Specific Documents
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.BankCheck;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<BankCheckV1>(inputSource);

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

#### Custom Documents (docTI & Custom APIs)

```csharp
using Mindee;
using Mindee.Http;
using Mindee.Parsing;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Set the endpoint configuration 
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "my-endpoint",
    accountName: "my-account"
    // optionally, lock the version
    //, version: "1.1"
);

// Call the API and parse the input
var response = await mindeeClient.EnqueueAndParseAsync(
    inputSource, myEndpoint);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

## Further Reading
Complete details on the working of the library are available in the following guides:

* [Getting started](https://developers.mindee.com/docs/dotnet-ocr-overview)
* [.NET Generated APIs](https://developers.mindee.com/docs/dotnet-generated-ocr)
* [.NET Custom APIs (API Builder - Deprecated)](https://developers.mindee.com/docs/dotnet-api-builder)
* [.NET Invoice OCR](https://developers.mindee.com/docs/dotnet-invoice-ocr)
* [.NET Receipt OCR](https://developers.mindee.com/docs/dotnet-receipt-ocr)
* [.NET Financial Document OCR](https://developers.mindee.com/docs/dotnet-financial-document-ocr)
* [.NET Passport OCR](https://developers.mindee.com/docs/dotnet-passport-ocr)
* [.NET Resume OCR](https://developers.mindee.com/docs/dotnet-resume-ocr)
* [.NET Proof of Address OCR](https://developers.mindee.com/docs/dotnet-proof-of-address-ocr)
* [.NET International Id OCR](https://developers.mindee.com/docs/dotnet-international-id-ocr)
* [.NET EU License Plate OCR](https://developers.mindee.com/docs/dotnet-eu-license-plate-ocr)
* [.NET EU Driver License OCR](https://developers.mindee.com/docs/dotnet-eu-driver-license-ocr)
* [.NET FR Bank Account Detail OCR](https://developers.mindee.com/docs/dotnet-fr-bank-account-details-ocr)
* [.NET FR Carte Grise OCR](https://developers.mindee.com/docs/dotnet-fr-carte-grise-ocr)
* [.NET FR Health Card OCR](https://developers.mindee.com/docs/dotnet-fr-health-card-ocr)
* [.NET FR ID Card OCR](https://developers.mindee.com/docs/dotnet-fr-carte-nationale-didentite-ocr)
* [.NET US Bank Check OCR](https://developers.mindee.com/docs/dotnet-us-bank-check-ocr)
* [.NET US W9 OCR](https://developers.mindee.com/docs/dotnet-us-w9-ocr)
* [.NET US Driver License OCR](https://developers.mindee.com/docs/dotnet-us-driver-license-ocr)
* [.NET Barcode Reader API](https://developers.mindee.com/docs/dotnet-barcode-reader-ocr)
* [.NET Cropper API](https://developers.mindee.com/docs/dotnet-cropper-ocr)
* [.NET Invoice Splitter API](https://developers.mindee.com/docs/dotnet-invoice-splitter-ocr)
* [.NET Multi Receipts Detector API](https://developers.mindee.com/docs/dotnet-multi-receipts-detector-ocr)

You can view the source code on [GitHub](https://github.com/mindee/mindee-api-dotnet).

You can also take a look at the
**[Reference Documentation](https://mindee.github.io/mindee-api-dotnet/)**.

## License
Copyright © Mindee

Available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).

## Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
