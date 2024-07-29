---
title: Passport OCR .NET
---
The .NET OCR SDK supports the [Passport API](https://platform.mindee.com/mindee/passport).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/passport/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Passport sample](https://github.com/mindee/client-lib-test-data/blob/main/products/passport/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Passport;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<PassportV1>(inputSource);

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
:Mindee ID: 18e41f6c-16cd-4f8e-8cd2-00ca02a35764
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/passport v1.0
:Rotation applied: Yes

Prediction
==========
:Country Code: GBR
:ID Number: 707797979
:Given Name(s): HENERT
:Surname: PUDARSAN
:Date of Birth: 1995-05-20
:Place of Birth: CAMTETH
:Gender: M
:Date of Issue: 2012-04-22
:Expiry Date: 2017-04-22
:MRZ Line 1: P<GBRPUDARSAN<<HENERT<<<<<<<<<<<<<<<<<<<<<<<
:MRZ Line 2: 7077979792GBR9505209M1704224<<<<<<<<<<<<<<00

Page Predictions
================

Page 0
------
:Country Code: GBR
:ID Number: 707797979
:Given Name(s): HENERT
:Surname: PUDARSAN
:Date of Birth: 1995-05-20
:Place of Birth: CAMTETH
:Gender: M
:Date of Issue: 2012-04-22
:Expiry Date: 2017-04-22
:MRZ Line 1: P<GBRPUDARSAN<<HENERT<<<<<<<<<<<<<<<<<<<<<<<
:MRZ Line 2: 7077979792GBR9505209M1704224<<<<<<<<<<<<<<00
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
* **PageId** (`int?`): the ID of the page, always `null` when at document-level.

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
The following fields are extracted for Passport V1:

## Date of Birth
**BirthDate**: The date of birth of the passport holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.BirthDate.Value);
```

## Place of Birth
**BirthPlace**: The place of birth of the passport holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.BirthPlace.Value);
```

## Country Code
**Country**: The country's 3 letter code (ISO 3166-1 alpha-3).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Country.Value);
```

## Expiry Date
**ExpiryDate**: The expiry date of the passport.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## Gender
**Gender**: The gender of the passport holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Gender.Value);
```

## Given Name(s)
**GivenNames**: The given name(s) of the passport holder.

```cs
foreach (var GivenNamesElem in result.Document.Inference.Prediction.GivenNames)
{
    System.Console.WriteLine(GivenNamesElem.Value);
}
```

## ID Number
**IdNumber**: The passport's identification number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IdNumber.Value);
```

## Date of Issue
**IssuanceDate**: The date the passport was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssuanceDate.Value);
```

## MRZ Line 1
**Mrz1**: Machine Readable Zone, first line

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz1.Value);
```

## MRZ Line 2
**Mrz2**: Machine Readable Zone, second line

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz2.Value);
```

## Surname
**Surname**: The surname of the passport holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Surname.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
