---
title: EU Driver License OCR .NET
---
The .NET OCR SDK supports the [Driver License API](https://platform.mindee.com/mindee/eu_driver_license).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/eu_driver_license/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Driver License sample](https://github.com/mindee/client-lib-test-data/blob/main/products/eu_driver_license/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Eu.DriverLicense;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<DriverLicenseV1>(inputSource);

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
:Mindee ID: b19cc32e-b3e6-4ff9-bdc7-619199355d54
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/eu_driver_license v1.0
:Rotation applied: Yes

Prediction
==========
:Country Code: FR
:Document ID: 13AA00002
:Driver License Category: AM A1 B1 B D BE DE
:Last Name: MARTIN
:First Name: PAUL
:Date Of Birth: 1981-07-14
:Place Of Birth: Utopiacity
:Expiry Date: 2018-12-31
:Issue Date: 2013-01-01
:Issue Authority: 99999UpiaCity
:MRZ: D1FRA13AA000026181231MARTIN<<9
:Address:

Page Predictions
================

Page 0
------
:Photo: Polygon with 4 points.
:Signature: Polygon with 4 points.
:Country Code: FR
:Document ID: 13AA00002
:Driver License Category: AM A1 B1 B D BE DE
:Last Name: MARTIN
:First Name: PAUL
:Date Of Birth: 1981-07-14
:Place Of Birth: Utopiacity
:Expiry Date: 2018-12-31
:Issue Date: 2013-01-01
:Issue Authority: 99999UpiaCity
:MRZ: D1FRA13AA000026181231MARTIN<<9
:Address:
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


### PositionField
The position field `PositionField` implements:

* **BoundingBox** (`BoundingBox`): contains exactly 4 relative vertices (points) coordinates of a right rectangle containing the field in the document.
* **Polygon** (`Polygon`): contains the relative vertices coordinates (`Polygon` extends `List<Point>`) of a polygon containing the field in the image.
* **Rectangle** (`Polygon`): a Polygon with four points that may be oriented (even beyond canvas).
* **Quadrangle** (`Quadrangle`): a free polygon made up of four points.

## Page-Level Fields
Some fields are constrained to the page level, and so will not be retrievable at document level.

# Attributes
The following fields are extracted for Driver License V1:

## Address
**Address**: EU driver license holders address

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Address.Value);
```

## Driver License Category
**Category**: EU driver license holders categories

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Category.Value);
```

## Country Code
**CountryCode**: Country code extracted as a string.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.CountryCode.Value);
```

## Date Of Birth
**DateOfBirth**: The date of birth of the document holder

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DateOfBirth.Value);
```

## Document ID
**DocumentId**: ID number of the Document.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentId.Value);
```

## Expiry Date
**ExpiryDate**: Date the document expires

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## First Name
**FirstName**: First name(s) of the driver license holder

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.FirstName.Value);
```

## Issue Authority
**IssueAuthority**: Authority that issued the document

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssueAuthority.Value);
```

## Issue Date
**IssueDate**: Date the document was issued

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssueDate.Value);
```

## Last Name
**LastName**: Last name of the driver license holder.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.LastName.Value);
```

## MRZ
**Mrz**: Machine-readable license number

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz.Value);
```

## Photo
[📄](#page-level-fields "This field is only present on individual pages.")**Photo**: Has a photo of the EU driver license holder

```cs
foreach (var PhotoElem in result.Document.Inference.Prediction.Photo)
{
    System.Console.WriteLine(PhotoElem).Polygon;
}
```

## Place Of Birth
**PlaceOfBirth**: Place where the driver license holder was born

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.PlaceOfBirth.Value);
```

## Signature
[📄](#page-level-fields "This field is only present on individual pages.")**Signature**: Has a signature of the EU driver license holder

```cs
foreach (var SignatureElem in result.Document.Inference.Prediction.Signature)
{
    System.Console.WriteLine(SignatureElem).Polygon;
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
