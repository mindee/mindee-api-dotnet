---
title: Bill of Lading OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-bill-of-lading-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Bill of Lading API](https://platform.mindee.com/mindee/bill_of_lading).

The [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/bill_of_lading/default_sample.jpg) can be used for testing purposes.
![Bill of Lading sample](https://github.com/mindee/client-lib-test-data/blob/main/products/bill_of_lading/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.BillOfLading;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<BillOfLadingV1>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

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

### Carrier Field
The shipping company responsible for transporting the goods.

A `BillOfLadingV1Carrier` implements the following attributes:

* **Name** (`string`): The name of the carrier.
* **ProfessionalNumber** (`string`): The professional number of the carrier.
* **Scac** (`string`): The Standard Carrier Alpha Code (SCAC) of the carrier.
Fields which are specific to this product; they are not used in any other product.

### Consignee Field
The party to whom the goods are being shipped.

A `BillOfLadingV1Consignee` implements the following attributes:

* **Address** (`string`): The address of the consignee.
* **Email** (`string`): The  email of the shipper.
* **Name** (`string`): The name of the consignee.
* **Phone** (`string`): The phone number of the consignee.
Fields which are specific to this product; they are not used in any other product.

### Items Field
The goods being shipped.

A `BillOfLadingV1CarrierItem` implements the following attributes:

* **Description** (`string`): A description of the item.
* **GrossWeight** (`double`): The gross weight of the item.
* **Measurement** (`double`): The measurement of the item.
* **MeasurementUnit** (`string`): The unit of measurement for the measurement.
* **Quantity** (`double`): The quantity of the item being shipped.
* **WeightUnit** (`string`): The unit of measurement for weights.
Fields which are specific to this product; they are not used in any other product.

### Notify Party Field
The party to be notified of the arrival of the goods.

A `BillOfLadingV1NotifyParty` implements the following attributes:

* **Address** (`string`): The address of the notify party.
* **Email** (`string`): The  email of the shipper.
* **Name** (`string`): The name of the notify party.
* **Phone** (`string`): The phone number of the notify party.
Fields which are specific to this product; they are not used in any other product.

### Shipper Field
The party responsible for shipping the goods.

A `BillOfLadingV1Shipper` implements the following attributes:

* **Address** (`string`): The address of the shipper.
* **Email** (`string`): The  email of the shipper.
* **Name** (`string`): The name of the shipper.
* **Phone** (`string`): The phone number of the shipper.

# Attributes
The following fields are extracted for Bill of Lading V1:

## Bill of Lading Number
**BillOfLadingNumber**: A unique identifier assigned to a Bill of Lading document.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BillOfLadingNumber.Value);
```

## Carrier
**Carrier**([BillOfLadingV1Carrier](#carrier-field)): The shipping company responsible for transporting the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Carrier.Value);
```

## Items
**CarrierItems**(List<[BillOfLadingV1CarrierItem](#items-field)>): The goods being shipped.

```csharp
foreach (var CarrierItemsElem in result.Document.Inference.Prediction.CarrierItems)
{
    System.Console.WriteLine(CarrierItemsElem.Value);
}
```

## Consignee
**Consignee**([BillOfLadingV1Consignee](#consignee-field)): The party to whom the goods are being shipped.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Consignee.Value);
```

## Date of issue
**DateOfIssue**: The date when the bill of lading is issued.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DateOfIssue.Value);
```

## Departure Date
**DepartureDate**: The date when the vessel departs from the port of loading.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DepartureDate.Value);
```

## Notify Party
**NotifyParty**([BillOfLadingV1NotifyParty](#notify-party-field)): The party to be notified of the arrival of the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.NotifyParty.Value);
```

## Place of Delivery
**PlaceOfDelivery**: The place where the goods are to be delivered.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PlaceOfDelivery.Value);
```

## Port of Discharge
**PortOfDischarge**: The port where the goods are unloaded from the vessel.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PortOfDischarge.Value);
```

## Port of Loading
**PortOfLoading**: The port where the goods are loaded onto the vessel.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PortOfLoading.Value);
```

## Shipper
**Shipper**([BillOfLadingV1Shipper](#shipper-field)): The party responsible for shipping the goods.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Shipper.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
