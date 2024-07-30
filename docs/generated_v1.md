---
title: Generated OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-generated-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports generated APIs.  
Generated APIs can theoretically support all APIs in a catch-all generic format.

# Quick-Start

```csharp
using Mindee;
using Mindee.Input;
using Mindee.Http;
using Mindee.Product.Generated;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Set the endpoint configuration
CustomEndpoint endpoint = new CustomEndpoint(
    endpointName: "my-endpoint",
    accountName: "my-account",
    version: "my-version"
);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<GeneratedV1>(inputSource, endpoint);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

# Generated Endpoints

As shown above, you will need to provide an account and an endpoint name at the very least.

Although it is optional, the version number should match the latest version of your build in most use-cases.  
If it is not set, it will default to "1".

# Field Types

## Generated Fields

By default, GeneratedV1 implements only one attribute:

- **Fields** (`Dictionary<string, `[GeneratedFeature](#generated-feature)`>`): Contains a dictionary of all features contained in a prediction.

### Generated Feature

A `GeneratedFeature` is a special type of custom list that extends `ArrayList<`[GeneratedObject](#generated-object-field)`>` and implements the following:

- **AsStringField()** (`StringField`): the value of the field as a `StringField`.
- **AsAmountField()** (`AmountField`): the value of the field as an `AmountField`.
- **AsDecimalField()** (`AmountField`): the value of the field as a `DecimalField`.
- **AsDateField()** (`DateField`): the value of the field as a `DateField`.
- **AsClassificationField()** (`ClassificationField`): the value of the field as a `ClassificationField`.

### Generated Object Field

By default, non-list objects will be stored in a `GeneratedObject` structure, which are an extension of simple hashmaps. These fields have access to the following:

- **IsList** (`bool`): Whether the field is a list-like object or not.
- **AsStringField()** (`StringField`): the value of the field as a `StringField`.
- **AsAmountField()** (`AmountField`): the value of the field as an `AmountField`.
- **AsDecimalField()** (`AmountField`): the value of the field as a `DecimalField`.
- **AsDateField()** (`DateField`): the value of the field as a `DateField`.
- **AsClassificationField()** (`ClassificationField`): the value of the field as a `ClassificationField`.
- **Polygon()** (`Polygon`): representation of the field as a `Polygon`.
- **PageId()** (`integer`): retrieves the ID of the page the field was found on. Note: this isn't supported on some APIs.
- **Confidence()** (`float`): retrieves the confidence score for a field, if it exists.

> Note: the `AsXXXXField()` methods mentioned above will return `null` if a value isn't found.

# Questions?

[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
