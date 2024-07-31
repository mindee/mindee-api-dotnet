---
title: Invoice Splitter API .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-invoice-splitter-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Invoice Splitter API](https://platform.mindee.com/mindee/invoice_splitter).

Using [this sample](https://github.com/mindee/client-lib-test-data/blob/main/products/invoice_splitter/default_sample.pdf), we are going to illustrate how to detect the pages of multiple invoices within the same document.

# Quick-Start

```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.InvoiceSplitter;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// load an input source
var inputSource = new LocalInputSource(filePath);

// call the product asynchronously with auto-polling
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
## Specific Fields
### Invoice Splitter V1 Page Group
List of page group indexes.

An `InvoiceSplitterV1PageGroup` implements the following attributes:

* **PageIndexes** (`int[]`): List of indexes of the pages of a single invoice.
* **Confidence** (`double`): The confidence of the prediction.

# Attributes
The following fields are extracted for Invoice Splitter V1:

## Invoice Page Groups
**PageGroups** (List<[InvoiceSplitterV1PageGroup](#invoice-splitter-v1-page-group)>): List of page indexes that belong to the same invoice in the PDF.

```csharp
foreach (var InvoiceSplitterV1PageGroupElem in result.Document.Inference.Prediction.InvoiceSplitterV1PageGroup)
{
    System.Console.WriteLine(InvoiceSplitterV1PageGroupElem).Polygon;
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
