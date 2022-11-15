The .NET OCR SDK supports [custom-built API](https://developers.mindee.com/docs/build-your-first-document-parsing-api) from the API Builder.
If your document isn't covered by one of Mindee's Off-the-Shelf APIs, you can create your own API using the [API Builder](https://developers.mindee.com/docs/overview).

For the following examples, we are using our own [W9s custom API](https://developers.mindee.com/docs/w9-forms-ocr) created with the [API Builder](https://developers.mindee.com/docs/overview).

## Quick Start

```csharp
string path = "/src/mypath/myfile.pdf";
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(path))
    .ParseAsync(new Endpoint(
        "wsnine",
        "1", 
        "john"));
```

If the `version` argument is set, you'll be required to update it every time a new model is trained.
This is probably not needed for development but essential for production use.

## Parsing Documents
Use the `ParseAsync` method to call the API prediction on your custom document.
The response class and document type must be specified when calling this method.

You have two different ways to parse a custom document.

1. Use the default prediction class named ``CustomPrediction`` 
```csharp
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync(new Endpoint(
        ProductName,
        Version, 
        OrganizationName));
```

2. You can also use your own class which will represent the required field. For example, the custom objet below:
```csharp

// The Endpoint attribute is required when using your own model.
// It will be used to know which Mindee API called.
[Endpoint(productName: "wsnine", version:"1", organizationName:"john")]

public sealed class WNine
{
  [JsonPropertyName("name")]
  public StringField Name { get; set; }

  [JsonPropertyName("employer_id")]
  public StringField EmployerId { get; set; }
}

var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(Path))
    .ParseAsync<RentInsurance>();
```

## CustomPrediction object
All the fields which are defined in the API builder when creating your custom document, are available.

`CustomPrediction` is a `Dictionary` with the key as a `string` for the name of the field, and a `ListField` as a value, to get values extracted for this field. 

Value fields are accessed via the `Values` property.

> 📘 **Info**
>
> Both document level and page level objects work in the same way.

### Fields property
A Map with the following structure:
* `Confidence`: a `double`
* `Values`: a list of `ListFieldValue` which containing a list of all values found for the field.

In the examples below we'll use the `employer_id` field.

```csharp
string path = "/src/mypath/myfile.pdf";
var prediction = await _mindeeClient
    .LoadDocument(new FileInfo(path))
    .ParseAsync(new Endpoint(
        "wnine",
        "1", 
        "john"));

ListField? employerId = prediction.Inference.Prediction.GetValueOrDefault("employer_id");
```

&nbsp;
&nbsp;
**Questions?**
<img alt="Slack Logo Icon" style="display:inline!important" src="https://files.readme.io/5b83947-Slack.png" width="20" height="20">&nbsp;&nbsp;[Join our Slack](https://slack.mindee.com)
