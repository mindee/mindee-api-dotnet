---
title: FR Carte Grise OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-fr-carte-grise-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Carte Grise API](https://platform.mindee.com/mindee/carte_grise).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/carte_grise/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Carte Grise sample](https://github.com/mindee/client-lib-test-data/blob/main/products/carte_grise/default_sample.jpg?raw=true)

# Quick-Start
```cs
using Mindee;
using Mindee.Input;
using Mindee.Product.Fr.CarteGrise;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<CarteGriseV1>(inputSource);

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
:Mindee ID: 4443182b-57c1-4426-a288-01b94f226e84
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/carte_grise v1.1
:Rotation applied: Yes

Prediction
==========
:a: AB-123-CD
:b: 1998-01-05
:c1: DUPONT YVES
:c3: 27 RUE DES ROITELETS 59169 FERIN LES BAINS FRANCE
:c41: 2 DELAROCHE
:c4a: EST LE PROPRIETAIRE DU VEHICULE
:d1:
:d3: MODELE
:e: VFS1V2009AS1V2009
:f1: 1915
:f2: 1915
:f3: 1915
:g: 3030
:g1: 1307
:i: 2009-12-04
:j: N1
:j1: VP
:j2: AA
:j3: CI
:p1: 1900
:p2: 90
:p3: GO
:p6: 6
:q: 006
:s1: 5
:s2:
:u1: 77
:u2: 3000
:v7: 155
:x1: 2011-07-06
:y1: 17835
:y2:
:y3: 0
:y4: 4
:y5: 2.5
:y6: 178.35
:Formula Number: 2009AS05284
:Owner's First Name: YVES
:Owner's Surname: DUPONT
:MRZ Line 1:
:MRZ Line 2: CI<<MARQUES<<<<<<<MODELE<<<<<<<2009AS0528402

Page Predictions
================

Page 0
------
:a: AB-123-CD
:b: 1998-01-05
:c1: DUPONT YVES
:c3: 27 RUE DES ROITELETS 59169 FERIN LES BAINS FRANCE
:c41: 2 DELAROCHE
:c4a: EST LE PROPRIETAIRE DU VEHICULE
:d1:
:d3: MODELE
:e: VFS1V2009AS1V2009
:f1: 1915
:f2: 1915
:f3: 1915
:g: 3030
:g1: 1307
:i: 2009-12-04
:j: N1
:j1: VP
:j2: AA
:j3: CI
:p1: 1900
:p2: 90
:p3: GO
:p6: 6
:q: 006
:s1: 5
:s2:
:u1: 77
:u2: 3000
:v7: 155
:x1: 2011-07-06
:y1: 17835
:y2:
:y3: 0
:y4: 4
:y5: 2.5
:y6: 178.35
:Formula Number: 2009AS05284
:Owner's First Name: YVES
:Owner's Surname: DUPONT
:MRZ Line 1:
:MRZ Line 2: CI<<MARQUES<<<<<<<MODELE<<<<<<<2009AS0528402
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
The following fields are extracted for Carte Grise V1:

## a
**A**: The vehicle's license plate number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.A.Value);
```

## b
**B**: The vehicle's first release date.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.B.Value);
```

## c1
**C1**: The vehicle owner's full name including maiden name.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.C1.Value);
```

## c3
**C3**: The vehicle owner's address.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.C3.Value);
```

## c41
**C41**: Number of owners of the license certificate.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.C41.Value);
```

## c4a
**C4A**: Mentions about the ownership of the vehicle.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.C4A.Value);
```

## d1
**D1**: The vehicle's brand.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.D1.Value);
```

## d3
**D3**: The vehicle's commercial name.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.D3.Value);
```

## e
**E**: The Vehicle Identification Number (VIN).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.E.Value);
```

## f1
**F1**: The vehicle's maximum admissible weight.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.F1.Value);
```

## f2
**F2**: The vehicle's maximum admissible weight within the license's state.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.F2.Value);
```

## f3
**F3**: The vehicle's maximum authorized weight with coupling.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.F3.Value);
```

## Formula Number
**FormulaNumber**: The document's formula number.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.FormulaNumber.Value);
```

## g
**G**: The vehicle's weight with coupling if tractor different than category M1.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.G.Value);
```

## g1
**G1**: The vehicle's national empty weight.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.G1.Value);
```

## i
**I**: The car registration date of the given certificate.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.I.Value);
```

## j
**J**: The vehicle's category.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.J.Value);
```

## j1
**J1**: The vehicle's national type.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.J1.Value);
```

## j2
**J2**: The vehicle's body type (CE).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.J2.Value);
```

## j3
**J3**: The vehicle's body type (National designation).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.J3.Value);
```

## MRZ Line 1
**Mrz1**: Machine Readable Zone, first line.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz1.Value);
```

## MRZ Line 2
**Mrz2**: Machine Readable Zone, second line.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Mrz2.Value);
```

## Owner's First Name
**OwnerFirstName**: The vehicle's owner first name.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.OwnerFirstName.Value);
```

## Owner's Surname
**OwnerSurname**: The vehicle's owner surname.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.OwnerSurname.Value);
```

## p1
**P1**: The vehicle engine's displacement (cm3).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.P1.Value);
```

## p2
**P2**: The vehicle's maximum net power (kW).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.P2.Value);
```

## p3
**P3**: The vehicle's fuel type or energy source.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.P3.Value);
```

## p6
**P6**: The vehicle's administrative power (fiscal horsepower).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.P6.Value);
```

## q
**Q**: The vehicle's power to weight ratio.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Q.Value);
```

## s1
**S1**: The vehicle's number of seats.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.S1.Value);
```

## s2
**S2**: The vehicle's number of standing rooms (person).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.S2.Value);
```

## u1
**U1**: The vehicle's sound level (dB).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.U1.Value);
```

## u2
**U2**: The vehicle engine's rotation speed (RPM).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.U2.Value);
```

## v7
**V7**: The vehicle's CO2 emission (g/km).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.V7.Value);
```

## x1
**X1**: Next technical control date.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.X1.Value);
```

## y1
**Y1**: Amount of the regional proportional tax of the registration (in euros).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Y1.Value);
```

## y2
**Y2**: Amount of the additional parafiscal tax of the registration (in euros).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Y2.Value);
```

## y3
**Y3**: Amount of the additional CO2 tax of the registration (in euros).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Y3.Value);
```

## y4
**Y4**: Amount of the fee for managing the registration (in euros).

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Y4.Value);
```

## y5
**Y5**: Amount of the fee for delivery of the registration certificate in euros.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Y5.Value);
```

## y6
**Y6**: Total amount of registration fee to be paid in euros.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.Y6.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
