---
title: Driver License OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-driver-license-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Driver License API](https://platform.mindee.com/mindee/driver_license).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/driver_license/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Driver License sample](https://github.com/mindee/client-lib-test-data/blob/main/products/driver_license/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.DriverLicense;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<DriverLicenseV1>(inputSource);

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
:Mindee ID: fbdeae38-ada3-43ac-aa58-e01a3d47e474
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/driver_license v1.0
:Rotation applied: Yes

Prediction
==========
:Country Code: USA
:State: AZ
:ID: D12345678
:Category: D
:Last Name: Sample
:First Name: Jelani
:Date of Birth: 1957-02-01
:Place of Birth:
:Expiry Date: 2018-02-01
:Issued Date: 2013-01-10
:Issuing Authority:
:MRZ:
:DD Number: DD1234567890123456
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
The following fields are extracted for Driver License V1:

## Category
**Category**: The category or class of the driver license.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Category.Value);
```

## Country Code
**CountryCode**: The alpha-3 ISO 3166 code of the country where the driver license was issued.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CountryCode.Value);
```

## Date of Birth
**DateOfBirth**: The date of birth of the driver license holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DateOfBirth.Value);
```

## DD Number
**DdNumber**: The DD number of the driver license.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DdNumber.Value);
```

## Expiry Date
**ExpiryDate**: The expiry date of the driver license.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## First Name
**FirstName**: The first name of the driver license holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.FirstName.Value);
```

## ID
**Id**: The unique identifier of the driver license.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Id.Value);
```

## Issued Date
**IssuedDate**: The date when the driver license was issued.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IssuedDate.Value);
```

## Issuing Authority
**IssuingAuthority**: The authority that issued the driver license.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IssuingAuthority.Value);
```

## Last Name
**LastName**: The last name of the driver license holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.LastName.Value);
```

## MRZ
**Mrz**: The Machine Readable Zone (MRZ) of the driver license.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz.Value);
```

## Place of Birth
**PlaceOfBirth**: The place of birth of the driver license holder.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PlaceOfBirth.Value);
```

## State
**State**: Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.State.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
