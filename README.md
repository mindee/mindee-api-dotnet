[![.NET 6](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml)
[![.NET 4.7.2](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet-fmk.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet-fmk.yml)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

# Mindee API Helper Library for .NET
The official Mindee API Helper Library for the .NET ecosystem to quickly and easily connect to Mindee's API services.

This library is compatible with:
* .NET Standard 2.0
* .NET 4.7.2
* .NET 6+

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
- From an environment variable
```
MindeeApiSettings__ApiKey
```
- From an appsettings.json file
```
"MindeeApiSettings": {
    "ApiKey": ""m-api-key"
},
```

### Instantiate from dependency injection (DI)
In your Startup.cs or Program.cs file, configure the dependency injection (DI) as follows:
```csharp
services.AddMindeeClient();
```
This call will configure the client entry point and the pdf library used internally.

Then, in your controller or service instance, pass as an argument the class ``MindeeClient``.


### Instantiate manually
Or, you could also simply instantiate a new instance of `MindeeClient`:
```csharp
using Mindee;

var mindeeClient = new MindeeClient(
            Options.Create(new MindeeSettings() { ApiKey = "MyKey" }));
```

Let's parse an invoice:
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo("/path/to/the/file.ext"))
    .ParseAsync<InvoiceV4Prediction>();
```

### Usage
You can also use the client with your custom documents:
```csharp
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "wnine",
    accountName: "john",
    version: "1.1" // optional
);

var prediction = await _mindeeClient
    .LoadDocument(new FileInfo("/path/to/the/file.ext"))
    .ParseAsync(myEndpoint);
```

## Further Reading
There's more to it than that for those that need more features, or want to
customize the experience.

All the juicy details are described in the **[Official Guide](docs/index.md)**.

You also can use the **[Reference documentation](docs/api-reference/Mindee/index.md)** to dig deeper in the code.

## License
Copyright © Mindee

Available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).
