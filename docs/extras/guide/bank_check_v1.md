---
title: US Bank Check OCR .NET
---
The .NET OCR SDK supports the [Bank Check API](https://platform.mindee.com/mindee/bank_check).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/bank_check/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Bank Check sample](https://github.com/mindee/client-lib-test-data/blob/main/products/bank_check/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.BankCheck;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<BankCheckV1>(inputSource);

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
:Mindee ID: b9809586-57ae-4f84-a35d-a85b2be1f2a2
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/bank_check v1.0
:Rotation applied: Yes

Prediction
==========
:Check Issue Date: 2022-03-29
:Amount: 15332.90
:Payees: JOHN DOE
         JANE DOE
:Routing Number:
:Account Number: 7789778136
:Check Number: 0003401

Page Predictions
================

Page 0
------
:Check Position: Polygon with 21 points.
:Signature Positions: Polygon with 6 points.
:Check Issue Date: 2022-03-29
:Amount: 15332.90
:Payees: JOHN DOE
         JANE DOE
:Routing Number:
:Account Number: 7789778136
:Check Number: 0003401
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

### AmountField
An amount field `AmountField` extends `BaseField`, but also implements:
* **Value** (`double?`): corresponds to the field value. Can be `null` if no value was extracted.

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
The following fields are extracted for Bank Check V1:

## Account Number
**AccountNumber** : The check payer's account number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.AccountNumber.Value);
```

## Amount
**Amount** : The amount of the check.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Amount.Value);
```

## Check Number
**CheckNumber** : The issuer's check number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.CheckNumber.Value);
```

## Check Position
[📄](#page-level-fields "This field is only present on individual pages.")**CheckPosition** : The position of the check on the document.

```cs
foreach (var CheckPositionElem in result.Document.Inference.Prediction.CheckPosition)
{
    System.Console.WriteLine(CheckPositionElem).Polygon;
}
```

## Check Issue Date
**Date** : The date the check was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Date.Value);
```

## Payees
**Payees** : List of the check's payees (recipients).

```cs
foreach (var PayeesElem in result.Document.Inference.Prediction.Payees)
{
    System.Console.WriteLine(PayeesElem.Value);
}
```

## Routing Number
**RoutingNumber** : The check issuer's routing number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.RoutingNumber.Value);
```

## Signature Positions
[📄](#page-level-fields "This field is only present on individual pages.")**SignaturesPositions** : List of signature positions

```cs
foreach (var page in result.Document.Inference.Pages)
{
    foreach (var SignaturesPositionsElem in page.Prediction.SignaturesPositions)
    {
        System.Console.WriteLine(SignaturesPositionsElem.Polygon);
        System.Console.WriteLine(SignaturesPositionsElem.Quadrangle);
        System.Console.WriteLine(SignaturesPositionsElem.Rectangle);
        System.Console.WriteLine(SignaturesPositionsElem.BoundingBox);
    }
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
