---
title: FR Bank Account Details OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-fr-bank-account-details-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Bank Account Details API](https://platform.mindee.com/mindee/bank_account_details).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/bank_account_details/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Bank Account Details sample](https://github.com/mindee/client-lib-test-data/blob/main/products/bank_account_details/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Fr.BankAccountDetails;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<BankAccountDetailsV2>(inputSource);

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
:Mindee ID: bc8f7265-8dab-49fe-810c-d50049605578
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/bank_account_details v2.0
:Rotation applied: Yes

Prediction
==========
:Account Holder's Names: MME HEGALALDIA L ENVOL
:Basic Bank Account Number:
  :Bank Code: 13335
  :Branch Code: 00040
  :Key: 06
  :Account Number: 08932891361
:IBAN: FR7613335000400893289136106
:SWIFT Code: CEPAFRPP333

Page Predictions
================

Page 0
------
:Account Holder's Names: MME HEGALALDIA L ENVOL
:Basic Bank Account Number:
  :Bank Code: 13335
  :Branch Code: 00040
  :Key: 06
  :Account Number: 08932891361
:IBAN: FR7613335000400893289136106
:SWIFT Code: CEPAFRPP333
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

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Basic Bank Account Number Field
Full extraction of BBAN, including: branch code, bank code, account and key.

A `BankAccountDetailsV2Bban` implements the following attributes:

* **BbanBankCode** (`string`): The BBAN bank code outputted as a string.
* **BbanBranchCode** (`string`): The BBAN branch code outputted as a string.
* **BbanKey** (`string`): The BBAN key outputted as a string.
* **BbanNumber** (`string`): The BBAN Account number outputted as a string.

# Attributes
The following fields are extracted for Bank Account Details V2:

## Account Holder's Names
**AccountHoldersNames**: Full extraction of the account holders names.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.AccountHoldersNames.Value);
```

## Basic Bank Account Number
**Bban**([BankAccountDetailsV2Bban](#basic-bank-account-number-field)): Full extraction of BBAN, including: branch code, bank code, account and key.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Bban.Value);
```

## IBAN
**Iban**: Full extraction of the IBAN number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Iban.Value);
```

## SWIFT Code
**SwiftCode**: Full extraction of the SWIFT code.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.SwiftCode.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
