---
title: Cropper OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-cropper-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Cropper API](https://platform.mindee.com/mindee/cropper).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/cropper/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Cropper sample](https://github.com/mindee/client-lib-test-data/blob/main/products/cropper/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Cropper;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<CropperV1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

```

**Output (RST):**
```rst
########
Document
########
:Mindee ID: 149ce775-8302-4798-8649-7eda9fb84a1a
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/cropper v1.0
:Rotation applied: No

Prediction
==========

Page Predictions
================

Page 0
------
:Document Cropper: Polygon with 26 points.
                   Polygon with 25 points.
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

## Page-Level Fields
Some fields are constrained to the page level, and so will not be retrievable at document level.

# Attributes
The following fields are extracted for Cropper V1:

## Document Cropper
[📄](#page-level-fields "This field is only present on individual pages.")**Cropping**: List of documents found in the image.

```cs
foreach (var page in result.Document.Inference.Pages)
{
    foreach (var CroppingElem in page.Prediction.Cropping)
    {
        System.Console.WriteLine(CroppingElem.Polygon);
        System.Console.WriteLine(CroppingElem.Quadrangle);
        System.Console.WriteLine(CroppingElem.Rectangle);
        System.Console.WriteLine(CroppingElem.BoundingBox);
    }
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
