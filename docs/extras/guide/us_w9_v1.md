---
title: US W9 OCR .NET
---
The .NET OCR SDK supports the [W9 API](https://platform.mindee.com/mindee/us_w9).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/us_w9/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![W9 sample](https://github.com/mindee/client-lib-test-data/blob/main/products/us_w9/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.W9;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<W9V1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

```

**Output (RST):**
```rst
########
Document
########
:Mindee ID: d7c5b25f-e0d3-4491-af54-6183afa1aaab
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/us_w9 v1.0
:Rotation applied: Yes

Prediction
==========

Page Predictions
================

Page 0
------
:Name: Stephen W Hawking
:SSN: 560758145
:Address: Somewhere In Milky Way
:City State Zip: Probably Still At Cambridge P O Box CB1
:Business Name:
:EIN: 942203664
:Tax Classification: individual
:Tax Classification Other Details:
:W9 Revision Date: august 2013
:Signature Position: Polygon with 4 points.
:Signature Date Position:
:Tax Classification LLC:
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


### PositionField
The position field `PositionField` implements:

* **BoundingBox** (`BoundingBox`): contains exactly 4 relative vertices (points) coordinates of a right rectangle containing the field in the document.
* **Polygon** (`Polygon`): contains the relative vertices coordinates (`Polygon` extends `List<Point>`) of a polygon containing the field in the image.
* **Rectangle** (`Polygon`): a Polygon with four points that may be oriented (even beyond canvas).
* **Quadrangle** (`Quadrangle`): a free polygon made up of four points.

## Page-Level Fields
Some fields are constrained to the page level, and so will not be retrievable at document level.

# Attributes
The following fields are extracted for W9 V1:

## Address
[📄](#page-level-fields "This field is only present on individual pages.")**Address** : The street address (number, street, and apt. or suite no.) of the applicant.

```cs
foreach (var AddressElem in result.Document.Address)
{
    System.Console.WriteLine(AddressElem)
      .Value;
}
```

## Business Name
[📄](#page-level-fields "This field is only present on individual pages.")**BusinessName** : The business name or disregarded entity name, if different from Name.

```cs
foreach (var BusinessNameElem in result.Document.BusinessName)
{
    System.Console.WriteLine(BusinessNameElem)
      .Value;
}
```

## City State Zip
[📄](#page-level-fields "This field is only present on individual pages.")**CityStateZip** : The city, state, and ZIP code of the applicant.

```cs
foreach (var CityStateZipElem in result.Document.CityStateZip)
{
    System.Console.WriteLine(CityStateZipElem)
      .Value;
}
```

## EIN
[📄](#page-level-fields "This field is only present on individual pages.")**Ein** : The employer identification number.

```cs
foreach (var EinElem in result.Document.Ein)
{
    System.Console.WriteLine(EinElem)
      .Value;
}
```

## Name
[📄](#page-level-fields "This field is only present on individual pages.")**Name** : Name as shown on the applicant's income tax return.

```cs
foreach (var NameElem in result.Document.Name)
{
    System.Console.WriteLine(NameElem)
      .Value;
}
```

## Signature Date Position
[📄](#page-level-fields "This field is only present on individual pages.")**SignatureDatePosition** : Position of the signature date on the document.

```cs
foreach (var SignatureDatePositionElem in result.Document.SignatureDatePosition)
{
    System.Console.WriteLine(SignatureDatePositionElem).Polygon;
}
```

## Signature Position
[📄](#page-level-fields "This field is only present on individual pages.")**SignaturePosition** : Position of the signature on the document.

```cs
foreach (var SignaturePositionElem in result.Document.SignaturePosition)
{
    System.Console.WriteLine(SignaturePositionElem).Polygon;
}
```

## SSN
[📄](#page-level-fields "This field is only present on individual pages.")**Ssn** : The applicant's social security number.

```cs
foreach (var SsnElem in result.Document.Ssn)
{
    System.Console.WriteLine(SsnElem)
      .Value;
}
```

## Tax Classification
[📄](#page-level-fields "This field is only present on individual pages.")**TaxClassification** : The federal tax classification, which can vary depending on the revision date.

```cs
foreach (var TaxClassificationElem in result.Document.TaxClassification)
{
    System.Console.WriteLine(TaxClassificationElem)
      .Value;
}
```

## Tax Classification LLC
[📄](#page-level-fields "This field is only present on individual pages.")**TaxClassificationLlc** : Depending on revision year, among S, C, P or D for Limited Liability Company Classification.

```cs
foreach (var TaxClassificationLlcElem in result.Document.TaxClassificationLlc)
{
    System.Console.WriteLine(TaxClassificationLlcElem)
      .Value;
}
```

## Tax Classification Other Details
[📄](#page-level-fields "This field is only present on individual pages.")**TaxClassificationOtherDetails** : Tax Classification Other Details.

```cs
foreach (var TaxClassificationOtherDetailsElem in result.Document.TaxClassificationOtherDetails)
{
    System.Console.WriteLine(TaxClassificationOtherDetailsElem)
      .Value;
}
```

## W9 Revision Date
[📄](#page-level-fields "This field is only present on individual pages.")**W9RevisionDate** : The Revision month and year of the W9 form.

```cs
foreach (var W9RevisionDateElem in result.Document.W9RevisionDate)
{
    System.Console.WriteLine(W9RevisionDateElem)
      .Value;
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
