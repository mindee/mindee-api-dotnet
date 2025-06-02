---
title: US Healthcare Card OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-us-healthcare-card-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Healthcare Card API](https://platform.mindee.com/mindee/us_healthcare_cards).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/us_healthcare_cards/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Healthcare Card sample](https://github.com/mindee/client-lib-test-data/blob/main/products/us_healthcare_cards/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.HealthcareCard;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<HealthcareCardV1>(inputSource);

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
:Mindee ID: 5e917fc8-5c13-42b2-967f-954f4eed9959
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/us_healthcare_cards v1.3
:Rotation applied: Yes

Prediction
==========
:Company Name: UnitedHealthcare
:Plan Name: Choice Plus
:Member Name: SUBSCRIBER SMITH
:Member ID: 123456789
:Issuer 80840:
:Dependents: SPOUSE SMITH
             CHILD1 SMITH
             CHILD2 SMITH
             CHILD3 SMITH
:Group Number: 98765
:Payer ID: 87726
:RX BIN: 610279
:RX ID:
:RX GRP: UHEALTH
:RX PCN: 9999
:Copays:
  +--------------+----------------------+
  | Service Fees | Service Name         |
  +==============+======================+
  | 20.00        | office_visit         |
  +--------------+----------------------+
  | 300.00       | emergency_room       |
  +--------------+----------------------+
  | 75.00        | urgent_care          |
  +--------------+----------------------+
  | 30.00        | specialist           |
  +--------------+----------------------+
:Enrollment Date:
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

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Copays Field
Copayments for covered services.

A `HealthcareCardV1Copay` implements the following attributes:

* **ServiceFees** (`double`): The price of the service.
* **ServiceName** (`string`): The name of the service.

#### Possible values include:
 - primary_care
 - emergency_room
 - urgent_care
 - specialist
 - office_visit
 - prescription


# Attributes
The following fields are extracted for Healthcare Card V1:

## Company Name
**CompanyName**: The name of the company that provides the healthcare plan.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CompanyName.Value);
```

## Copays
**Copays**(List<[HealthcareCardV1Copay](#copays-field)>): Copayments for covered services.

```csharp
foreach (var CopaysElem in result.Document.Inference.Prediction.Copays)
{
    System.Console.WriteLine(CopaysElem.Value);
}
```

## Dependents
**Dependents**: The list of dependents covered by the healthcare plan.

```csharp
foreach (var DependentsElem in result.Document.Inference.Prediction.Dependents)
{
    System.Console.WriteLine(DependentsElem.Value);
}
```

## Enrollment Date
**EnrollmentDate**: The date when the member enrolled in the healthcare plan.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.EnrollmentDate.Value);
```

## Group Number
**GroupNumber**: The group number associated with the healthcare plan.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.GroupNumber.Value);
```

## Issuer 80840
**Issuer80840**: The organization that issued the healthcare plan.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Issuer80840.Value);
```

## Member ID
**MemberId**: The unique identifier for the member in the healthcare system.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.MemberId.Value);
```

## Member Name
**MemberName**: The name of the member covered by the healthcare plan.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.MemberName.Value);
```

## Payer ID
**PayerId**: The unique identifier for the payer in the healthcare system.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PayerId.Value);
```

## Plan Name
**PlanName**: The name of the healthcare plan.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PlanName.Value);
```

## RX BIN
**RxBin**: The BIN number for prescription drug coverage.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.RxBin.Value);
```

## RX GRP
**RxGrp**: The group number for prescription drug coverage.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.RxGrp.Value);
```

## RX ID
**RxId**: The ID number for prescription drug coverage.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.RxId.Value);
```

## RX PCN
**RxPcn**: The PCN number for prescription drug coverage.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.RxPcn.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
