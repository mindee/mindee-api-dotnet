---
title: Invoice Splitter OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-invoice-splitter-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Invoice Splitter API](https://platform.mindee.com/mindee/invoice_splitter).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/invoice_splitter/default_sample.pdf), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Invoice Splitter sample](https://github.com/mindee/client-lib-test-data/blob/main/products/invoice_splitter/default_sample.pdf?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.InvoiceSplitter;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<InvoiceSplitterV1>(inputSource);

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
:Mindee ID: 15ad7a19-7b75-43d0-b0c6-9a641a12b49b
:Filename: default_sample.pdf

Inference
#########
:Product: mindee/invoice_splitter v1.1
:Rotation applied: No

Prediction
==========
:Invoice Page Groups:
  :Page indexes: 0
  :Page indexes: 1

Page Predictions
================

Page 0
------
:Invoice Page Groups:

Page 1
------
:Invoice Page Groups:
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

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Invoice Page Groups Field
List of page groups. Each group represents a single invoice within a multi-invoice document.

A `InvoiceSplitterV1InvoicePageGroup` implements the following attributes:

* **PageIndexes** (`List<int>`): List of page indexes that belong to the same invoice (group).

# Attributes
The following fields are extracted for Invoice Splitter V1:

## Invoice Page Groups
**InvoicePageGroups**(List<[InvoiceSplitterV1InvoicePageGroup](#invoice-page-groups-field)>): List of page groups. Each group represents a single invoice within a multi-invoice document.

```csharp
foreach (var InvoicePageGroupsElem in result.Document.Inference.Prediction.InvoicePageGroups)
{
    System.Console.WriteLine(InvoicePageGroupsElem.Value);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
