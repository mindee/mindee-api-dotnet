---
title: US US Mail OCR .NET
category: 622b805aaec68102ea7fcbc2
slug: dotnet-us-us-mail-ocr
parentDoc: 6357abb22e33070016cbda4b
---
The .NET OCR SDK supports the [US Mail API](https://platform.mindee.com/mindee/us_mail).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/us_mail/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![US Mail sample](https://github.com/mindee/client-lib-test-data/blob/main/products/us_mail/default_sample.jpg?raw=true)

# Quick-Start
```csharp
using Mindee;
using Mindee.Input;
using Mindee.Product.Us.UsMail;

string apiKey = "my-api-key";
string filePath = "/path/to/the/file.ext";

// Construct a new client
MindeeClient mindeeClient = new MindeeClient(apiKey);

// Load an input source as a path string
// Other input types can be used, as mentioned in the docs
var inputSource = new LocalInputSource(filePath);

// Call the product asynchronously with auto-polling
var response = await mindeeClient
    .EnqueueAndParseAsync<UsMailV3>(inputSource);

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
:Mindee ID: f9c36f59-977d-4ddc-9f2d-31c294c456ac
:Filename: default_sample.jpg

Inference
#########
:Product: mindee/us_mail v3.0
:Rotation applied: Yes

Prediction
==========
:Sender Name: company zed
:Sender Address:
  :City: Dallas
  :Complete Address: 54321 Elm Street, Dallas, Texas 54321
  :Postal Code: 54321
  :State: TX
  :Street: 54321 Elm Street
:Recipient Names: Jane Doe
:Recipient Addresses:
  +-----------------+-------------------------------------+-------------------+-------------+------------------------+-------+---------------------------+-----------------+
  | City            | Complete Address                    | Is Address Change | Postal Code | Private Mailbox Number | State | Street                    | Unit            |
  +=================+=====================================+===================+=============+========================+=======+===========================+=================+
  | Detroit         | 1234 Market Street PMB 4321, Det... | False             | 12345       | 4321                   | MI    | 1234 Market Street        |                 |
  +-----------------+-------------------------------------+-------------------+-------------+------------------------+-------+---------------------------+-----------------+
:Return to Sender: False
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

### BooleanField
The boolean field `BooleanField` extends BaseField, but also implements:
* **Value** (`bool?`): corresponds to the value of the field.

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Recipient Addresses Field
The addresses of the recipients.

A `UsMailV3RecipientAddress` implements the following attributes:

* **City** (`string`): The city of the recipient's address.
* **Complete** (`string`): The complete address of the recipient.
* **IsAddressChange** (`bool?`): Indicates if the recipient's address is a change of address.
* **PostalCode** (`string`): The postal code of the recipient's address.
* **PrivateMailboxNumber** (`string`): The private mailbox number of the recipient's address.
* **State** (`string`): Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
* **Street** (`string`): The street of the recipient's address.
* **Unit** (`string`): The unit number of the recipient's address.
Fields which are specific to this product; they are not used in any other product.

### Sender Address Field
The address of the sender.

A `UsMailV3SenderAddress` implements the following attributes:

* **City** (`string`): The city of the sender's address.
* **Complete** (`string`): The complete address of the sender.
* **PostalCode** (`string`): The postal code of the sender's address.
* **State** (`string`): Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
* **Street** (`string`): The street of the sender's address.

# Attributes
The following fields are extracted for US Mail V3:

## Return to Sender
**IsReturnToSender**: Whether the mailing is marked as return to sender.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.IsReturnToSender.Value);
```

## Recipient Addresses
**RecipientAddresses**(List<[UsMailV3RecipientAddress](#recipient-addresses-field)>): The addresses of the recipients.

```csharp
foreach (var RecipientAddressesElem in result.Document.Inference.Prediction.RecipientAddresses)
{
    System.Console.WriteLine(RecipientAddressesElem.Value);
}
```

## Recipient Names
**RecipientNames**: The names of the recipients.

```csharp
foreach (var RecipientNamesElem in result.Document.Inference.Prediction.RecipientNames)
{
    System.Console.WriteLine(RecipientNamesElem.Value);
}
```

## Sender Address
**SenderAddress**([UsMailV3SenderAddress](#sender-address-field)): The address of the sender.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SenderAddress.Value);
```

## Sender Name
**SenderName**: The name of the sender.

```csharp
System.Console.WriteLine(result.Document.Inference.Prediction.SenderName.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
