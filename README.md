[![License: MIT](https://img.shields.io/github/license/mindee/mindee-api-nodejs)](https://opensource.org/licenses/MIT)
[![GitHub Workflow Status](https://github.com/mindee/mindee-api-dotnet/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/mindee/mindee-api-dotnet/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/mindee?color=%23fd3246)](https://www.nuget.org/packages/Mindee)
[![NuGet](https://img.shields.io/nuget/dt/Mindee)](https://www.nuget.org/packages/Mindee)
[![Join Mindee on Slack](https://img.shields.io/badge/Slack-4A154B?style=flat&logo=slack&label=MindeeCommunity)](https://mindee-community.slack.com/join/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw#/shared-invite/email)

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

var mindeeClient = MindeeClientInit.Create("MyKey");
```

Let's parse an invoice:
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo("/path/to/the/file.ext"))
    .ParseAsync<InvoiceV4Inference>();
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

You also can use the **[Reference documentation](https://mindee.github.io/mindee-api-dotnet/api-reference/Mindee)** to dig deeper in the code.

Or feel free to reach us in our slack community channel :-) ! 

## License
Copyright © Mindee

Available as open source under the terms of the [MIT License](https://opensource.org/licenses/MIT).
