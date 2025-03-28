---
title: Custom OCR .NET (Deprecated)
category: 622b805aaec68102ea7fcbc2
slug: dotnet-api-builder
parentDoc: 6357abb22e33070016cbda4b
---
> 🚧 This product is still supported, but is considered to be deprecated. If you are looking for the DocTI API documentation, you can find it [here](https://developers.mindee.com/docs/dotnet-generated-ocr).

## Quick Start

```csharp
using Mindee;
using Mindee.Input;
using Mindee.Http;
using Mindee.Parsing;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
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

var response = await mindeeClient.ParseAsync(
    inputSource, myEndpoint);

// Print a summary of the predictions
System.Console.WriteLine(response.Document.ToString());

// Print the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());
```

If the `version` argument is set, you'll be required to update it every time a new model is trained.
This is probably not needed for development, but essential for production use.

## Parsing Documents
Use the `ParseAsync` method to call the API prediction on your custom document.
The response class and document type must be specified when calling this method.

You have two different ways to parse a custom document.

1. Don't specify the return class, and use the default one (named ``CustomPrediction``):
```csharp
var response = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync(myEndpoint);
```

2. You can also use your own class which will represent the required fields. For example:
```csharp
using Mindee.Http;

public sealed class WNineV1DocumentPrediction
{
  [JsonPropertyName("name")]
  public StringField Name { get; set; }

  [JsonPropertyName("employer_id")]
  public StringField EmployerId { get; set; }
  
  (...)
}

// The CustomEndpoint attribute is required when using your own model.
// It will be used to know which Mindee API called.
[Endpoint(
    endpointName: "wnine",
    accountName: "john",
    // optionally, lock the version
    //version: "1.1"
)]
public sealed class WNineV1Inference : Inference<WNineV1DocumentPrediction, WNineV1DocumentPrediction>
{
}

var response = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync<WNineV1Inference>();
```

## CustomV1Inference object
All the fields which are present in the API builder 
are available (the fields are defined when creating your custom API).

`CustomV1` is an object which contains a document prediction and pages prediction result.
### `CustomV1Page` 
Which is a `Dictionnary<String, ListField>` with the key as a `string` for the name of the field, and a `ListField` as a value.

### `CustomV1Document` 
Which contains 2 properties : `ClassificationFields` and `Fields`. 
Both are a Map and the key is a `string` for the name of the field and for the value :
* `ClassificationFields` have a `ClassificationField` object as value. Each `ClassificationField` contains a value.
* `Fields` have a `ListField` object as value. Each `ListField` contains a list of all values extracted for this field.

> 📘 **Info**
>
> Both document level and page level objects work in the same way.


### Fields property
A Map with the following structure:
* `Confidence`: a `double`
* `Values`: a list of `ListFieldValue` which containing a list of all values found for the field.

In the examples below we'll use the `employer_id` field.

```csharp
Endpoint myEndpoint = new Endpoint(
    endpointName: "wnine",
    accountName: "john",
    // optionally, lock the version
    //version: "1.1"
);

var response = await _mindeeClient
    .LoadDocument(new FileInfo(path))
    .ParseAsync(myEndpoint);

ListField? employerId = response.Document.Inference.Pages.FirstOrDefault()?.Prediction.GetValueOrDefault("employer_id");
```

## Line items reconstructions
We offer the possibility to use post-processing for the prediction result from your custom API.
 
In the example below, imagine that your custom document has a table with 4 columns.
So, you want to extract all the line items (table rows) from it.

In that case, you will have to define 4 fields and do the annotation vertically for each one.

After training your model, test it using the Mindee client as below to get your document parsed and line items reconstructed.

```csharp
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "wnine",
    accountName: "john",
    // optionally, lock the version
    //, version: "1.1"
);

var response = await _mindeeClient
    .LoadDocument(new FileInfo(path))
    .ParseAsync(myEndpoint);

// The fields used to define the columns
var columnFieldNames = new List<string>() {
    "beneficiary_birth_date",
    "beneficiary_number",
    "beneficiary_name",
    "beneficiary_rank"
    };

// You can adjust the height variation of your lines.
// The value will depend on your table of course!
var lineHeigthTolerance = 0.011d;

// The anchor must be the column where there is always a value in your table. 
var anchor = new Anchor("beneficiary_name", lineHeigthTolerance);

var lineItems = LineItemsGenerator.Generate(
    columnFieldNames,
    response.Document.Inference.Prediction.Fields,
    anchor
    ); 
```

&nbsp;
&nbsp;
**Questions?**
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-1jv6nawjq-FDgFcF2T5CmMmRpl9LLptw)
