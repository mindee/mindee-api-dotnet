---
title: Multi Receipts Detector OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-multi-receipts-detector-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Multi Receipts Detector API](https://platform.mindee.com/mindee/multi_receipts_detector).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/multi_receipts_detector/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Multi Receipts Detector sample](https://github.com/mindee/client-lib-test-data/blob/main/products/multi_receipts_detector/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.MultiReceiptsDetector;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<MultiReceiptsDetectorV1>(inputSource);

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
:Mindee ID: d7c5b25f-e0d3-4491-af54-6183afa1aaab
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/multi_receipts_detector v1.0
:Rotation applied: Yes

Prediction
==========
:List of Receipts: Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.

Page Predictions
================

Page 0
------
:List of Receipts: Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
                   Polygon with 4 points.
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


### PositionField
The position field `PositionField` implements:

* **BoundingBox** (`BoundingBox`): contains exactly 4 relative vertices (points) coordinates of a right rectangle containing the field in the document.
* **Polygon** (`Polygon`): contains the relative vertices coordinates (`Polygon` extends `List<Point>`) of a polygon containing the field in the image.
* **Rectangle** (`Polygon`): a Polygon with four points that may be oriented (even beyond canvas).
* **Quadrangle** (`Quadrangle`): a free polygon made up of four points.

# Attributes
The following fields are extracted for Multi Receipts Detector V1:

## List of Receipts
**Receipts**: Positions of the receipts on the document.

```csharp
foreach (var ReceiptsElem in result.Document.Inference.Prediction.Receipts)
{
    System.Console.WriteLine(ReceiptsElem.Polygon);
    System.Console.WriteLine(ReceiptsElem.Quadrangle);
    System.Console.WriteLine(ReceiptsElem.Rectangle);
    System.Console.WriteLine(ReceiptsElem.BoundingBox);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
