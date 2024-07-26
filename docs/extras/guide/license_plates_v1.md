---
title: EU License Plate OCR .NET
---
The .NET OCR SDK supports the [License Plate API](https://platform.mindee.com/mindee/license_plates).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/license_plates/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![License Plate sample](https://github.com/mindee/client-lib-test-data/blob/main/products/license_plates/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Eu.LicensePlate;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<LicensePlateV1>(inputSource);

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
:Mindee ID: f0f48232-2c80-4473-9c6f-88a09111b84d
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/license_plates v1.0
:Rotation applied: No

Prediction
==========
:License Plates: BY-323-YB

Page Predictions
================

Page 0
------
:License Plates: BY-323-YB
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
* **PageId** (`int?`): the ID of the page, is `null` when at document-level.

> **Note:** A `Point` simply refers to a List of `double`.


Aside from the previous attributes, all basic fields have access to a custom `ToString` method that can be used to print their value as a string.

### StringField
The text field `StringField` extends `BaseField`, but also implements:
* **Value** (`string`): corresponds to the field value.
* **RawValue** (`string`): corresponds to the raw value as it appears on the document.

# Attributes
The following fields are extracted for License Plate V1:

## License Plates
**LicensePlates** : List of all license plates found in the image.

```cs
foreach (LicensePlatesElem in result.Document.Inference.Prediction.LicensePlates)
{
    System.Console.WriteLine(LicensePlatesElem.Value);
}
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
