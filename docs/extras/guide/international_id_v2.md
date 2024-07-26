---
title: International ID OCR .NET
---
The .NET OCR SDK supports the [International ID API](https://platform.mindee.com/mindee/international_id).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/international_id/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![International ID sample](https://github.com/mindee/client-lib-test-data/blob/main/products/international_id/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.InternationalId;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<InternationalIdV2>(inputSource);

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
:Mindee ID: cfa20a58-20cf-43b6-8cec-9505fa69d1c2
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/international_id v2.0
:Rotation applied: No

Prediction
==========
:Document Type: IDENTIFICATION_CARD
:Document Number: 12345678A
:Surnames: MUESTRA
           MUESTRA
:Given Names: CARMEN
:Sex: F
:Birth Date: 1980-01-01
:Birth Place: CAMPO DE CRIPTANA CIUDAD REAL ESPANA
:Nationality: ESP
:Personal Number: BAB1834284<44282767Q0
:Country of Issue: ESP
:State of Issue: MADRID
:Issue Date:
:Expiration Date: 2030-01-01
:Address: C/REAL N13, 1 DCHA COLLADO VILLALBA MADRID MADRID MADRID
:MRZ Line 1: IDESPBAB1834284<44282767Q0<<<<
:MRZ Line 2: 8001010F1301017ESP<<<<<<<<<<<3
:MRZ Line 3: MUESTRA<MUESTRA<<CARMEN<<<<<<<
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


### ClassificationField
The classification field `ClassificationField` extends `BaseField`, but also implements:
* **Value** (`strong`): corresponds to the field value.

> Note: a classification field's `value is always a `string`.

### StringField
The text field `StringField` extends `BaseField`, but also implements:
* **Value** (`string`): corresponds to the field value.
* **RawValue** (`string`): corresponds to the raw value as it appears on the document.

### DateField
The date field `DateField` extends `StringField`, but also implements:

* **DateObject** (`DateTime?`): an accessible representation of the value as a C# object. Can be `null`.

# Attributes
The following fields are extracted for International ID V2:

## Address
**Address** : The physical address of the document holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Address.Value);
```

## Birth Date
**BirthDate** : The date of birth of the document holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.BirthDate.Value);
```

## Birth Place
**BirthPlace** : The place of birth of the document holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.BirthPlace.Value);
```

## Country of Issue
**CountryOfIssue** : The country where the document was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.CountryOfIssue.Value);
```

## Document Number
**DocumentNumber** : The unique identifier assigned to the document.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentNumber.Value);
```

## Document Type
**DocumentType** : The type of personal identification document.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentType.Value);
```

## Expiration Date
**ExpiryDate** : The date when the document becomes invalid.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## Given Names
**GivenNames** : The list of the document holder's given names.

```cs
foreach (GivenNamesElem in result.Document.Inference.Prediction.GivenNames)
{
    System.Console.WriteLine(GivenNamesElem.Value);
}
```

## Issue Date
**IssueDate** : The date when the document was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssueDate.Value);
```

## MRZ Line 1
**MrzLine1** : The Machine Readable Zone, first line.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.MrzLine1.Value);
```

## MRZ Line 2
**MrzLine2** : The Machine Readable Zone, second line.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.MrzLine2.Value);
```

## MRZ Line 3
**MrzLine3** : The Machine Readable Zone, third line.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.MrzLine3.Value);
```

## Nationality
**Nationality** : The country of citizenship of the document holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Nationality.Value);
```

## Personal Number
**PersonalNumber** : The unique identifier assigned to the document holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.PersonalNumber.Value);
```

## Sex
**Sex** : The biological sex of the document holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Sex.Value);
```

## State of Issue
**StateOfIssue** : The state or territory where the document was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.StateOfIssue.Value);
```

## Surnames
**Surnames** : The list of the document holder's family names.

```cs
foreach (SurnamesElem in result.Document.Inference.Prediction.Surnames)
{
    System.Console.WriteLine(SurnamesElem.Value);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
