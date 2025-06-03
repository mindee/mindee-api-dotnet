---
title: Invoice OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-invoice-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [Invoice API](https://platform.mindee.com/mindee/invoices).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/invoices/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![Invoice sample](https://github.com/mindee/client-lib-test-data/blob/main/products/invoices/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Invoice;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the API and parse the input
var response = await mindeeClient
    .ParseAsync<InvoiceV4>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

```

You can also call this product asynchronously:

```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Invoice;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<InvoiceV4>(inputSource);

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
:Mindee ID: 744748d5-9051-461c-b70c-bbf81f5ff943
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/invoices v4.11
:Rotation applied: Yes

Prediction
==========
:Locale: en-CA; en; CA; CAD;
:Invoice Number: 14
:Purchase Order Number: AD29094
:Reference Numbers: AD29094
:Purchase Date: 2018-09-25
:Due Date:
:Payment Date:
:Total Net: 2145.00
:Total Amount: 2608.20
:Total Tax: 193.20
:Taxes:
  +---------------+--------+----------+---------------+
  | Base          | Code   | Rate (%) | Amount        |
  +===============+========+==========+===============+
  | 2145.00       |        | 8.00     | 193.20        |
  +---------------+--------+----------+---------------+
:Supplier Payment Details:
:Supplier Name: TURNPIKE DESIGNS
:Supplier Company Registrations:
:Supplier Address: 156 University Ave, Toronto ON, Canada, M5H 2H7
:Supplier Phone Number: 4165551212
:Supplier Website:
:Supplier Email: j_coi@example.com
:Customer Name: JIRO DOI
:Customer Company Registrations:
:Customer Address: 1954 Bloor Street West Toronto, ON, M6P 3K9 Canada
:Customer ID:
:Shipping Address:
:Billing Address: 1954 Bloor Street West Toronto, ON, M6P 3K9 Canada
:Document Type: INVOICE
:Document Type Extended: INVOICE
:Purchase Subcategory:
:Purchase Category: miscellaneous
:Line Items:
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
  | Description                          | Product code | Quantity | Tax Amount | Tax Rate (%) | Total Amount | Unit of measure | Unit Price |
  +======================================+==============+==========+============+==============+==============+=================+============+
  | Platinum web hosting package Down... |              | 1.00     |            |              | 65.00        |                 | 65.00      |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
  | 2 page website design Includes ba... |              | 3.00     |            |              | 2100.00      |                 | 2100.00    |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
  | Mobile designs Includes responsiv... |              | 1.00     |            |              | 250.00       | 1               | 250.00     |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+

Page Predictions
================

Page 0
------
:Locale: en-CA; en; CA; CAD;
:Invoice Number: 14
:Purchase Order Number: AD29094
:Reference Numbers: AD29094
:Purchase Date: 2018-09-25
:Due Date:
:Payment Date:
:Total Net: 2145.00
:Total Amount: 2608.20
:Total Tax: 193.20
:Taxes:
  +---------------+--------+----------+---------------+
  | Base          | Code   | Rate (%) | Amount        |
  +===============+========+==========+===============+
  | 2145.00       |        | 8.00     | 193.20        |
  +---------------+--------+----------+---------------+
:Supplier Payment Details:
:Supplier Name: TURNPIKE DESIGNS
:Supplier Company Registrations:
:Supplier Address: 156 University Ave, Toronto ON, Canada, M5H 2H7
:Supplier Phone Number: 4165551212
:Supplier Website:
:Supplier Email: j_coi@example.com
:Customer Name: JIRO DOI
:Customer Company Registrations:
:Customer Address: 1954 Bloor Street West Toronto, ON, M6P 3K9 Canada
:Customer ID:
:Shipping Address:
:Billing Address: 1954 Bloor Street West Toronto, ON, M6P 3K9 Canada
:Document Type: INVOICE
:Document Type Extended: INVOICE
:Purchase Subcategory:
:Purchase Category: miscellaneous
:Line Items:
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
  | Description                          | Product code | Quantity | Tax Amount | Tax Rate (%) | Total Amount | Unit of measure | Unit Price |
  +======================================+==============+==========+============+==============+==============+=================+============+
  | Platinum web hosting package Down... |              | 1.00     |            |              | 65.00        |                 | 65.00      |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
  | 2 page website design Includes ba... |              | 3.00     |            |              | 2100.00      |                 | 2100.00    |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
  | Mobile designs Includes responsiv... |              | 1.00     |            |              | 250.00       | 1               | 250.00     |
  +--------------------------------------+--------------+----------+------------+--------------+--------------+-----------------+------------+
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


### AddressField
Aside from the basic `BaseField` attributes, the address field `AddressField` also implements the following:

* **StreetNumber** (`string`): String representation of the street number. Can be `null`.
* **StreetName** (`string`): Name of the street. Can be `null`.
* **PoBox** (`string`): String representation of the PO Box number. Can be `null`.
* **AddressComplement** (`string`): Address complement. Can be `null`.
* **City** (`string`): City name. Can be `null`.
* **Postalcode** (`string`): String representation of the postal code. Can be `null`.
* **State** (`string`): State name. Can be `null`.
* **Country** (`string`): Country name. Can be `null`.

Note: The `Value` field of an AddressField should be a concatenation of the rest of the values.

### AmountField
An amount field `AmountField` extends `BaseField`, but also implements:
* **Value** (`double?`): corresponds to the field value. Can be `null` if no value was extracted.


### ClassificationField
The classification field `ClassificationField` extends `BaseField`, but also implements:
* **Value** (`strong`): corresponds to the field value.

> Note: a classification field's `value is always a `string`.


### CompanyRegistration
Aside from the basic `BaseField` attributes, the company registration field `CompanyRegistration` also implements the following:

* **Type** (`string`): the type of company.
* **Value** (`string`): corresponds to the field value.
* **ToTableLine()**: a method that formats the data to fit in a .rst display.

### StringField
The text field `StringField` extends `BaseField`, but also implements:
* **Value** (`string`): corresponds to the field value.
* **RawValue** (`string`): corresponds to the raw value as it appears on the document.

### DateField
The date field `DateField` extends `StringField`, but also implements:

* **DateObject** (`DateTime?`): an accessible representation of the value as a C# object. Can be `null`.

### Locale
The locale field `Locale` extends `BaseField`, but also implements:

* **Language** (`string`): ISO 639-1 language code (e.g.: `en` for English). Can be `null`.
* **Country** (`string`): ISO 3166-1 alpha-2 or ISO 3166-1 alpha-3 code for countries (e.g.: `GRB` or `GB` for "Great Britain"). Can be `null`.
* **Currency** (`string`): ISO 4217 code for currencies (e.g.: `USD` for "US Dollars"). Can be `null`.

### PaymentDetail
The payment details field `PaymentDetail` extends `BaseField`, but it also implements:

* **AccountNumber** (`string`): number of an account, expressed as a string. Can be `null`.
* **Iban** (`string`): International Bank Account Number. Can be `null`.
* **RoutingNumber** (`string`): routing number of an account. Can be `null`.
* **Swift** (`string`): the account holder's bank's SWIFT Business Identifier Code (BIC). Can be `null`.

### Taxes
#### Tax
Aside from the basic `BaseField` attributes, the tax field `Tax` also implements the following:

* **Rate** (`double?`): the tax rate applied to an item expressed as a percentage. Can be `null`.
* **Code** (`string`): tax code (or equivalent, depending on the origin of the document).
* **Base** (`double`): base amount used for the tax. Can be `null`.
* **Value** (`double`): the value of the tax. Can be `null`.

> Note: currently `Tax` is not used on its own, and is accessed through a parent `Taxes` object, a list-like structure.

#### Taxes (List)
The `Taxes` field represents a List of `Tax` objects. As it is the representation of several objects, it has access to a custom `ToString` method that can render a `Tax` object as a table line.

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Line Items Field
List of all the line items present on the invoice.

A `InvoiceV4LineItem` implements the following attributes:

* **Description** (`string`): The item description.
* **ProductCode** (`string`): The product code of the item.
* **Quantity** (`double`): The item quantity
* **TaxAmount** (`double`): The item tax amount.
* **TaxRate** (`double`): The item tax rate in percentage.
* **TotalAmount** (`double`): The item total amount.
* **UnitMeasure** (`string`): The item unit of measure.
* **UnitPrice** (`double`): The item unit price.

# Attributes
The following fields are extracted for Invoice V4:

## Billing Address
**BillingAddress**: The customer billing address.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.BillingAddress.Value);
```

## Purchase Category
**Category**: The purchase category.

#### Possible values include:
 - 'toll'
 - 'food'
 - 'parking'
 - 'transport'
 - 'accommodation'
 - 'telecom'
 - 'miscellaneous'
 - 'software'
 - 'shopping'
 - 'energy'

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Category.Value);
```

## Customer Address
**CustomerAddress**: The address of the customer.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CustomerAddress.Value);
```

## Customer Company Registrations
**CustomerCompanyRegistrations**: List of company registration numbers associated to the customer.

```csharp
foreach (var CustomerCompanyRegistrationsElem in result.Document.Inference.Prediction.CustomerCompanyRegistrations)
{
    System.Console.WriteLine(CustomerCompanyRegistrationsElem.Value);
}
```

## Customer ID
**CustomerId**: The customer account number or identifier from the supplier.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CustomerId.Value);
```

## Customer Name
**CustomerName**: The name of the customer or client.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.CustomerName.Value);
```

## Purchase Date
**Date**: The date the purchase was made.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Date.Value);
```

## Document Type
**DocumentType**: Document type: INVOICE or CREDIT NOTE.

#### Possible values include:
 - 'INVOICE'
 - 'CREDIT NOTE'

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentType.Value);
```

## Document Type Extended
**DocumentTypeExtended**: Document type extended.

#### Possible values include:
 - 'CREDIT NOTE'
 - 'INVOICE'
 - 'OTHER'
 - 'OTHER_FINANCIAL'
 - 'PAYSLIP'
 - 'PURCHASE ORDER'
 - 'QUOTE'
 - 'RECEIPT'
 - 'STATEMENT'

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DocumentTypeExtended.Value);
```

## Due Date
**DueDate**: The date on which the payment is due.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.DueDate.Value);
```

## Invoice Number
**InvoiceNumber**: The invoice number or identifier.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.InvoiceNumber.Value);
```

## Line Items
**LineItems**(List<[InvoiceV4LineItem](#line-items-field)>): List of all the line items present on the invoice.

```csharp
foreach (var LineItemsElem in result.Document.Inference.Prediction.LineItems)
{
    System.Console.WriteLine(LineItemsElem.Value);
}
```

## Locale
**Locale**: The locale of the document.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Locale.Value);
```

## Payment Date
**PaymentDate**: The date on which the payment is due / was full-filled.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PaymentDate.Value);
```

## Purchase Order Number
**PoNumber**: The purchase order number.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.PoNumber.Value);
```

## Reference Numbers
**ReferenceNumbers**: List of all reference numbers on the invoice, including the purchase order number.

```csharp
foreach (var ReferenceNumbersElem in result.Document.Inference.Prediction.ReferenceNumbers)
{
    System.Console.WriteLine(ReferenceNumbersElem.Value);
}
```

## Shipping Address
**ShippingAddress**: Customer's delivery address.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.ShippingAddress.Value);
```

## Purchase Subcategory
**Subcategory**: The purchase subcategory for transport, food and shopping.

#### Possible values include:
 - 'plane'
 - 'taxi'
 - 'train'
 - 'restaurant'
 - 'shopping'
 - 'other'
 - 'groceries'
 - 'cultural'
 - 'electronics'
 - 'office_supplies'
 - 'micromobility'
 - 'car_rental'
 - 'public'
 - 'delivery'
 - null

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.Subcategory.Value);
```

## Supplier Address
**SupplierAddress**: The address of the supplier or merchant.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierAddress.Value);
```

## Supplier Company Registrations
**SupplierCompanyRegistrations**: List of company registration numbers associated to the supplier.

```csharp
foreach (var SupplierCompanyRegistrationsElem in result.Document.Inference.Prediction.SupplierCompanyRegistrations)
{
    System.Console.WriteLine(SupplierCompanyRegistrationsElem.Value);
}
```

## Supplier Email
**SupplierEmail**: The email address of the supplier or merchant.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierEmail.Value);
```

## Supplier Name
**SupplierName**: The name of the supplier or merchant.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierName.Value);
```

## Supplier Payment Details
**SupplierPaymentDetails**: List of payment details associated to the supplier of the invoice.

```csharp
foreach (var SupplierPaymentDetailsElem in result.Document.Inference.Prediction.SupplierPaymentDetails)
{
    System.Console.WriteLine(SupplierPaymentDetailsElem.Value);
}
```

## Supplier Phone Number
**SupplierPhoneNumber**: The phone number of the supplier or merchant.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierPhoneNumber.Value);
```

## Supplier Website
**SupplierWebsite**: The website URL of the supplier or merchant.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SupplierWebsite.Value);
```

## Taxes
**Taxes**: List of taxes. Each item contains the detail of the tax.

```csharp
foreach (var TaxesElem in result.Document.Inference.Prediction.Taxes)
{
    System.Console.WriteLine(TaxesElem.Value);
}
```

## Total Amount
**TotalAmount**: The total amount of the invoice: includes taxes, tips, fees, and other charges.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalAmount.Value);
```

## Total Net
**TotalNet**: The net amount of the invoice: does not include taxes, fees, and discounts.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalNet.Value);
```

## Total Tax
**TotalTax**: The total tax: the sum of all the taxes for this invoice.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.TotalTax.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
