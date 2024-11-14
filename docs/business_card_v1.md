---
title: Business Card OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-business-card-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Business Card API](https://platform.mindee.com/mindee/business_card).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/business_card/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Business Card sample](https://github.com/mindee/client-lib-test-data/blob/main/products/business_card/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.BusinessCard;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<BusinessCardV1>(inputSource);

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
:Mindee ID: 6f9a261f-7609-4687-9af0-46a45156566e
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/business_card v1.0
:Rotation applied: Yes

Prediction
==========
:Firstname: Andrew
:Lastname: Morin
:Job Title: Founder & CEO
:Company: RemoteGlobal
:Email: amorin@remoteglobalconsulting.com
:Phone Number: +14015555555
:Mobile Number: +13015555555
:Fax Number: +14015555556
:Address: 178 Main Avenue, Providence, RI 02111
:Website: www.remoteglobalconsulting.com
:Social Media: https://www.linkedin.com/in/johndoe
               https://twitter.com/johndoe
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

# Attributes
The following fields are extracted for Business Card V1:

## Address
**Address**: The address of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Address.Value);
```

## Company
**Company**: The company the person works for.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Company.Value);
```

## Email
**Email**: The email address of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Email.Value);
```

## Fax Number
**FaxNumber**: The Fax number of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.FaxNumber.Value);
```

## Firstname
**Firstname**: The given name of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Firstname.Value);
```

## Job Title
**JobTitle**: The job title of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.JobTitle.Value);
```

## Lastname
**Lastname**: The lastname of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Lastname.Value);
```

## Mobile Number
**MobileNumber**: The mobile number of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.MobileNumber.Value);
```

## Phone Number
**PhoneNumber**: The phone number of the person.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PhoneNumber.Value);
```

## Social Media
**SocialMedia**: The social media profiles of the person or company.

```csharp
foreach (var SocialMediaElem in result.Document.Inference.Prediction.SocialMedia)
{
    System.Console.WriteLine(SocialMediaElem.Value);
}
```

## Website
**Website**: The website of the person or company.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Website.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
