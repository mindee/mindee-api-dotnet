---
title: FR Carte Vitale OCR .NET
---
The .NET OCR SDK supports the [Carte Vitale API](https://platform.mindee.com/mindee/carte_vitale).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/carte_vitale/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Carte Vitale sample](https://github.com/mindee/client-lib-test-data/blob/main/products/carte_vitale/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Fr.CarteVitale;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<CarteVitaleV1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

```

**Output (RST):**
```rst
########
Document
########
:Mindee ID: 8c25cc63-212b-4537-9c9b-3fbd3bd0ee20
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/carte_vitale v1.0
:Rotation applied: Yes

Prediction
==========
:Given Name(s): NATHALIE
:Surname: DURAND
:Social Security Number: 269054958815780
:Issuance Date: 2007-01-01

Page Predictions
================

Page 0
------
:Given Name(s): NATHALIE
:Surname: DURAND
:Social Security Number: 269054958815780
:Issuance Date: 2007-01-01
```

# Field Types
## Standard Fields
These fields are generic and used in several products.

### BaseField
Each prediction object contains a set of fields that inherit from the generic `BaseField` class.
A typical `BaseField` object will have the following attributes:

* **Confidence** (`double?`): the confidence score of the field prediction.
* **BoundingBox** (`BoundingBox`): contains exactly 4 relative vertices (points) coordinates of a right rectangle containing the field in the document.
* **Polygon** (`Polygon`): contains the relative vertices coordinates (`Polygon` extends `List<Point>`) of a polygon containing the field in the image.
* **PageId** (`int?`): the ID of the page, is `null` when at document-level.

> **Note:** A `Point` simply refers to a List of `double`.


Aside from the previous attributes, all basic fields have access to a custom `ToString` method that can be used to print their value as a string.

### StringField
The text field `StringField` extends `BaseField`, but also implements:
* **Value** (`string`): corresponds to the field value.
* **RawValue** (`string`): corresponds to the raw value as it appears on the document.

### DateField
The date field `DateField` extends `StringField`, but also implements:

* **DateObject** (`DateTime?`): an accessible representation of the value as a C# object. Can be `null`.

# Attributes
The following fields are extracted for Carte Vitale V1:

## Given Name(s)
**GivenNames** : The given name(s) of the card holder.

```cs
foreach (GivenNamesElem in result.Document.Inference.Prediction.GivenNames)
{
    System.Console.WriteLine(GivenNamesElem.Value);
}
```

## Issuance Date
**IssuanceDate** : The date the card was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssuanceDate.Value);
```

## Social Security Number
**SocialSecurity** : The Social Security Number (Numéro de Sécurité Sociale) of the card holder

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.SocialSecurity.Value);
```

## Surname
**Surname** : The surname of the card holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Surname.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
