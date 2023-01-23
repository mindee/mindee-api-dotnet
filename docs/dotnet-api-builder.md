The .NET OCR SDK supports [custom-built API](https://developers.mindee.com/docs/build-your-first-document-parsing-api) from the API Builder.
If your document isn't covered by one of Mindee's Off-the-Shelf APIs, you can create your own API using the [API Builder](https://developers.mindee.com/docs/overview).

For the following examples, we are using our own [W9s custom API](https://developers.mindee.com/docs/w9-forms-ocr) created with the [API Builder](https://developers.mindee.com/docs/overview).

## Quick Start

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

If the `version` argument is set, you'll be required to update it every time a new model is trained.
This is probably not needed for development but essential for production use.

## Parsing Documents
Use the `ParseAsync` method to call the API prediction on your custom document.
The response class and document type must be specified when calling this method.

You have two different ways to parse a custom document.

1. Don't specify the return class, and use the default one (named ``CustomPrediction``):
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync(myEndpoint);
```

2. You can also use your own class which will represent the required fields. For example:
```csharp

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
[CustomEndpoint(
    endpointName: "wnine",
    accountName: "john",
    version: "1.1" // optional
)]
public sealed class WNineV1Inference : Inference<WNineV1DocumentPrediction, WNineV1DocumentPrediction>
{
}

var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync<WNineV1Inference>();
```

## CustomV1Inference object
All the fields which are present in the API builder 
are available (the fields are defined when creating your custom API).

`CustomV1Inference` is an object which contains a document prediction and pages prediction result.
### `CustomV1PagePrediction` 
Which is a `Dictionnary<String, ListField>` with the key as a `string` for the name of the field, and a `ListField` as a value.

### `CustomV1DocumentPrediction` 
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
CustomEndpoint myEndpoint = new CustomEndpoint(
    endpointName: "wnine",
    accountName: "john",
    version: "1.1" // optional
);

var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(path))
    .ParseAsync(myEndpoint);

ListField? employerId = prediction.Inference.Pages.FirstOrDefault()?.Prediction.GetValueOrDefault("employer_id");
```

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
