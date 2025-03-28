---
title: FR Carte Nationale d'Identité OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-fr-carte-nationale-didentite-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Carte Nationale d'Identité API](https://platform.mindee.com/mindee/idcard_fr).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/idcard_fr/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Carte Nationale d'Identité sample](https://github.com/mindee/client-lib-test-data/blob/main/products/idcard_fr/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Fr.IdCard;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<IdCardV2>(inputSource);

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
:Mindee ID: d33828f1-ef7e-4984-b9df-a2bfaa38a78d
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/idcard_fr v2.0
:Rotation applied: Yes

Prediction
==========
:Nationality:
:Card Access Number: 175775H55790
:Document Number:
:Given Name(s): Victor
                Marie
:Surname: DAMBARD
:Alternate Name:
:Date of Birth: 1994-04-24
:Place of Birth: LYON 4E ARRONDISSEM
:Gender: M
:Expiry Date: 2030-04-02
:Mrz Line 1: IDFRADAMBARD<<<<<<<<<<<<<<<<<<075025
:Mrz Line 2: 170775H557903VICTOR<<MARIE<9404246M5
:Mrz Line 3:
:Date of Issue: 2015-04-03
:Issuing Authority: SOUS-PREFECTURE DE BELLE (02)

Page Predictions
================

Page 0
------
:Document Type: OLD
:Document Sides: RECTO & VERSO
:Nationality:
:Card Access Number: 175775H55790
:Document Number:
:Given Name(s): Victor
                Marie
:Surname: DAMBARD
:Alternate Name:
:Date of Birth: 1994-04-24
:Place of Birth: LYON 4E ARRONDISSEM
:Gender: M
:Expiry Date: 2030-04-02
:Mrz Line 1: IDFRADAMBARD<<<<<<<<<<<<<<<<<<075025
:Mrz Line 2: 170775H557903VICTOR<<MARIE<9404246M5
:Mrz Line 3:
:Date of Issue: 2015-04-03
:Issuing Authority: SOUS-PREFECTURE DE BELLE (02)
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

## Page-Level Fields
Some fields are constrained to the page level, and so will not be retrievable at document level.

# Attributes
The following fields are extracted for Carte Nationale d'Identité V2:

## Alternate Name
**AlternateName**: The alternate name of the card holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.AlternateName.Value);
```

## Issuing Authority
**Authority**: The name of the issuing authority.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Authority.Value);
```

## Date of Birth
**BirthDate**: The date of birth of the card holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BirthDate.Value);
```

## Place of Birth
**BirthPlace**: The place of birth of the card holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BirthPlace.Value);
```

## Card Access Number
**CardAccessNumber**: The card access number (CAN).

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CardAccessNumber.Value);
```

## Document Number
**DocumentNumber**: The document number.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentNumber.Value);
```

## Document Sides
[📄](#page-level-fields "This field is only present on individual pages.")**DocumentSide**: The sides of the document which are visible.

#### Possible values include:
 - 'RECTO'
 - 'VERSO'
 - 'RECTO & VERSO'

```csharp
foreach (var DocumentSideElem in result.Document.Inference.Prediction.DocumentSide)
{
    System.Console.WriteLine(DocumentSideElem)
      .Value;
}
```

## Document Type
[📄](#page-level-fields "This field is only present on individual pages.")**DocumentType**: The document type or format.

#### Possible values include:
 - 'NEW'
 - 'OLD'

```csharp
foreach (var DocumentTypeElem in result.Document.Inference.Prediction.DocumentType)
{
    System.Console.WriteLine(DocumentTypeElem)
      .Value;
}
```

## Expiry Date
**ExpiryDate**: The expiry date of the identification card.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## Gender
**Gender**: The gender of the card holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Gender.Value);
```

## Given Name(s)
**GivenNames**: The given name(s) of the card holder.

```csharp
foreach (var GivenNamesElem in result.Document.Inference.Prediction.GivenNames)
{
    System.Console.WriteLine(GivenNamesElem.Value);
}
```

## Date of Issue
**IssueDate**: The date of issue of the identification card.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IssueDate.Value);
```

## Mrz Line 1
**Mrz1**: The Machine Readable Zone, first line.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz1.Value);
```

## Mrz Line 2
**Mrz2**: The Machine Readable Zone, second line.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz2.Value);
```

## Mrz Line 3
**Mrz3**: The Machine Readable Zone, third line.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz3.Value);
```

## Nationality
**Nationality**: The nationality of the card holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Nationality.Value);
```

## Surname
**Surname**: The surname of the card holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Surname.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
