---
title: US US Mail OCR .NET
---
The .NET OCR SDK supports the [US Mail API](https://platform.mindee.com/mindee/us_mail).

Using the [sample below](https://github.com/mindee/client-lib-test-data/blob/main/products/us_mail/default_sample.jpg), we are going to illustrate how to extract the data that we want using the OCR SDK.
![US Mail sample](https://github.com/mindee/client-lib-test-data/blob/main/products/us_mail/default_sample.jpg?raw=true)

# Quick-Start
```cs
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
    .EnqueueAndParseAsync<UsMailV2>(inputSource);

// Print a summary of all the predictions
System.Console.WriteLine(response.Document.ToString());

// Print only the document-level predictions
// System.Console.WriteLine(response.Document.Inference.Prediction.ToString());

```

**Output (RST):**
```rst
:Sender Name: zed
:Sender Address:
  :City: Dallas
  :Complete Address: 54321 Elm Street, Dallas, Texas ...
  :Postal Code: 54321
  :State: TX
  :Street: 54321 Elm Street
:Recipient Names: Jane Doe
:Recipient Addresses:
  +-----------------+-------------------------------------+-------------------+-------------+------------------------+-------+---------------------------+
  | City            | Complete Address                    | Is Address Change | Postal Code | Private Mailbox Number | State | Street                    |
  +=================+=====================================+===================+=============+========================+=======+===========================+
  | Detroit         | 1234 Market Street PMB 4321, Det... |                   | 12345       | 4321                   | MI    | 1234 Market Street        |
  +-----------------+-------------------------------------+-------------------+-------------+------------------------+-------+---------------------------+
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

## Specific Fields
Fields which are specific to this product; they are not used in any other product.

### Recipient Addresses Field
The addresses of the recipients.

A `UsMailV2RecipientAddress` implements the following attributes:

* **City** (`string`): The city of the recipient's address.
* **Complete** (`string`): The complete address of the recipient.
* **IsAddressChange** (`bool?`): Indicates if the recipient's address is a change of address.
* **PostalCode** (`string`): The postal code of the recipient's address.
* **PrivateMailboxNumber** (`string`): The private mailbox number of the recipient's address.
* **State** (`string`): Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
* **Street** (`string`): The street of the recipient's address.
Fields which are specific to this product; they are not used in any other product.

### Sender Address Field
The address of the sender.

A `UsMailV2SenderAddress` implements the following attributes:

* **City** (`string`): The city of the sender's address.
* **Complete** (`string`): The complete address of the sender.
* **PostalCode** (`string`): The postal code of the sender's address.
* **State** (`string`): Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
* **Street** (`string`): The street of the sender's address.

# Attributes
The following fields are extracted for US Mail V2:

## Recipient Addresses
**RecipientAddresses** (List<[UsMailV2RecipientAddress](#recipient-addresses-field)>): The addresses of the recipients.

```cs
foreach (RecipientAddressesElem in result.Document.Inference.Prediction.RecipientAddresses)
{
    System.Console.WriteLine(RecipientAddressesElem.Value);
}
```

## Recipient Names
**RecipientNames** : The names of the recipients.

```cs
foreach (RecipientNamesElem in result.Document.Inference.Prediction.RecipientNames)
{
    System.Console.WriteLine(RecipientNamesElem.Value);
}
```

## Sender Address
**SenderAddress** ([UsMailV2SenderAddress](#sender-address-field)): The address of the sender.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.SenderAddress.Value);
```

## Sender Name
**SenderName** : The name of the sender.

```cs
System.Console.WriteLine(result.Document.Inference.Prediction.SenderName.Value);
```

# Questions?
[Join our Slack](https://join.slack.com/t/mindee-community/shared_invite/zt-2d0ds7dtz-DPAF81ZqTy20chsYpQBW5g)
