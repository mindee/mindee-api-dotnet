---
title: IND Passport - India OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-ind-passport---india-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Passport - India API](https://platform.mindee.com/mindee/ind_passport).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/ind_passport/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Passport - India sample](https://github.com/mindee/client-lib-test-data/blob/main/products/ind_passport/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Ind.IndianPassport;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<IndianPassportV1>(inputSource);

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
:Mindee ID: cf88fd43-eaa1-497a-ba29-a9569a4edaa7
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/ind_passport v1.0
:Rotation applied: Yes

Prediction
==========
:Page Number: 1
:Country: IND
:ID Number: J8369854
:Given Names: JOCELYN MICHELLE
:Surname: DOE
:Birth Date: 1959-09-23
:Birth Place: GUNDUGOLANU
:Issuance Place: HYDERABAD
:Gender: F
:Issuance Date: 2011-10-11
:Expiry Date: 2021-10-10
:MRZ Line 1: P<DOE<<JOCELYNMICHELLE<<<<<<<<<<<<<<<<<<<<<
:MRZ Line 2: J8369854<4IND5909234F2110101<<<<<<<<<<<<<<<8
:Legal Guardian:
:Name of Spouse:
:Name of Mother:
:Old Passport Date of Issue:
:Old Passport Number:
:Address Line 1:
:Address Line 2:
:Address Line 3:
:Old Passport Place of Issue:
:File Number:
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

# Attributes
The following fields are extracted for Passport - India V1:

## Address Line 1
**Address1**: The first line of the address of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Address1.Value);
```

## Address Line 2
**Address2**: The second line of the address of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Address2.Value);
```

## Address Line 3
**Address3**: The third line of the address of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Address3.Value);
```

## Birth Date
**BirthDate**: The birth date of the passport holder, ISO format: YYYY-MM-DD.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BirthDate.Value);
```

## Birth Place
**BirthPlace**: The birth place of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BirthPlace.Value);
```

## Country
**Country**: ISO 3166-1 alpha-3 country code (3 letters format).

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Country.Value);
```

## Expiry Date
**ExpiryDate**: The date when the passport will expire, ISO format: YYYY-MM-DD.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## File Number
**FileNumber**: The file number of the passport document.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.FileNumber.Value);
```

## Gender
**Gender**: The gender of the passport holder.

#### Possible values include:
 - 'M'
 - 'F'

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Gender.Value);
```

## Given Names
**GivenNames**: The given names of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.GivenNames.Value);
```

## ID Number
**IdNumber**: The identification number of the passport document.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IdNumber.Value);
```

## Issuance Date
**IssuanceDate**: The date when the passport was issued, ISO format: YYYY-MM-DD.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IssuanceDate.Value);
```

## Issuance Place
**IssuancePlace**: The place where the passport was issued.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IssuancePlace.Value);
```

## Legal Guardian
**LegalGuardian**: The name of the legal guardian of the passport holder (if applicable).

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.LegalGuardian.Value);
```

## MRZ Line 1
**Mrz1**: The first line of the machine-readable zone (MRZ) of the passport document.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz1.Value);
```

## MRZ Line 2
**Mrz2**: The second line of the machine-readable zone (MRZ) of the passport document.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz2.Value);
```

## Name of Mother
**NameOfMother**: The name of the mother of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.NameOfMother.Value);
```

## Name of Spouse
**NameOfSpouse**: The name of the spouse of the passport holder (if applicable).

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.NameOfSpouse.Value);
```

## Old Passport Date of Issue
**OldPassportDateOfIssue**: The date of issue of the old passport (if applicable), ISO format: YYYY-MM-DD.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.OldPassportDateOfIssue.Value);
```

## Old Passport Number
**OldPassportNumber**: The number of the old passport (if applicable).

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.OldPassportNumber.Value);
```

## Old Passport Place of Issue
**OldPassportPlaceOfIssue**: The place of issue of the old passport (if applicable).

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.OldPassportPlaceOfIssue.Value);
```

## Page Number
**PageNumber**: The page number of the passport document.

#### Possible values include:
 - '1'
 - '2'

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PageNumber.Value);
```

## Surname
**Surname**: The surname of the passport holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Surname.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
