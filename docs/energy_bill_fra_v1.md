---
title: FR Energy Bill OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-fr-energy-bill-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Energy Bill API](https://platform.mindee.com/mindee/energy_bill_fra).

The [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/energy_bill_fra/default_sample.jpg) can be used for testing purposes.
![Energy Bill sample](https://github.com/mindee/client-lib-test-data/blob/main/products/energy_bill_fra/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Fr.EnergyBill;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<EnergyBillV1>(inputSource);

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

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Energy Consumer Field
The entity that consumes the energy.

A `EnergyBillV1EnergyConsumer` implements the following attributes:

* **Address** (`string`): The address of the energy consumer.
* **Name** (`string`): The name of the energy consumer.
Fields which are specific to this product; they are not used in any other product.

### Energy Supplier Field
The company that supplies the energy.

A `EnergyBillV1EnergySupplier` implements the following attributes:

* **Address** (`string`): The address of the energy supplier.
* **Name** (`string`): The name of the energy supplier.
Fields which are specific to this product; they are not used in any other product.

### Energy Usage Field
Details of energy consumption.

A `EnergyBillV1EnergyUsage` implements the following attributes:

* **Description** (`string`): Description or details of the energy usage.
* **EndDate** (`string`): The end date of the energy usage.
* **StartDate** (`string`): The start date of the energy usage.
* **TaxRate** (`double`): The rate of tax applied to the total cost.
* **Total** (`double`): The total cost of energy consumed.
* **UnitPrice** (`double`): The price per unit of energy consumed.
Fields which are specific to this product; they are not used in any other product.

### Meter Details Field
Information about the energy meter.

A `EnergyBillV1MeterDetail` implements the following attributes:

* **MeterNumber** (`string`): The unique identifier of the energy meter.
* **MeterType** (`string`): The type of energy meter.

#### Possible values include:
 - electricity
 - gas
 - water
 - None

* **Unit** (`string`): The unit of measurement for energy consumption, which can be kW, m³, or L.
Fields which are specific to this product; they are not used in any other product.

### Subscription Field
The subscription details fee for the energy service.

A `EnergyBillV1Subscription` implements the following attributes:

* **Description** (`string`): Description or details of the subscription.
* **EndDate** (`string`): The end date of the subscription.
* **StartDate** (`string`): The start date of the subscription.
* **TaxRate** (`double`): The rate of tax applied to the total cost.
* **Total** (`double`): The total cost of subscription.
* **UnitPrice** (`double`): The price per unit of subscription.
Fields which are specific to this product; they are not used in any other product.

### Taxes and Contributions Field
Details of Taxes and Contributions.

A `EnergyBillV1TaxesAndContribution` implements the following attributes:

* **Description** (`string`): Description or details of the Taxes and Contributions.
* **EndDate** (`string`): The end date of the Taxes and Contributions.
* **StartDate** (`string`): The start date of the Taxes and Contributions.
* **TaxRate** (`double`): The rate of tax applied to the total cost.
* **Total** (`double`): The total cost of Taxes and Contributions.
* **UnitPrice** (`double`): The price per unit of Taxes and Contributions.

# Attributes
The following fields are extracted for Energy Bill V1:

## Contract ID
**ContractId**: The unique identifier associated with a specific contract.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ContractId.Value);
```

## Delivery Point
**DeliveryPoint**: The unique identifier assigned to each electricity or gas consumption point. It specifies the exact location where the energy is delivered.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DeliveryPoint.Value);
```

## Due Date
**DueDate**: The date by which the payment for the energy invoice is due.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DueDate.Value);
```

## Energy Consumer
**EnergyConsumer**([EnergyBillV1EnergyConsumer](#energy-consumer-field)): The entity that consumes the energy.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.EnergyConsumer.Value);
```

## Energy Supplier
**EnergySupplier**([EnergyBillV1EnergySupplier](#energy-supplier-field)): The company that supplies the energy.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.EnergySupplier.Value);
```

## Energy Usage
**EnergyUsage**(List<[EnergyBillV1EnergyUsage](#energy-usage-field)>): Details of energy consumption.

```csharp
foreach (var EnergyUsageElem in result.Document.Inference.Prediction.EnergyUsage)
{
    System.Console.WriteLine(EnergyUsageElem.Value);
}
```

## Invoice Date
**InvoiceDate**: The date when the energy invoice was issued.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.InvoiceDate.Value);
```

## Invoice Number
**InvoiceNumber**: The unique identifier of the energy invoice.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.InvoiceNumber.Value);
```

## Meter Details
**MeterDetails**([EnergyBillV1MeterDetail](#meter-details-field)): Information about the energy meter.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.MeterDetails.Value);
```

## Subscription
**Subscription**(List<[EnergyBillV1Subscription](#subscription-field)>): The subscription details fee for the energy service.

```csharp
foreach (var SubscriptionElem in result.Document.Inference.Prediction.Subscription)
{
    System.Console.WriteLine(SubscriptionElem.Value);
}
```

## Taxes and Contributions
**TaxesAndContributions**(List<[EnergyBillV1TaxesAndContribution](#taxes-and-contributions-field)>): Details of Taxes and Contributions.

```csharp
foreach (var TaxesAndContributionsElem in result.Document.Inference.Prediction.TaxesAndContributions)
{
    System.Console.WriteLine(TaxesAndContributionsElem.Value);
}
```

## Total Amount
**TotalAmount**: The total amount to be paid for the energy invoice.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalAmount.Value);
```

## Total Before Taxes
**TotalBeforeTaxes**: The total amount to be paid for the energy invoice before taxes.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalBeforeTaxes.Value);
```

## Total Taxes
**TotalTaxes**: Total of taxes applied to the invoice.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalTaxes.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)