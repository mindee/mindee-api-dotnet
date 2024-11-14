---
title: Delivery note OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-delivery-note-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Delivery note API](https://platform.mindee.com/mindee/delivery_notes).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/delivery_notes/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Delivery note sample](https://github.com/mindee/client-lib-test-data/blob/main/products/delivery_notes/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.DeliveryNote;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<DeliveryNoteV1>(inputSource);

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
:Mindee ID: d5ead821-edec-4d31-a69a-cf3998d9a506
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/delivery_notes v1.0
:Rotation applied: Yes

Prediction
==========
:Delivery Date: 2019-10-02
:Delivery Number: INT-001
:Supplier Name: John Smith
:Supplier Address: 4490 Oak Drive, Albany, NY 12210
:Customer Name: Jessie M Horne
:Customer Address: 4312 Wood Road, New York, NY 10031
:Total Amount: 204.75
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

# Attributes
The following fields are extracted for Delivery note V1:

## Customer Address
**CustomerAddress**: The address of the customer receiving the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CustomerAddress.Value);
```

## Customer Name
**CustomerName**: The name of the customer receiving the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CustomerName.Value);
```

## Delivery Date
**DeliveryDate**: The date on which the delivery is scheduled to arrive.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DeliveryDate.Value);
```

## Delivery Number
**DeliveryNumber**: A unique identifier for the delivery note.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DeliveryNumber.Value);
```

## Supplier Address
**SupplierAddress**: The address of the supplier providing the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierAddress.Value);
```

## Supplier Name
**SupplierName**: The name of the supplier providing the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierName.Value);
```

## Total Amount
**TotalAmount**: The total monetary value of the goods being delivered.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalAmount.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
