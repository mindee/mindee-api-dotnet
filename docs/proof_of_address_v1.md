---
title: Proof of Address OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-proof-of-address-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Proof of Address API](https://platform.mindee.com/mindee/proof_of_address).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/proof_of_address/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Proof of Address sample](https://github.com/mindee/client-lib-test-data/blob/main/products/proof_of_address/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.ProofOfAddress;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<ProofOfAddressV1>(inputSource);

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
:Mindee ID: 5d2361e9-405e-4fc1-8531-f92a3aef0c38
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/proof_of_address v1.1
:Rotation applied: Yes

Prediction
==========
:Locale: en; en; USD;
:Issuer Name: PPL ELECTRIC UTILITIES
:Issuer Company Registrations:
:Issuer Address: 2 NORTH 9TH STREET CPC-GENN1 ALLENTOWN.PA 18101-1175
:Recipient Name:
:Recipient Company Registrations:
:Recipient Address: 123 MAIN ST ANYTOWN,PA 18062
:Dates: 2011-07-27
        2011-07-06
        2011-08-03
        2011-07-27
        2011-06-01
        2011-07-01
        2010-07-01
        2010-08-01
        2011-07-01
        2009-08-01
        2010-07-01
        2011-07-27
:Date of Issue: 2011-07-27

Page Predictions
================

Page 0
------
:Locale: en; en; USD;
:Issuer Name: PPL ELECTRIC UTILITIES
:Issuer Company Registrations:
:Issuer Address: 2 NORTH 9TH STREET CPC-GENN1 ALLENTOWN.PA 18101-1175
:Recipient Name:
:Recipient Company Registrations:
:Recipient Address: 123 MAIN ST ANYTOWN,PA 18062
:Dates: 2011-07-27
        2011-07-06
        2011-08-03
        2011-07-27
        2011-06-01
        2011-07-01
        2010-07-01
        2010-08-01
        2011-07-01
        2009-08-01
        2010-07-01
        2011-07-27
:Date of Issue: 2011-07-27
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


### CompanyRegistration
Aside from the basic `BaseField` attributes, the company registration field `CompanyRegistration` also implements the following:

* **Type** (`string`): the type of company.
* **Value** (`string`): corresponds to the field value.
* **ToTableLine()**: a method that formats the data to fit in a .rst display.

### StringField
The text field `StringField` extends `BaseField`, but also implements:
* **Value** (`string`): corresponds to the field value.
* **RawValue** (`string`): corresponds to the raw value as it appears on the document.

### DateField
The date field `DateField` extends `StringField`, but also implements:

* **DateObject** (`DateTime?`): an accessible representation of the value as a C# object. Can be `null`.

### Locale
The locale field `Locale` extends `BaseField`, but also implements:

* **Language** (`string`): ISO 639-1 language code (e.g.: `en` for English). Can be `null`.
* **Country** (`string`): ISO 3166-1 alpha-2 or ISO 3166-1 alpha-3 code for countries (e.g.: `GRB` or `GB` for "Great Britain"). Can be `null`.
* **Currency** (`string`): ISO 4217 code for currencies (e.g.: `USD` for "US Dollars"). Can be `null`.

# Attributes
The following fields are extracted for Proof of Address V1:

## Date of Issue
**Date**: The date the document was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Date.Value);
```

## Dates
**Dates**: List of dates found on the document.

```cs
foreach (var DatesElem in result.Document.Inference.Prediction.Dates)
{
    System.Console.WriteLine(DatesElem.Value);
}
```

## Issuer Address
**IssuerAddress**: The address of the document's issuer.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssuerAddress.Value);
```

## Issuer Company Registrations
**IssuerCompanyRegistration**: List of company registrations found for the issuer.

```cs
foreach (var IssuerCompanyRegistrationElem in result.Document.Inference.Prediction.IssuerCompanyRegistration)
{
    System.Console.WriteLine(IssuerCompanyRegistrationElem.Value);
}
```

## Issuer Name
**IssuerName**: The name of the person or company issuing the document.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssuerName.Value);
```

## Locale
**Locale**: The locale detected on the document.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Locale.Value);
```

## Recipient Address
**RecipientAddress**: The address of the recipient.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.RecipientAddress.Value);
```

## Recipient Company Registrations
**RecipientCompanyRegistration**: List of company registrations found for the recipient.

```cs
foreach (var RecipientCompanyRegistrationElem in result.Document.Inference.Prediction.RecipientCompanyRegistration)
{
    System.Console.WriteLine(RecipientCompanyRegistrationElem.Value);
}
```

## Recipient Name
**RecipientName**: The name of the person or company receiving the document.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.RecipientName.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
