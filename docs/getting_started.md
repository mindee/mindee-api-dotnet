---
title: '.NET Library: Overview'
category: 622b805aaec68102ea7fcbc2
slug: dotnet-ocr-overview
parentDoc: 6357abb22e33070016cbda4b
---
This guide will help you get the most out of the Mindee .NET OCR SDK to easily extract data from your documents.

## Installation

### Prerequisites
This library is compatible with:
* .NET 4.7.2
* .NET 6+

You'll also need NuGet for installing the package.

### Standard Installation
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

### Development Installation
If you plan to update the source code, you'll need to follow these steps to get started.

1. First clone the repo.

```shell
git clone git@github.com:mindee/mindee-api-dotnet.git
```

2. Navigate to the cloned directory and install all required libraries.

```shell
dotnet restore 
```

## Updating the Library
It is important to always check the version of the Mindee OCR SDK you are using, as new and updated
features won’t work on older versions.

To get the latest version of your OCR SDK:
```shell
dotnet update package Mindee
```

To install a specific version of Mindee:
```shell
dotnet add package Mindee -v <VERSION>
```

## Usage
Using Mindee's APIs can be broken down into the following steps:
1. [Get a `MindeeClient`](#initializing-the-client)
2. [Load a file](#loading-a-document-file)
3. [Send the file](#sending-a-document) to Mindee's API
4. [Process the result](#process-the-result) in some way

Let's take a deep dive into how this works.

### Initializing the Client
The `MindeeClient` enables you to load a document and execute the parse method on it, according to a specific model.

In most cases, you'll just to pass MindeeClient as a constructor parameter of your class and your DI engine will do the rest.

However, you will need to declare the MindeeClient in your Startup.cs or Program.cs file as follows:
```csharp
services.AddMindeeClient();
```

(Or, you could also simply instantiate a new instance of `MindeeClient` using one of `MindeeClientInit` methods.)

This call will configure the client entry point and the PDF library used internally.

Do not forget to initialize your [API key](https://developers.mindee.com/docs/make-your-first-request#create-an-api-key).

You must pass the value through arguments of your application, environment variables or from app settings directly.

#### Set the API key in the environment
API keys should be set as environment variables, especially for any production deployment.

The following environment variable will set the global API key:
```shell
Mindee__ApiKey="my-api-key"
```

You could also define the key in your appsettings.json config file:
```json
{
  "$schema": "https://json.schemastore.org/appsettings.json",
  "Mindee": {
    "ApiKey": "my-api-key"
  }
}
```

## Loading a file
Before being able to send a file to the API, it must be loaded first.

You don't need to worry about different MIME types, the library will take care of handling
all supported types automatically.

Once a file is loaded, interacting with it is done in exactly the same way, regardless
of how it was loaded.

There are a few different ways of loading a document file, depending on your use case:
* [Path](#path)
* [File Object](#stream-object)
* [Bytes](#bytes)

### Path
Load from a file directly from disk. Requires an absolute path, as a string.

```csharp
string filePath = "/path/to/the/file.ext";
var inputSource = new LocalInputSource(filePath);
```

### FileInfo
Load from a `FileInfo` object.

```csharp
string filePath = "/path/to/the/file.ext";
var inputSource = new LocalInputSource(filePath);
```

### Stream Object
Load a standard readable `Stream` object.

**Note**: The original filename of the encoded file is required when calling the method.

```csharp
Stream myStream;
string fileName = "myfile.pdf";
var inputSource = new LocalInputSource(myStream, fileName);
```

### Bytes
Load file contents from a byte array.

**Note**: The original filename of the encoded file is required when calling the method.

```csharp
byte[] myFileInBytes = new byte[] { byte.MinValue };
string fileName = "myfile.pdf";
var inputSource = new LocalInputSource(myFileInBytes, fileName);
```

## Parsing a file
To send a file to the API, we need to specify how to process the document.
This will determine which API endpoint is used and how the API return will be handled internally by the library.

More specifically, we need to set the class object which will represent the values extracted by the API.

The `ParseAsync` method is generic, and its return type will depend on it.

Each document type available in the library has its corresponding object class.
This is detailed in each document-specific guide.

### Off-the-Shelf Documents
Simply setting the correct class is enough:
```csharp
// Load an input source as a path string
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<InvoiceV4>(inputSource);
```

### Custom Documents
In this case, you will have two ways to handle them.

The first one enables the possibility to use a class object which represents a kind of dictionary where,
keys will be the name of each field define in your Custom API model (on the Mindee platform).

It also requires that you instantiate a new `CustomEndpoint` object to define the information of your custom API built.
```csharp
// Set the endpoint configuration
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "my-endpoint",
    accountName: "my-account"
    // optionally, lock the version
    //, version: "1.1"
);

// Call the API and parse the input
var response = await mindeeClient.ParseAsync(
    inputSource, myEndpoint);
```

## Process the result
Regardless of the model, it will be encapsulated in a `Document` object and therefore will have the following attributes:
* `inference` — [Inference](#inference)
* `ocr` — [OCR data](#OCR)

### Inference
Regroup the prediction on all the pages of the document and the prediction for all the document.
* `Prediction` — [Document level prediction](#document-level-prediction)
* `Pages` — [Page level prediction](#page-level-prediction)

#### Document level prediction
The `prediction` attribute is an object specific to the type of document being processed.
It contains the data extracted from the entire document, all pages combined.

It's possible to have the same field in various pages, but at the document level,
only the highest confidence field data will be shown (this is all done automatically at the API level).

```csharp
// print a summary of the document-level info
_logger.Debug(response.Dcocument.Inference.Prediction.ToString());
```

The various attributes are detailed in these document-specific guides:
* [Invoice](dotnet-invoice-ocr.md)
* [Receipt](dotnet-receipt-ocr.md)
* [Passport](dotnet-passport-ocr.md)
* [custom-built API](dotnet-api-builder.md)

#### Page level prediction
The `pages` attribute is a list of `prediction` object of the same class as the [`prediction` attribute](#document-level-prediction).

Each page element contains the data extracted for a particular page of the document.
The order of the elements in the array matches the order of the pages in the document.

All response objects have this property, regardless of the number of pages.
Single page documents will have a single entry.

### OCR
The `ocr` attribute can be filled by the API.

This requires a `PredictOptions` object with `fullText` set to `true`.

This object is then sent to the `ParseAsync` method:
```csharp
var predictOptions = new PredictOptions(allWords: true);
var response = await mindeeClient.ParseAsync<InvoiceV4>(inputSource, predictOptions);

_logger.Debug(response.Document.Ocr.MvisionV1.Pages[0].AllWords);
```

It will contain all the words that have been read in the document.


&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
