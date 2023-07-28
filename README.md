[![License: MIT](https://img.shields.io/github/license/mindee/mindee-api-dotnet)](https://opensource.org/licenses/MIT) [![GitHub Workflow Status](https://github.com/mindee/mindee-api-dotnet/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml) [![NuGet](https://img.shields.io/nuget/v/mindee)](https://www.nuget.org/packages/Mindee) [![NuGet](https://img.shields.io/nuget/dt/Mindee)](https://www.nuget.org/packages/Mindee)

# Mindee API Helper Library for .NET
Quickly and easily connect to Mindee's API services using .NET.

## Requirements
The following .NET versions are tested and supported:
* Standard 2.0
* 4.7.2, 4.8 (Windows only)
* 6.0, 7.0 (Linux, macOS, Windows)

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
MindeeApiSettings__ApiKey
```
* From an appsettings.json file
```
"MindeeApiSettings": {
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

var mindeeClient = MindeeClientInit.Create("my-api-key");
```

### Loading a File and Parsing It

#### Global Documents
```csharp
using Mindee;
using Mindee.Product.Invoice;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// load an input source
var inputSource = new LocalInputSource(filePath));
//
// alternate input sources ...
// var inputSource = new LocalInputSource(IO.Stream fileStream, string filename)
// var inputSource = new LocalInputSource(IO.FileInfo fileinfo)
// 
// etc, check the docs ;-)

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<InvoiceV4>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

#### Region-Specific Documents
```csharp
using Mindee;
using Mindee.Parsing.Us.BankCheck;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// load an input source
var inputSource = new LocalInputSource(filePath));
//
// alternate input sources ...
// var inputSource = new LocalInputSource(IO.Stream fileStream, string filename)
// var inputSource = new LocalInputSource(IO.FileInfo fileinfo)
// 
// etc, check the docs ;-)

var response = await mindeeClient
    .ParseAsync<BankCheckV1Inference>();

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

#### Custom Document (API Builder)

```csharp
using Mindee;
using Mindee.Http;
using Mindee.Parsing;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

MindeeClient mindeeClient = new MindeeClient(apiKey);

// load an input source
var inputSource = new LocalInputSource(filePath));
//
// alternate input sources ...
// var inputSource = new LocalInputSource(IO.Stream fileStream, string filename)
// var inputSource = new LocalInputSource(IO.FileInfo fileinfo)
// 
// etc, check the docs ;-)

// Set the endpoint configuration 
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "my-endpoint",
    accountName: "my-account"
    // optionally, lock the version
    //, version: "1.1"
);

var response = await mindeeClient
    .LoadDocument(new FileInfo(filePath))
    .ParseAsync(myEndpoint);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

## Further Reading
Complete details on the working of the library are available in the following guides:

* [Overview](https://developers.mindee.com/docs/dotnet-ocr-overview)
* [Custom APIs](https://developers.mindee.com/docs/dotnet-api-builder)
* [Invoices](https://developers.mindee.com/docs/dotnet-invoice-ocr)
* [Receipts](https://developers.mindee.com/docs/dotnet-receipt-ocr)
* [Passports](https://developers.mindee.com/docs/dotnet-passport-ocr)
* [US bank Checks](https://developers.mindee.com/docs/dotnet-us-bank-check-ocr)
* [Licenses Plates](https://developers.mindee.com/docs/dotnet-licence-plates-ocr)

You can view the source code on [GitHub](https://github.com/mindee/mindee-api-nodejs).

You can also take a look at the
**[Reference Documentation](https://mindee.github.io/mindee-api-dotnet/)**.

## License
Copyright © Mindee

Available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).

## Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
