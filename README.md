[![.NET 6](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml)
[![.NET 4.7.2](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet-fmk.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet-fmk.yml)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

# Mindee API Helper Library for .NET
The official Mindee API Helper Library for the .NET ecosystem to quickly and easily connect to Mindee's API services.

This library is compatible with:
* .NET 4.7.2
* .NET 6+

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
This call will configure the client entry point and the pdf library used internally.

Then, in your controller or service instance, pass as an argument the class ``MindeeClient``.

Let's parse an invoice:
```csharp
var prediction = await _mindeeClient
    .LoadDocument(File.OpenRead(Path), System.IO.Path.GetFileName(Path))
    .ParseAsync<InvoiceV3Prediction>();
```

You can also use the client with your custom documents:
```csharp
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "wnine",
    accountName: "john",
    version: "1.1" // optional
);

var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync(myEndpoint);
```

## Further Reading
There's more to it than that for those that need more features, or want to
customize the experience.

All the juicy details are described in the
**[Official Guide](https://developers.mindee.com/docs/dotnet-ocr-sdk)**.

## License
Copyright © Mindee

Available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).
