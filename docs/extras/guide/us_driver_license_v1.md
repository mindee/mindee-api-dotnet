---
title: US Driver License OCR .NET
---
The .NET OCR SDK supports the [Driver License API](https://platform.mindee.com/mindee/us_driver_license).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/us_driver_license/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Driver License sample](https://github.com/mindee/client-lib-test-data/blob/main/products/us_driver_license/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.DriverLicense;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<DriverLicenseV1>(inputSource);

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
:Mindee ID: bf70068d-d3d6-49dc-b93a-b4b7d156fc3d
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/us_driver_license v1.0
:Rotation applied: Yes

Prediction
==========
:State: AZ
:Driver License ID: D12345678
:Expiry Date: 2018-02-01
:Date Of Issue: 2013-01-10
:Last Name: SAMPLE
:First Name: JELANI
:Address: 123 MAIN STREET PHOENIX AZ 85007
:Date Of Birth: 1957-02-01
:Restrictions: NONE
:Endorsements: NONE
:Driver License Class: D
:Sex: M
:Height: 5-08
:Weight: 185
:Hair Color: BRO
:Eye Color: BRO
:Document Discriminator: 1234567890123456

Page Predictions
================

Page 0
------
:Photo: Polygon with 4 points.
:Signature: Polygon with 4 points.
:State: AZ
:Driver License ID: D12345678
:Expiry Date: 2018-02-01
:Date Of Issue: 2013-01-10
:Last Name: SAMPLE
:First Name: JELANI
:Address: 123 MAIN STREET PHOENIX AZ 85007
:Date Of Birth: 1957-02-01
:Restrictions: NONE
:Endorsements: NONE
:Driver License Class: D
:Sex: M
:Height: 5-08
:Weight: 185
:Hair Color: BRO
:Eye Color: BRO
:Document Discriminator: 1234567890123456
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

### DateField
The date field `DateField` extends `StringField`, but also implements:

* **DateObject** (`DateTime?`): an accessible representation of the value as a C# object. Can be `null`.


### PositionField
The position field `PositionField` implements:

* **BoundingBox** (`BoundingBox`): contains exactly 4 relative vertices (points) coordinates of a right rectangle containing the field in the document.
* **Polygon** (`Polygon`): contains the relative vertices coordinates (`Polygon` extends `List<Point>`) of a polygon containing the field in the image.
* **Rectangle** (`Polygon`): a Polygon with four points that may be oriented (even beyond canvas).
* **Quadrangle** (`Quadrangle`): a free polygon made up of four points.

## Page-Level Fields
Some fields are constrained to the page level, and so will not be retrievable at document level.

# Attributes
The following fields are extracted for Driver License V1:

## Address
**Address** : US driver license holders address

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Address.Value);
```

## Date Of Birth
**DateOfBirth** : US driver license holders date of birth

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DateOfBirth.Value);
```

## Document Discriminator
**DdNumber** : Document Discriminator Number of the US Driver License

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DdNumber.Value);
```

## Driver License Class
**DlClass** : US driver license holders class

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DlClass.Value);
```

## Driver License ID
**DriverLicenseId** : ID number of the US Driver License.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.DriverLicenseId.Value);
```

## Endorsements
**Endorsements** : US driver license holders endorsements

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Endorsements.Value);
```

## Expiry Date
**ExpiryDate** : Date on which the documents expires.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.ExpiryDate.Value);
```

## Eye Color
**EyeColor** : US driver license holders eye colour

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.EyeColor.Value);
```

## First Name
**FirstName** : US driver license holders first name(s)

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.FirstName.Value);
```

## Hair Color
**HairColor** : US driver license holders hair colour

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.HairColor.Value);
```

## Height
**Height** : US driver license holders hight

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Height.Value);
```

## Date Of Issue
**IssuedDate** : Date on which the documents was issued.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.IssuedDate.Value);
```

## Last Name
**LastName** : US driver license holders last name

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.LastName.Value);
```

## Photo
[📄](#page-level-fields "This field is only present on individual pages.")**Photo** : Has a photo of the US driver license holder

```cs
foreach(var PhotoElem in result.Document.Photo)
{
    System.Console.WriteLine(PhotoElem).Polygon;
}
```

## Restrictions
**Restrictions** : US driver license holders restrictions

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Restrictions.Value);
```

## Sex
**Sex** : US driver license holders gender

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Sex.Value);
```

## Signature
[📄](#page-level-fields "This field is only present on individual pages.")**Signature** : Has a signature of the US driver license holder

```cs
foreach(var SignatureElem in result.Document.Signature)
{
    System.Console.WriteLine(SignatureElem).Polygon;
}
```

## State
**State** : US State

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.State.Value);
```

## Weight
**Weight** : US driver license holders weight

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Weight.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
