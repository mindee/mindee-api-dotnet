[![.NET 6](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml)
[![.NET 4.7.2](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet-fmk.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet-fmk.yml)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

# Mindee API Helper Library for .NET

The official Mindee API Helper Library for the .NET ecosystem to quickly and easily connect to Mindee's API services

It targets .NET Standard 2.0 to be used with .NET Framework 4.7.2 as well that with .NET 5/6.

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

Then, in your controller or service instance, pass as an argument the class ``MindeeClient``.

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

## Further Reading
There's more to it than that for those that need more features, or want to
customize the experience.

All the juicy details are described in the
**[Official Guide](https://developers.mindee.com/docs/dotnet-ocr-sdk)**.

## License
Copyright © Mindee

Available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).
